using ProSMan.Backend.Core;
using System;

namespace ProSMan.Backend.Model
{
	public class Task : IEntityBase<Guid>
	{
		public Task()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public int ActualSpentTime { get; set; }
		public Priority Priority { get; set; }
		public bool IsFinished { get; set; }

		public Guid ProjectId { get; set; }
		public Guid? CategoryId { get; set; }
		public Guid SprintId { get; set; }

		public virtual Project Project { get; set; }
		public virtual Category Category { get; set; }
		public virtual Sprint Sprint { get; set; }

	}

	public enum Priority : int
	{
		Low = 1,
		Medium,
		High
	}
}
