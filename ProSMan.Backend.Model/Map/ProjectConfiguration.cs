using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSMan.Backend.Core.Extensions;

namespace ProSMan.Backend.Model
{
	public class ProjectConfiguration : DbEntityConfiguration<Project>
	{
		public override void Configure(EntityTypeBuilder<Project> entity)
		{
			entity.ToTable("Projects");
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Id)
				.IsRequired()
				.ValueGeneratedNever();

			entity.HasMany(x => x.Categories)
				.WithOne(y => y.Project);

			entity.HasMany(x => x.Sprints)
				.WithOne(y => y.Project);

			entity.HasMany(x => x.Tasks)
				.WithOne(y => y.Project);

			entity.HasOne(x => x.User)
				.WithMany(y => y.Projects);
		}
	}
}
