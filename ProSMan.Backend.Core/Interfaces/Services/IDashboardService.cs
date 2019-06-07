using ProSMan.Backend.Core.Interfaces.Entities;

namespace ProSMan.Backend.Core.Interfaces.Services
{
    public interface IDashboardService
    {
		IDashboard GetDashboard(string username);
	}
}
