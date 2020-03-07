using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prosman.Backend.UnitTests.Constants;
using Prosman.Backend.UnitTests.Models;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using System;

namespace Prosman.Backend.UnitTests.Helpers
{
	public class DbContextHelper
	{
		public DbContextOptions<ProSManContext> CreateNewContextOptions()
		{
			var serviceProvider = new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider();

			var builder = new DbContextOptionsBuilder<ProSManContext>();
			builder.UseInMemoryDatabase("Prosman")
				   .UseInternalServiceProvider(serviceProvider);

			return builder.Options;
		}

		public void FillData(ProSManContext context, Guid randomGuid, bool needToFillTasks = false, 
			ExtraEntitiesModel extraEntitiesModel= null)
		{
			context.Users.Add(new User
			{
				Id = randomGuid.ToString(),
				PasswordHash = "",
				UserName = EntityNameConstants.User
			});

			context.Projects.Add(new Project
			{
				Id = randomGuid,
				Name = EntityNameConstants.Project,
				UserId = randomGuid.ToString()
			});

			context.Sprints.Add(new Sprint
			{
				Id = randomGuid,
				Name = EntityNameConstants.Sprint,
				ProjectId = randomGuid,
				FromDate = new DateTime()
			});

			context.Categories.Add(new Category
			{
				Id = randomGuid,
				Name = EntityNameConstants.Category,
				ProjectId = randomGuid
			});

			if (needToFillTasks)
			{
				context.Tasks.Add(new Task
				{
					Id = randomGuid,
					CategoryId = randomGuid,
					ProjectId = randomGuid,
					SprintId = randomGuid,
					Name = EntityNameConstants.Task,
					TimeEstimate = 5
				});

				context.BacklogTasks.Add(new BacklogTask
				{
					Id = randomGuid,
					ProjectId = randomGuid,
					Name = EntityNameConstants.BacklogTask
				});

				context.NonSprintTasks.Add(new NonSprintTask
				{
					Id = randomGuid,
					ProjectId = randomGuid,
					Name = EntityNameConstants.NonSprintTask
				});
			}

			if (extraEntitiesModel != null)
			{
				for (int i = 0; i < extraEntitiesModel.Count; i++)
				{
					context.Sprints.Add(new Sprint
					{
						Id = Guid.NewGuid(),
						Name = EntityNameConstants.ExtraSprint,
						ProjectId = randomGuid,
						FromDate = new DateTime().AddDays(1)
					});
				}
			}

			context.SaveChanges();
		}
	}
}
