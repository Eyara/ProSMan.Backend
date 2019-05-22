using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
	public class UpdateCategoryCommand : IRequest<bool>
	{
		public CategoryViewModel Category { get; set; }

		public UpdateCategoryCommand(CategoryViewModel category)
		{
			Category = category;
		}
	}
}
