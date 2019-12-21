using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Queries.Dashboards
{
	public class GetDashboardQuery : IRequest<DashboardViewModel>
	{
		public string Username { get; set; }

		public GetDashboardQuery(string username)
		{
			Username = username;
		}
	}
}
