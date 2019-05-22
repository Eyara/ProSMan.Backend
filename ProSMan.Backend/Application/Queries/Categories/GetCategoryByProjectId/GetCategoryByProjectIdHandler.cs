using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Category.GetByProjectId
{
	public class GetCategoryByProjectIdHandler : IRequestHandler<GetCategoryByProjectIdQuery,
		ListRequest<CategoryViewModel>>
	{
		private ICategoryService _sprintService { get; set; }

		public GetCategoryByProjectIdHandler(
			ICategoryService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<ListRequest<CategoryViewModel>> Handle(GetCategoryByProjectIdQuery request,
			CancellationToken cancellationToken)
		{
			var result = _sprintService
				.GetListByProjectId(request.Id);
			return new ListRequest<CategoryViewModel>(result
				.ConvertAll(x => x as CategoryViewModel));
		}
	}
}