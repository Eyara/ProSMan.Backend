using Microsoft.EntityFrameworkCore;
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
		}
	}
}
