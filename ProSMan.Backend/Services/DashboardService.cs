using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProSMan.Backend.API.Services
{
	public class DashboardService : IDashboardService
	{
		private ProSManContext _dbContext { get; set; }

		public DashboardService(ProSManContext context)
		{
			_dbContext = context;
		}

		public IDashboard GetDashboard(string username)
		{
			var projects = GetProjectDashboard(username);

			return new DashboardViewModel(projects);
		}

		public List<ProjectDashboard> GetProjectDashboard(string username)
		{
			var projects = _dbContext.Projects
				.Where(x => x.User.UserName == username && !x.IsDeleted)
				.ToList();

			var projectList = new List<ProjectDashboard>();

			foreach (var project in projects)
			{
				projectList.Add(new ProjectDashboard(GetOverviewProject(project.Id),
					GetCategoryDashboard(project.Id))
				{
					Name = project.Name
				});
			}

			return projectList;
		}

		public OverviewProjectDashboard GetOverviewProject(Guid projectId)
		{
			var totalTaskCount = _dbContext.Tasks.Count(task => task.ProjectId == projectId);
			var totalSprintCount = _dbContext.Sprints.Count(sprint => sprint.ProjectId == projectId && !sprint.IsDeleted);
			var totalHoursCount = _dbContext.Tasks
					.Where(task => task.ProjectId == projectId)
					.Sum(task => task.TimeEstimate);

			return new OverviewProjectDashboard
			{
				TotalBacklogTasks = _dbContext.BacklogTasks.Count(task => task.ProjectId == projectId),
				TotalNonSprintTasks = _dbContext.NonSprintTasks.Count(task => task.ProjectId == projectId),
				TotalSprints = totalSprintCount,
				AverageHoursInSprint = Math.Round(totalHoursCount / (double)totalSprintCount, 2),
				AverageTasksInSprint = Math.Round(totalTaskCount / (double)totalSprintCount, 2)
			};
		}

		public List<CategoryDashboard> GetCategoryDashboard(Guid projectId)
		{
			var categoriesQuery = _dbContext.Categories
				.Where(x => x.ProjectId == projectId && !x.IsDeleted);

			var tasksQuery = _dbContext.Tasks
				.Where(x => x.ProjectId == projectId &&
					categoriesQuery.Any(c => c.Id == x.CategoryId));

			var tasksCount = tasksQuery.Count();

			return categoriesQuery
				.Select(x => new CategoryDashboard
				{
					Name = x.Name,
					Proportion = Math.Round(tasksQuery.Count(t => t.CategoryId == x.Id) /
						Convert.ToDouble(tasksCount), 3)
				})
				.ToList();
		}
	}
}