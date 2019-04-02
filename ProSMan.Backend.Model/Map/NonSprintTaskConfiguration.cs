using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSMan.Backend.Core.Extensions;

namespace ProSMan.Backend.Model.Map
{
	class NonSprintTaskConfiguration : DbEntityConfiguration<NonSprintTask>
	{
		public override void Configure(EntityTypeBuilder<NonSprintTask> entity)
		{
			entity.ToTable("NonSprintTasks");
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Id)
				.IsRequired()
				.ValueGeneratedNever();

			entity.HasOne(x => x.Project)
				.WithMany(x => x.NonSprintTasks)
				.HasForeignKey(x => x.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
