using MediatR;
using ProSMan.Backend.Core.Base;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetSprintsByProjectHandler : IRequestHandler<GetSprintsByProjectQuery, PaginationResponse<SprintViewModel>>
	{
		private ISprintService _sprintService { get; set; }

		public GetSprintsByProjectHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<PaginationResponse<SprintViewModel>> Handle(GetSprintsByProjectQuery request,
			CancellationToken cancellationToken)
		{
			var paginatedSprints = _sprintService
				.GetListByProjectId(request.Model);

			return new PaginationResponse<SprintViewModel>
			{
				CurrentPage = paginatedSprints.CurrentPage,
				LastPage = paginatedSprints.LastPage,
				PageCount = paginatedSprints.PageCount,
				TotalCount = paginatedSprints.TotalCount,
				Items = paginatedSprints.Items.ConvertAll(x => x as SprintViewModel)
			};
		}
	}
}