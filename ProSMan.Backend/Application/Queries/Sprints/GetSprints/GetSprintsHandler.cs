using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetSprintsByIdHandler : IRequestHandler<GetSprintsByIdQuery, ListRequest<SprintViewModel>>
	{
		private ISprintService _sprintService { get; set; }

		public GetSprintsByIdHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<ListRequest<SprintViewModel>> Handle(GetSprintsByIdQuery request,
			CancellationToken cancellationToken)
		{
			var result = _sprintService
				.GetListById(request.Id);
			return new ListRequest<SprintViewModel>(result
				.ConvertAll(x => x as SprintViewModel));
		}
	}
}