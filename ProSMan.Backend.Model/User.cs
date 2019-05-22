using Microsoft.AspNetCore.Identity;
using ProSMan.Backend.Core;
using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Model
{
	public class User : IdentityUser, IEntityBase<string>, IUser
	{
		public User()
		{
			SecurityStamp = Guid.NewGuid().ToString();
		}

		public string Fullname { get; set; }

		public virtual ICollection<Project> Projects { get; set; }
	}
}
