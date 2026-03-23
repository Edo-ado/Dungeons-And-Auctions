using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryObject : IRepositoryObject
    {

        //Repository se encarga de tener las consultas
        private readonly DAContext _context;

        public RepositoryObject(DAContext context)
        {
            _context = context;
        }

        public async Task<Objects?> FindByIdAsync(int id)
        {
            return await _context.Set<Objects>()
                   .AsNoTracking()
                   .Include(o => o.Category)
                   .Include(o => o.Auctions)
                       .ThenInclude(a => a.IdstateNavigation)
                   .Include(o => o.User)
                   .Include(o => o.IdQualityNavigation)
               .Include(o => o.IdImageNavigation)
                   .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<List<Categories>> GetCategoriesByIdObject(int id)
        {
            return await _context.Set<Objects>()
                .AsNoTracking()
                .Where(o => o.Id == id)
                .SelectMany(o => o.Category)

                .ToListAsync();
        }

        public async Task<ICollection<Objects>> ListAsync()
        {
            return await _context.Objects
               .AsNoTracking()
               .Include(o => o.Category)
               .Include(o => o.User)
               .Include(o => o.IdQualityNavigation)
               .Include(o => o.IdImageNavigation)
               .ToListAsync();
        }

        public async Task UpdateAsync(Objects entity, List<int> categoryIds, List<byte[]> imagenes)
        {
            // entity DEBE venir trackeado
            // Igual se reestablece
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            // Si el mapping ya actualizó propiedades escalares, esto garantiza update
            _context.Entry(entity).State = EntityState.Modified;

            await ApplyCategoriesAsync(entity, categoryIds);




            if (imagenes != null && imagenes.Any())
            {
                var oldImages = _context.Images.Where(i => i.IdObject == entity.Id);
                _context.Images.RemoveRange(oldImages);

                foreach (var img in imagenes)
                {
                    _context.Images.Add(new Images
                    {
                        IdObject = entity.Id,
                        ImageData = img
                    });
                }
            }

            await _context.SaveChangesAsync();
        
    }


        public async Task<int> AddAsync(Objects entity, List<int> categoryIds, List<byte[]> imagenes)
        {
            await ApplyCategoriesAsync(entity, categoryIds);

       
            entity.IdImageNavigation ??= new List<Images>();

            foreach (var img in imagenes)
            {
                entity.IdImageNavigation.Add(new Images
                {
                    ImageData = img
                });
            }
            await _context.Set<Objects>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await _context.Objects.FindAsync(id);
            if (obj is null) return;
            obj.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleActiveAsync(int id)
        {
            var obj = await _context.Objects.FindAsync(id);

            if (obj is null)
                return;

            obj.IsActive = !obj.IsActive;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasActiveAuctionAsync(int objectId)
        {
            return await _context.Auctions
                .Include(a => a.IdstateNavigation)
                .AnyAsync(a => a.Idobject == objectId
                    && a.IdstateNavigation.Name == "Open");
        }

        public async Task ApplyCategoriesAsync(Objects ObjectToUpdate, List<int> selectedCategorias)
        {
            //si no enviaron categorías, se establece vacío
            if (selectedCategorias == null || selectedCategorias.Count == 0)
            {
                ObjectToUpdate.Category = new List<Categories>();
                return;
            }

           var ids = selectedCategorias.Distinct().ToList();
            //el distinct se encarga de eliminar duplicados de la lista

      
            //Trae SOLO las categorías requeridas solicitadas
            var categorias = await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

            ObjectToUpdate.Category = categorias;
        }

        public async Task<bool> HasBeenAuctionedAsync(int objectId)
        {
            return await _context.Auctions.AnyAsync(a => a.Idobject == objectId);
        }
    }
}
