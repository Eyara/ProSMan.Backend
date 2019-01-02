using ProSMan.Backend.Core;

namespace ProSMan.Backend.Model
{
    public class Task: IEntityBase<int>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public int ActualSpentTime { get; set; }
		public Priority Priority { get; set; }
		public bool IsFinished { get; set; }

		public int ProjectId { get; set; }
		public int CategoryId { get; set; }
		public int SprintId { get; set; }

		public virtual Project Project { get; set; }
		public virtual Category Category { get; set; }
		public virtual Sprint Sprint { get; set; }

    }

	public enum Priority: int
	{
		Low = 1,
		Medium,
		High
	}
}
