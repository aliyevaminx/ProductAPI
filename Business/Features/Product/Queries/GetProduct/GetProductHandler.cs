using AutoMapper;
using Business.Features.Product.Dtos;
using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Product.Read;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Queries.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, Response<ProductDto>>
{
	private readonly IProductReadRepository _productReadRepository;
	private readonly IMapper _mapper;

	public GetProductHandler(IProductReadRepository productReadRepository,
							 IMapper mapper)
	{
		_productReadRepository = productReadRepository;
		_mapper = mapper;
	}

	public async Task<Response<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
	{
		var product = await _productReadRepository.GetAsync(request.Id);
		if (product is null)
			throw new NotFoundException("Product is not found");

		return new Response<ProductDto>
		{
			Data = _mapper.Map<ProductDto>(product)
		};
	}
}
