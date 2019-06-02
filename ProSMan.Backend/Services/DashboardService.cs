using Microsoft.EntityFrameworkCore;
using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
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

			return new DashboardViewModel(projects)
			{
				AverageDayHours = GetAverageDayHours(username),
			};
		}

		private List<ProjectDashboard> GetProjectDashboard(string username)
		{
			var projects = _dbContext.Projects
				.Where(x => x.User.UserName == username && !x.IsDeleted)
				.ToList();

			var projectList = new List<ProjectDashboard>();

			foreach(var project in projects)
			{
				var sprint = GetSprintDashboard(project.Id);
				var category = GetCategoryDashboard(project.Id);

				projectList.Add(new ProjectDashboard(GetSprintDashboard(project.Id), 
					GetCategoryDashboard(project.Id))
				{
					Name = project.Name
				});
			}

			return projectList;
		}

		private List<SprintDashboard> GetSprintDashboard(Guid projectId)
		{
			var sprints = _dbContext.Sprints
				.Where(x => x.ProjectId == projectId && !x.IsDeleted)
				.ToList();

			return sprints
				.Select(x => new SprintDashboard(GetTaskDashboard(x.Id))
				{
					Name = x.Name,
					TaskCount = GetTaskDashboard(x.Id).ConvertAll(t => t as ITaskDashboard).Count()
				})
				.ToList();
		}

		private List<TaskDashboard> GetTaskDashboard(Guid sprintId)
		{
			return _dbContext.Tasks
				.Where(x => x.SprintId == sprintId && x.FinishedOn.HasValue)
				.GroupBy(x => x.FinishedOn)
				.Select(x => new TaskDashboard
				{
					Date = x.Key.Value,
					Count = x.Count()
				})
				.ToList();
		}

		private List<CategoryDashboard> GetCategoryDashboard(Guid projectId)
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

		private double GetAverageDayHours(string username)
		{
			var minTaskDate = _dbContext.Tasks
				.Min(x => x.Date);

			var minNonSprintTaskDate = _dbContext.NonSprintTasks
				.Min(x => x.Date);

			minTaskDate = minTaskDate.HasValue ? minTaskDate.Value : DateTime.UtcNow;
			minNonSprintTaskDate = minNonSprintTaskDate.HasValue ? minNonSprintTaskDate.Value : DateTime.UtcNow;

			var minDate = minTaskDate.Value < minNonSprintTaskDate.Value
				? minTaskDate.Value
				: minNonSprintTaskDate.Value;

			var dayCount = DateTime.UtcNow.Subtract(minDate).TotalDays;

			var tasksHours = _dbContext.Tasks
				.Where(x => x.Project.User.UserName == username && x.IsFinished)
				.Sum(x => x.TimeEstimate);

			var nonSprintTasksHours = _dbContext.NonSprintTasks
				.Where(x => x.Project.User.UserName == username && x.IsFinished)
				.Sum(x => x.TimeEstimate);

			var averageDayHours = (tasksHours + nonSprintTasksHours) / dayCount;

			return averageDayHours;
		}
	}
}