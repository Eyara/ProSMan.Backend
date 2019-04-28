using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSMan.Backend.Core.Extensions;
using System;

namespace ProSMan.Backend.Model
{
	public class TaskConfiguration : DbEntityConfiguration<Task>
	{
		public override void Configure(EntityTypeBuilder<Task> entity)
		{
			entity.ToTable("Tasks");
			entity.HasKey(x => x.Id);

			entity.HasOne(x => x.Project)
				.WithMany(x => x.Tasks)
				.HasForeignKey(x => x.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(x => x.Category)
				.WithMany(x => x.Tasks)
				.HasForeignKey(x => x.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(x => x.Sprint)
				.WithMany(x => x.Tasks)
				.HasForeignKey(x => x.SprintId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
