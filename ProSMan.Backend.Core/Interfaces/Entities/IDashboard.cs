using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface IDashboard
    {
		double AverageDayHours { get; set; }
		List<IProjectDashboard> Projects { get; set; }
	}

	public interface IProjectDashboard
	{
		string Name { get; set; }
		List<ISprintDashboard> Sprints { get; set; }
		List<ICategoryDashboard> Categories { get; set; }
	}

	public interface ISprintDashboard
	{
		string Name { get; set; }
		int TaskCount { get; set; }
		List<ITaskDashboard> Tasks { get; set; }
	}

	public interface ITaskDashboard
	{
		DateTime Date { get; set; }
		int Count { get; set; }
	}

	public interface ICategoryDashboard
	{
		string Name { get; set; }
		double Proportion { get; set; }
	}
}
