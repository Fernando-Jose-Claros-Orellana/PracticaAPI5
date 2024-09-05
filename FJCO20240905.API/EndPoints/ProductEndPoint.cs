using FJCO20240905.API.Models.DAL;
using FJCO20240905.API.Models.EN;
using FJCO20240905.DTOs.ProductFJCO_DTOs;

namespace FJCO20240905.API.EndPoints
{
    public static class ProductEndPoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {
          
            app.MapPost("/product/search", async (SearchQueryProductDTO productDTO, FJCOProductDAL productDAL) =>
            {
               
                var product = new ProductFJCO
                {
                   Nombre = productDTO.Nombre != null ? productDTO.Nombre : string.Empty,
                    Descripcion = productDTO.Descripcion != null ? productDTO.Descripcion : string.Empty,
                };

               var products = new List<ProductFJCO>();
                int countRow = 0;

                if (productDTO.SendRowCount == 2)
                {
                    products = await productDAL.Search(product, skip: productDTO.skip, take: productDTO.take);
                    if (products.Count > 0)
                        countRow = await productDAL.CountSearch(product);
                }
                else
                {
                   products = await productDAL.Search(product, skip: productDTO.skip, take: productDTO.take);
                }

                var productResult = new SearchResultProductDTO
                {
                    Data = new List<SearchResultProductDTO.ProductDTO>(),
                    CountRow = countRow
                };

                products.ForEach(s => {
                    productResult.Data.Add(new SearchResultProductDTO.ProductDTO
                    {
                        Id = s.Id,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        Precio = s.Precio
                    });
                });

                return productResult;
            });
                
            
            app.MapGet("/product/{id}", async (int id, FJCOProductDAL productDAL) =>
            { 
                var product = await productDAL.GetById(id);

                var productResult = new GetIdResultProductDTO
                {
                    Id = product.Id,
                    Nombre = product.Nombre,
                    Descripcion = product.Descripcion,
                    Precio = product.Precio
                };

                if (productResult.Id > 0)
                    return Results.Ok(productResult);
                else
                    return Results.NotFound(productResult);
            });

            app.MapPost("/product", async (CreateProductDTO productDTO, FJCOProductDAL productDAL) =>
            {
                var product = new ProductFJCO
                {
                    Nombre = productDTO.Nombre,
                    Descripcion = productDTO.Decripcion,
                    Precio = productDTO.Precio
                };

                int result = await productDAL.Create(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo PUT para editar un cliente existente
            app.MapPut("/product", async (EditProductDTO productDTO, FJCOProductDAL productDAL) =>
            {
                var product = new ProductFJCO
                {
                    Id = productDTO.Id,
                    Nombre = productDTO.Nombre,
                    Descripcion = productDTO.Descripcion,
                    Precio = productDTO.Precio
                };

                int result = await productDAL.Edit(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            // Configurar un endpoint de tipo DELETE para eliminar un cliente por ID
            app.MapDelete("/product/{id}", async (int id, FJCOProductDAL productDAL) =>
            {
                int result = await productDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
        }
    }
}
