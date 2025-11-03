using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Product
{
    public record ProductResponse
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    );

    // Canonical write DTO to reduce duplication across Add/Update
    public record ProductWriteDto
    (
        string Name,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.AddProduct
{
    using L.GastosProdutos.Core.Application.Contracts.Product;

    public record AddProductRequest(string Name, decimal Price, decimal Quantity, int UnitOfMeasure)
        : ProductWriteDto(Name, Price, Quantity, UnitOfMeasure);

    public record AddProductResponse(string ProductId);
}

namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.GetProduct
{
    using L.GastosProdutos.Core.Application.Contracts.Product;
    using L.GastosProdutos.Core.Domain.Enums;

    public record GetProductResponse(
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    ) : ProductResponse(Id, Name, Price, Quantity, UnitPrice, UnitOfMeasure);
}

namespace L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct
{
    using L.GastosProdutos.Core.Domain.Enums;
    using L.GastosProdutos.Core.Application.Contracts.Product;

    public record UpdateProductRequest
    (
        string Id,
        string Name,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );

    public record UpdateProductDto(string Name, decimal Price, decimal Quantity, int UnitOfMeasure)
        : ProductWriteDto(Name, Price, Quantity, UnitOfMeasure);
}

