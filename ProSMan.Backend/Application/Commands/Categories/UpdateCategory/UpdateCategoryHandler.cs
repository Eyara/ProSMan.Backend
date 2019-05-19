using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
	public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
	{
		private ICategoryService _categoryService { get; set; }

		public UpdateCategoryHandler(
			ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var entity = _categoryService.GetItemById(request.Category.Id);

			if (entity == null)
			{
				return false;
			}

			return _categoryService.Update(request.Category);
		}
	}
}
