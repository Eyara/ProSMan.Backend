using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Categories
{
	public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
	{
		private ICategoryService _categoryService { get; set; }

		public DeleteCategoryHandler(
			ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var entity = _categoryService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _categoryService.Delete(entity);
		}
	}
}
