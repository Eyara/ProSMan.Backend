using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface IUser: IEntityBase<string>
    {
		string UserName { get; set; }
		string Fullname { get; set; }
	}
}
