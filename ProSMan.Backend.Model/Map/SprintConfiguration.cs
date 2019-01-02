using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProSMan.Backend.Core.Extensions;

namespace ProSMan.Backend.Model
{
	public class SprintConfiguration : DbEntityConfiguration<Sprint>
	{
		public override void Configure(EntityTypeBuilder<Sprint> entity)
		{
			entity.ToTable("Sprints");
			entity.HasKey(x => x.Id);

			entity.HasOne(x => x.Project)
				.WithMany(x => x.Sprints)
				.HasForeignKey(x => x.ProjectId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
		}
	}
}
