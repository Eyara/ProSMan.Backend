using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProSMan.Backend.Core.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProSMan.Backend.Model
{
	public class SprintConfiguration : DbEntityConfiguration<Sprint>
	{
		public override void Configure(EntityTypeBuilder<Sprint> entity)
		{
			entity.ToTable("Sprints");
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Id)
				.IsRequired()
				.ValueGeneratedNever();

			entity.HasOne(x => x.Project)
				.WithMany(x => x.Sprints)
				.HasForeignKey(x => x.ProjectId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasMany(x => x.Tasks)
				.WithOne(y => y.Sprint);

		}
	}
}
