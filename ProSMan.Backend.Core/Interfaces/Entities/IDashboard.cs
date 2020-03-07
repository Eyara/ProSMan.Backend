using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
	public interface IDashboard
    {
		List<IProjectDashboard> Projects { get; set; }
	}

	public interface IProjectDashboard
	{
		string Name { get; set; }
		IOverviewProjectDashboard Overview { get; set; }
		List<ICategoryDashboard> Categories { get; set; }
	}

	public interface IOverviewProjectDashboard
	{
		double AverageHoursInSprint { get; set; }
		double AverageTasksInSprint { get; set; }
		int TotalSprints { get; set; }
		int TotalBacklogTasks { get; set; }
		int TotalNonSprintTasks { get; set; }
	}

	public interface ICategoryDashboard
	{
		string Name { get; set; }
		double Proportion { get; set; }
	}
}
