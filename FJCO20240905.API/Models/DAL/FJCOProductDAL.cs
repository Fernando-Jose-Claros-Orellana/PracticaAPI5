using FJCO20240905.API.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace FJCO20240905.API.Models.DAL
{
    public class FJCOProductDAL
    {
        readonly FJCOContext _context;

        public FJCOProductDAL(FJCOContext context)
        {
            _context = context; 
        }

        public async Task<int> Create(ProductFJCO product)
        {
            _context.ProductsFJCO.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductFJCO> GetById(int id)
        {
            var product = await _context.ProductsFJCO.FirstOrDefaultAsync(s => s.Id == id);
            return product != null ? product:new ProductFJCO();
        }


        public async Task<int> Edit(ProductFJCO product)
        {
            int result = 0;
            var productUpdate = await GetById(product.Id);
            if (productUpdate.Id != 0)
            {
                productUpdate.Nombre = product.Nombre;
                productUpdate.Descripcion = product.Descripcion;
                productUpdate.Precio = product.Precio;

                result = await _context.SaveChangesAsync();
            }
           return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var product = await GetById(id);
            if (product.Id != 0)
            {
                _context.ProductsFJCO.Remove(product);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        private IQueryable<ProductFJCO> Query(ProductFJCO product)
        {
            var query = _context.ProductsFJCO.AsQueryable();
            if (!string.IsNullOrEmpty(product.Nombre))
            {
                query = query.Where(s => s.Nombre.Contains(product.Nombre));
            }
            if (!string.IsNullOrEmpty(product.Descripcion))
            {
                query = query.Where(s => s.Descripcion.Contains(product.Descripcion));
            }
            if (product.Precio != 0)
            {
                query = query.Where(s => s.Precio == product.Precio);
            }
            return query;
        }

        public async Task<int> CountSearch(ProductFJCO product)
        {
            return await Query(product).CountAsync();
        }

        public async Task<List<ProductFJCO>> Search(ProductFJCO product, int take = 10, int skip = 0)
        {
           take = take == 0 ? 10 : take;
            var query = Query(product);
            query = query.OrderByDescending(s => s.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }

    }
}
