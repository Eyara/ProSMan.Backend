using ProSMan.Backend.Core;
using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Model
{
	public class Category : IEntityBase<Guid>, ICategory
	{
		public Category()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Boolean IsDeleted { get; set; }

		public Guid ProjectId { get; set; }

		public virtual Project Project { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
	}
}
