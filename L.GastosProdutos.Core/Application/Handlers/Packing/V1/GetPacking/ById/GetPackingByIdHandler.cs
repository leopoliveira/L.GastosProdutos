﻿using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking.ById
{
    public class GetPackingByIdHandler : IRequestHandler<GetPackingByIdRequest, GetPackingResponse>
    {
        private readonly IPackingRepository _repository;

        public GetPackingByIdHandler(IPackingRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPackingResponse> Handle
        (
            GetPackingByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var packing = await _repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Packing not found.");

            return new GetPackingResponse
            (
                packing.Id,
                packing.Name,
                packing.Description,
                packing.Price,
                packing.Quantity,
                packing.UnitPrice,
                packing.UnitOfMeasure
            );
        }
    }
}
