using ProSMan.Backend.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Model
{
    public class Sprint: IEntityBase<int>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public Boolean IsFinished { get; set; }

		public int ProjectId { get; set; }

		public virtual Project Project { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
    }
}
