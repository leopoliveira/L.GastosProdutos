using L.GastosProdutos.Core.Application.Handlers.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct;

namespace L.GastosProdutos.Core.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<GetProductResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<AddProductResponse> AddAsync(AddProductRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(string id, UpdateProductDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}

