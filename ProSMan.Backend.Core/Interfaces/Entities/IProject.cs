using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface IProject
    {
		Guid Id { get; set; }
		string Name { get; set; }
	}
}
