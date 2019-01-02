using ProSMan.Backend.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Model
{
    public class Project: IEntityBase<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Category> Categories { get; set; }
		public virtual ICollection<Sprint> Sprints { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }
	}
}
