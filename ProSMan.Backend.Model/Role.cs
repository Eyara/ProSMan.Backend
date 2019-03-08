using Microsoft.AspNetCore.Identity;
using ProSMan.Backend.Core;
using System;

namespace ProSMan.Backend.Model
{
	public class Role : IdentityRole, IEntityBase<string>
	{
		public Role()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
