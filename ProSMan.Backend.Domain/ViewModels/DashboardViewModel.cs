using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class DashboardViewModel: IDashboard
	{
		public double AverageDayHours { get; set; }
		public List<IProjectDashboard> Projects { get; set; }

		public DashboardViewModel(List<ProjectDashboard> projects)
		{
			Projects = projects.ConvertAll(x => x as IProjectDashboard);
		}
    }
	
	public class ProjectDashboard: IProjectDashboard
	{
		public string Name { get; set; }
		public List<ISprintDashboard> Sprints { get; set; }
		public List<ICategoryDashboard> Categories { get; set; }

		public ProjectDashboard(List<SprintDashboard> sprints,
			List<CategoryDashboard> categories)
		{
			Sprints = sprints.ConvertAll(x => x as ISprintDashboard);
			Categories = categories.ConvertAll(x => x as ICategoryDashboard);
		}
	}

	public class SprintDashboard: ISprintDashboard
	{
		public string Name { get; set; }
		public int TaskCount { get; set; }
		public List<ITaskDashboard> Tasks { get; set; }

		public SprintDashboard(List<TaskDashboard> tasks)
		{
			Tasks = tasks.ConvertAll(x => x as ITaskDashboard);
		}
	}

	public class TaskDashboard: ITaskDashboard
	{
		public DateTime Date { get; set; }
		public int Count { get; set; }
	}

	public class CategoryDashboard: ICategoryDashboard
	{
		public string Name { get; set; }
		public double Proportion { get; set; }
	}
}
