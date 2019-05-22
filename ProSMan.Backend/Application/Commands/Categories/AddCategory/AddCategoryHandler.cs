using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
	public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, bool>
	{
		private ICategoryService _categoryService { get; set; }

		public AddCategoryHandler(
			ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
		{
			return _categoryService.Add(request.Category);
		}
	}
}
