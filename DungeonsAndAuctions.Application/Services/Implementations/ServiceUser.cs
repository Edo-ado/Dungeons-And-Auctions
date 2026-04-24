using AutoMapper;
using D_A.Application.Config;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Application.Utils;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DNDA.Web.Models.Reports;

namespace D_A.Application.Services.Implementations
{
    public class ServiceUser : IServiceUser
    {
        private readonly IRepositoryUser _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;
        private readonly ILogger<ServiceUser> _logger;

        public ServiceUser(
            IRepositoryUser repository, 
            IMapper mapper, 
            IOptions<AppConfig> options,
            ILogger<ServiceUser> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
            _logger = logger;
        }

        public async Task<List<UsersDTO>> ListAsync()
        {
            var users = await _repository.ListAsync();
            return _mapper.Map<List<UsersDTO>>(users);
        }

        public async Task<UsersDTO> FindByIdAsync(int id)
        {
            var user = await _repository.FindByIdAsync(id);
            return _mapper.Map<UsersDTO>(user);
        }

        public async Task UpdateAsync(int id, UsersDTO dto)
        {
            var user = await _repository.FindByIdForUpdateAsync(id);
            _mapper.Map(dto, user);
            await _repository.UpdateAsync(user!);
        }

        public async Task ToggleBlockAsync(int id) => await _repository.ToggleBlockAsync(id);
        public async Task ToggleActiveAsync(int id) => await _repository.ToggleActiveAsync(id);

        public async Task<UsersDTO> GetWinnerUserByPaymentAsync(int winnerUserId)
        {
            var user = await _repository.GetWinnerUserByPaymentAsync(winnerUserId);
            return _mapper.Map<UsersDTO>(user);
        }

        public async Task<ICollection<BuyerActivity>> GetBuyerActivityReportAsync(DateTime dateFrom, DateTime dateTo)
            => await _repository.GetBuyerActivityReportAsync(dateFrom, dateTo);

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _repository.FindByEmailAsync(email);
            return user != null;
        }

        /// <summary>
        /// Login con validación detallada y logging mejorado
        /// </summary>
        public async Task<UsersDTO?> LoginAsync(string email, string password)
        {
            try
            {
                _logger.LogInformation("Intento de login para: {Email}", email);

                // Buscar usuario por email
                var user = await _repository.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado: {Email}", email);
                    return null;
                }

                // Validar que no esté bloqueado
                if (user.IsBlocked)
                {
                    _logger.LogWarning("Usuario bloqueado: {Email}", email);
                    return null;
                }

                // Validar que esté activo
                if (!user.Active)
                {
                    _logger.LogWarning("Usuario inactivo: {Email}", email);
                    return null;
                }

                // Verificar contraseña
                byte[] passwordHash = Cryptography.EncryptToBytes(password, _options.Value.Crypto.Secret);

                bool passwordMatch = await _repository.LoginAsync(email, passwordHash) != null;

                if (!passwordMatch)
                {
                    _logger.LogWarning("Contraseña incorrecta para: {Email}", email);
                    return null;
                }

                _logger.LogInformation("Login exitoso para: {Email} (Id={UserId})", email, user.Id);
                return _mapper.Map<UsersDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en LoginAsync para email: {Email}", email);
                return null;
            }
        }

        public async Task<UsersDTO?> RegisterAsync(RegisterUserDTO dto)
        {
            try
            {
                if (await EmailExistsAsync(dto.Email))
                {
                    _logger.LogWarning("Intento de registro con email duplicado: {Email}", dto.Email);
                    return null;
                }

                string secret = _options.Value.Crypto.Secret;
                if (string.IsNullOrEmpty(secret))
                {
                    _logger.LogError("Crypto:Secret no está configurado en appsettings.json");
                    return null;
                }

                byte[] passwordHash = Cryptography.EncryptToBytes(dto.Password, secret);

                var newUser = new Users
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                    Email = dto.Email,
                    PasswordHash = passwordHash,
                    BirthDate = dto.BirthDate,
                    CountryId = dto.CountryId,
                    GenderId = dto.GenderId,
                    RoleId = dto.RoleId,
                    PhoneNumber = dto.PhoneNumber,
                    AboutMe = dto.AboutMe,
                    Active = true,
                    IsBlocked = false,
                    RegisterDate = DateOnly.FromDateTime(DateTime.Today),
                    LastLogin = null
                };

                await _repository.CreateAsync(newUser);

                _logger.LogInformation("Usuario registrado exitosamente: {Email} (Id={UserId})", newUser.Email, newUser.Id);

                return new UsersDTO
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    RoleId = newUser.RoleId,
                    CountryId = newUser.CountryId,
                    GenderId = newUser.GenderId,
                    Active = newUser.Active,
                    IsBlocked = newUser.IsBlocked,
                    RegisterDate = newUser.RegisterDate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en RegisterAsync para email: {Email}", dto.Email);
                return null;
            }
        }



        public async Task<Users?> GetProfileAsync(int userId)
    => await _repository.GetProfileAsync(userId);

        public async Task<bool> UpdateProfileAsync(int userId, string firstName, string lastName, string? phoneNumber, string? aboutMe)
            => await _repository.UpdateProfileAsync(userId, firstName, lastName, phoneNumber, aboutMe);

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            byte[] currentHash = Cryptography.EncryptToBytes(currentPassword, _options.Value.Crypto.Secret);
            byte[] newHash = Cryptography.EncryptToBytes(newPassword, _options.Value.Crypto.Secret);
            return await _repository.ChangePasswordAsync(userId, currentHash, newHash);
        }

        public async Task<ICollection<AuctionBidHistory>> GetBidHistoryAsync(int userId)
            => await _repository.GetBidHistoryAsync(userId);

        public async Task<ICollection<Auctions>> GetUserAuctionsAsync(int userId)
            => await _repository.GetUserAuctionsAsync(userId);

        public async Task<ICollection<Payment>> GetUserPaymentsAsync(int userId)
            => await _repository.GetUserPaymentsAsync(userId);





    }
}