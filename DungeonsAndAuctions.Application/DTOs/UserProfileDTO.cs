namespace D_A.Application.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AboutMe { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly RegisterDate { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }

    public class UpdateProfileDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AboutMe { get; set; }
    }

    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    public class UserBidHistoryDTO
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime BidDate { get; set; }
        public bool IsLastBid { get; set; }
    }

    public class UserAuctionDTO
    {
        public int Id { get; set; }
        public string ObjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal BasePrice { get; set; }
        public string State { get; set; } = string.Empty;
    }

    public class UserPaymentDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string AuctionName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}