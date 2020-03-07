using ProSMan.Backend.Core.Interfaces.Entities;
using System.Collections.Generic;

namespace ProSMan.Backend.Domain.ViewModels
{
	public class DashboardViewModel: IDashboard
	{
		public List<IProjectDashboard> Projects { get; set; }

		public DashboardViewModel(List<ProjectDashboard> projects)
		{
			Projects = projects.ConvertAll(x => x as IProjectDashboard);
		}
    }
	
	public class ProjectDashboard: IProjectDashboard
	{
		public string Name { get; set; }
		public IOverviewProjectDashboard Overview { get; set; }
		public List<ICategoryDashboard> Categories { get; set; }

		public ProjectDashboard(OverviewProjectDashboard overview,
			List<CategoryDashboard> categories)
		{
			Overview = overview;
			Categories = categories.ConvertAll(x => x as ICategoryDashboard);
		}
	}

	public class OverviewProjectDashboard: IOverviewProjectDashboard
	{
		public double AverageHoursInSprint { get; set; }
		public double AverageTasksInSprint { get; set; }
		public int TotalSprints { get; set; }
		public int TotalBacklogTasks { get; set; }
		public int TotalNonSprintTasks { get; set; }
	}

	public class CategoryDashboard: ICategoryDashboard
	{
		public string Name { get; set; }
		public double Proportion { get; set; }
	}
}
