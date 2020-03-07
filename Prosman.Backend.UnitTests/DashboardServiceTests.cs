using Prosman.Backend.UnitTests.Constants;
using Prosman.Backend.UnitTests.Helpers;
using Prosman.Backend.UnitTests.Models;
using ProSMan.Backend.API.Services;
using ProSMan.Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Prosman.Backend.UnitTests
{
	public class DashboardServiceTests
	{
		[Theory]
		[MemberData(nameof(GetTestCases))]
		public void GetCategoryDashboard(IdentifierBase<Guid> identifierModel)
		{
			var contextHelper = new DbContextHelper();

			using (var dbContext = new ProSManContext(contextHelper.CreateNewContextOptions()))
			{
				contextHelper.FillData(dbContext, identifierModel.Id);
				
				var dashboardService = new DashboardService(dbContext);

				var categoryDashboard = dashboardService.GetCategoryDashboard(identifierModel.Id);

				var category = dbContext.Categories
					.Where(c => c.ProjectId == identifierModel.Id)
					.FirstOrDefault();

				Assert.True(categoryDashboard != null);
				Assert.True(categoryDashboard.Count > 0);
				Assert.Contains(categoryDashboard, cd => cd.Name == category.Name);
			}
		}

		[Theory]
		[MemberData(nameof(GetTestCases))]
		public void GetProjectDashboard(IdentifierBase<Guid> identifierModel)
		{
			var contextHelper = new DbContextHelper();

			using (var dbContext = new ProSManContext(contextHelper.CreateNewContextOptions()))
			{
				contextHelper.FillData(dbContext, identifierModel.Id);

				var dashboardService = new DashboardService(dbContext);

				var projectDashboard = dashboardService.GetProjectDashboard(EntityNameConstants.User);

				var project = dbContext.Projects
					.Where(c => c.Id == identifierModel.Id)
					.FirstOrDefault();

				Assert.True(projectDashboard != null);
				Assert.True(projectDashboard.Count > 0);
				Assert.Contains(projectDashboard, cd => cd.Name == project.Name);
			}
		}

		[Theory]
		[MemberData(nameof(GetTestCases))]
		public void GetDashboard(IdentifierBase<Guid> identifierModel)
		{
			var contextHelper = new DbContextHelper();

			using (var dbContext = new ProSManContext(contextHelper.CreateNewContextOptions()))
			{
				contextHelper.FillData(dbContext, identifierModel.Id);

				var dashboardService = new DashboardService(dbContext);

				var dasboard = dashboardService.GetDashboard(EntityNameConstants.User);

				var project = dbContext.Projects
					.Where(c => c.Id == identifierModel.Id)
					.FirstOrDefault();

				Assert.True(dasboard != null);
				Assert.True(dasboard.Projects != null);
				Assert.True(dasboard.Projects.Count > 0);
			}
		}

		[Theory]
		[MemberData(nameof(GetTestCases))]
		public void GetOverviewProject(IdentifierBase<Guid> identifierModel)
		{
			var contextHelper = new DbContextHelper();

			using (var dbContext = new ProSManContext(contextHelper.CreateNewContextOptions()))
			{
				contextHelper.FillData(dbContext, identifierModel.Id, true, new ExtraEntitiesModel
				{
					Count = 2
				});

				var dashboardService = new DashboardService(dbContext);

				var overviewProject = dashboardService.GetOverviewProject(identifierModel.Id);

				Assert.True(overviewProject != null);
				Assert.True(overviewProject.AverageTasksInSprint > 0);
				Assert.True(overviewProject.TotalBacklogTasks > 0);
				Assert.True(overviewProject.TotalNonSprintTasks > 0);
				Assert.True(overviewProject.TotalSprints > 0);
				Assert.True(overviewProject.AverageHoursInSprint > 0);

				// check round to two digits after comma
				Assert.Equal(overviewProject.AverageTasksInSprint, Math.Round(overviewProject.AverageTasksInSprint, 2));
				Assert.Equal(overviewProject.AverageHoursInSprint, Math.Round(overviewProject.AverageHoursInSprint, 2));
			}
		}

		public static IEnumerable<object[]> GetTestCases()
		{
			yield return new object[] {
				new IdentifierBase<Guid>
				{
					Id = Guid.NewGuid()
				}
			};
		}
	}
}
