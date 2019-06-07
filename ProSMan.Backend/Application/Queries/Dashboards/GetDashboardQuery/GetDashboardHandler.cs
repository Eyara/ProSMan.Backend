using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Dashboards
{
	public class GetDashboardHandler : IRequestHandler<GetDashboardQuery, DashboardViewModel>
	{
		private IDashboardService _dashboardService { get; set; }

		public GetDashboardHandler(
			IDashboardService projectService)
		{
			_dashboardService = projectService;
		}

		public async Task<DashboardViewModel> Handle(GetDashboardQuery request,
			CancellationToken cancellationToken)
		{
			return _dashboardService.GetDashboard(request.Username)
				as DashboardViewModel;
		}
	}
}