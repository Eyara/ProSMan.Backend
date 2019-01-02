using ProSMan.Backend.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Model
{
    public class Category: IEntityBase<int>
    {
		public int Id { get; set; }
		public string Name { get; set; }

		public int ProjectId { get; set; }

		public virtual Project Project { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
    }
}
