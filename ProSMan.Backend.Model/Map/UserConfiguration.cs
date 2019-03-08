using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSMan.Backend.Core.Extensions;

namespace ProSMan.Backend.Model
{
	public class UserConfiguration : DbEntityConfiguration<User>
	{
		public override void Configure(EntityTypeBuilder<User> entity)
		{
		}
	}
}
