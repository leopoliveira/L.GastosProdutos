using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Application.Contracts.Packing
{
    // Base read model for Packing
    public record PackingResponse
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    );

    // Base write DTO reused across requests
    public record PackingWriteDto
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.AddPacking
{
    using L.GastosProdutos.Core.Application.Contracts.Packing;

    public record AddPackingRequest(string Name, string? Description, decimal Price, decimal Quantity, int UnitOfMeasure)
        : PackingWriteDto(Name, Description, Price, Quantity, UnitOfMeasure);

    public record AddPackingResponse(string PackingID);
}

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking
{
    using L.GastosProdutos.Core.Application.Contracts.Packing;
    using L.GastosProdutos.Core.Domain.Enums;

    public record GetPackingResponse(
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        decimal UnitPrice,
        EnumUnitOfMeasure UnitOfMeasure
    ) : PackingResponse(Id, Name, Description, Price, Quantity, UnitPrice, UnitOfMeasure);
}

namespace L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking
{
    using L.GastosProdutos.Core.Domain.Enums;
    using L.GastosProdutos.Core.Application.Contracts.Packing;

    public record UpdatePackingRequest
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    );

    public record UpdatePackingDto(string Name, string? Description, decimal Price, decimal Quantity, int UnitOfMeasure)
        : PackingWriteDto(Name, Description, Price, Quantity, UnitOfMeasure);
}

