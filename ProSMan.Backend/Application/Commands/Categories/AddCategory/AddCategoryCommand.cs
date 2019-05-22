using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
    public class AddCategoryCommand : IRequest<bool>
    {
		public CategoryViewModel Category { get; set; }

		public AddCategoryCommand(CategoryViewModel category)
		{
			Category = category;
		}
    }
}
