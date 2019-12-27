using AutoMapper;
using Prosman.Backend.UnitTests.Helpers;
using ProSMan.Backend.API.Extensions.Profiles;
using ProSMan.Backend.API.Services;
using ProSMan.Backend.Core.Interfaces.Entities;
using ProSMan.Backend.Domain.ViewModels;
using ProSMan.Backend.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Prosman.Backend.UnitTests
{
	public class BacklogServiceTests
	{
		[Theory]
		[MemberData(nameof(GetBacklogTestCases))]
		public void Add(IBacklogTask backlogTask)
		{
			var contextHelper = new DbContextHelper();

			using (var dbContext = new ProSManContext(contextHelper.CreateNewContextOptions()))
			{
				contextHelper.FillData(dbContext, backlogTask.ProjectId);

				#region Mock dependencies

				var mapper = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile(new BacklogTaskProfile());
				}).CreateMapper();

				#endregion

				var backlogTaskService = new BacklogTaskService(dbContext, mapper);

				backlogTaskService.Add(backlogTask);

				var backlogEntity = dbContext.BacklogTasks
					.FirstOrDefault();

				Assert.Equal(backlogTask.Name, backlogEntity.Name);
				Assert.Equal(backlogTask.Description, backlogEntity.Description);
				Assert.Equal(backlogTask.ProjectId, backlogEntity.ProjectId);
			}
		}
		public static IEnumerable<object[]> GetBacklogTestCases()
		{
			yield return new object[] {
				new BacklogTaskViewModel
				{
					Description = "Test",
					Name = "Name",
					ProjectId = Guid.NewGuid()
				}
			};
		}
	}
}
