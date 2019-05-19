using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface ICategory
    {
		Guid Id { get; set; }
		string Name { get; set; }
		Guid ProjectId { get; set; }
	}
}
