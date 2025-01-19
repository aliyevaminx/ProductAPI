using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Product.Read;
using Data.Repositories.Product.Write;
using Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response>
{
	private readonly IProductWriteRepository _productWriteRepository;
	private readonly IProductReadRepository _productReadRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteProductHandler(IProductWriteRepository productWriteRepository,
								IProductReadRepository productReadRepository,
								IUnitOfWork unitOfWork)
	{
		_productWriteRepository = productWriteRepository;
		_productReadRepository = productReadRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var product = await _productReadRepository.GetAsync(request.Id);
		if (product is null)
			throw new NotFoundException("Product is not found");

		_productWriteRepository.Delete(product);
		await _unitOfWork.CommitAsync();

		return new Response()
		{
			Message = "Product deleted successfully"
		};
	}
}
