using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

		public void FillData(ProSManContext context, Guid randomGuid)
		{
			context.Users.Add(new User
			{
				Id = randomGuid.ToString(),
				PasswordHash = "",
				UserName = "TestUser"
			});

			context.Projects.Add(new Project
			{
				Id = randomGuid,
				Name = "TestProj",
				UserId = randomGuid.ToString()
			});

			context.Sprints.Add(new Sprint
			{
				Id = randomGuid,
				Name = "TestSprint",
				ProjectId = randomGuid,
				FromDate = new DateTime()
			});

			context.Categories.Add(new Category
			{
				Id = randomGuid,
				Name = "TestCategory",
				ProjectId = randomGuid
			});

			context.SaveChanges();
		}
	}
}
