using Microsoft.EntityFrameworkCore;
using ProSMan.Backend.Core.Extensions;
using ProSMan.Backend.Model;
using System;

namespace ProSMan.Backend.Infrastructure
{
    public class ProSManContext: DbContext
    {
		public ProSManContext(DbContextOptions<ProSManContext> options)
			: base(options)
		{ }

		public DbSet<Project> Projects { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Sprint> Sprints { get; set; }
		public DbSet<Task> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.AddConfiguration(new ProjectConfiguration());
			builder.AddConfiguration(new CategoryConfiguration());
			builder.AddConfiguration(new SprintConfiguration());
			builder.AddConfiguration(new TaskConfiguration());
		}
	}
}
