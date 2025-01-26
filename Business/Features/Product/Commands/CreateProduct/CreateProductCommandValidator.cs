using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name)
			   .NotEmpty()
			   .WithMessage("Name is required");

		RuleFor(x => x.Quantity)
			.GreaterThan(0)
			.WithMessage("Quantity must be at least one");

		RuleFor(x => x.Price)
			.GreaterThan(0)
			.WithMessage("Price must be greater than zero");

		RuleFor(x => x.Description)
			.MinimumLength(20)
			.MaximumLength(200)
			.WithMessage("Description should be between 20-200 characters");

		RuleFor(x => x.Type)
			.IsInEnum()
			.WithMessage("Type must be correct");

		RuleFor(x => x.Photo)
			.NotEmpty()
			.WithMessage("Şəkil daxil edilməlidir");

		RuleFor(x => x.Photo)
			.Must(IsCorrectFormat)
			.WithMessage("Photo type is incorrect");
	}

	private bool IsCorrectFormat(string photo)
	{
		try
		{
			_ = Convert.FromBase64String(photo);
			var data = photo.Substring(0, 5);

			switch (data.ToUpper())
			{
				case "IVBOR":
				case "/9J/4":
					return true;
				default:
					return false;
			};
		}
		catch (Exception)
		{
			return false;
		}
	}
}
