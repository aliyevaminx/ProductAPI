using AutoMapper;
using Business.Features.Product.Commands.CreateProduct;
using Business.Wrappers;
using Core.Constants;
using Core.Exceptions;
using Data.Repositories.Product.Read;
using Data.Repositories.Product.Write;
using Data.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockData.Product.CreateProductHandler;

namespace UnitTests.Handles.Product.Command.Product;

public class CreateProductHandlerTests
{
	private readonly Mock<IUnitOfWork> _unitOfWork;
	private readonly Mock<IProductReadRepository> _productReadRepository;
	private readonly Mock<IProductWriteRepository> _productWriteRepository;
	private readonly Mock<IMapper> _mapper;
	private readonly CreateProductHandler _handler;

	public CreateProductHandlerTests()
	{
		_unitOfWork = new Mock<IUnitOfWork>();
		_mapper = new Mock<IMapper>();
		_productReadRepository = new Mock<IProductReadRepository>();
		_productWriteRepository = new Mock<IProductWriteRepository>();

		_handler = new CreateProductHandler(_unitOfWork.Object, _productWriteRepository.Object, _productReadRepository.Object, _mapper.Object);
	}

	[Fact]
	public async Task Handle_WhenValidatorIsFailed_ShouldThrowValidationException()
	{
		//Arrange
		var request = CreateProductHandlerMockData.CreateProductCommandV1;

		//Act
		Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

		//Assert
		var exception = await Assert.ThrowsAsync<ValidationException>(func);
		Assert.Contains("Şəkil daxil edilməlidir", exception.Errors);
	}

	[Fact]
	public async Task Handle_WhenProductAlreadyExist_ShouldThrowValidationException()
	{
		//Arrange
		var request = CreateProductHandlerMockData.CreateProductCommandV2;

		_productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
			.ReturnsAsync(new Core.Entities.Product());

		//Act 
		Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

		//Assert
		var exception = await Assert.ThrowsAsync<ValidationException>(func);
		Assert.Contains("The product is already exist", exception.Errors);
	}

	[Fact]
	public async Task Handle_WhenFlowIsSucceeded_ShouldReturnResponseModel()
	{
		//Arrange
		var request = CreateProductHandlerMockData.CreateProductCommandV2;

		_productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
			.ReturnsAsync(value: null);

		_mapper.Setup(x => x.Map<Core.Entities.Product>(request))
			.Returns(new Core.Entities.Product());

		//Act
		var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

		//Assert
		Assert.IsType<Response>(response);
		Assert.Equal("Product created successfully", response.Message);
	}
}