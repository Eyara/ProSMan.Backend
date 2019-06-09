using ProSMan.Backend.Core;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Model
{
	public class Sprint : IEntityBase<Guid>
	{
		public Sprint()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime? FinishedOn { get; set; }
		public Boolean IsFinished { get; set; }
		public Boolean IsDeleted { get; set; }

		public Guid ProjectId { get; set; }

		public Project Project { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
	}
}
