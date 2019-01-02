using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Domain.ViewModels
{
	public class TaskViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public int ActualSpentTime { get; set; }
		public string Priority { get; set; }
		public bool IsFinished { get; set; }

		public int ProjectId { get; set; }
		public int CategoryId { get; set; }
		public int SprintId { get; set; }
	}

	public enum Priority : int
	{
		Low = 1,
		Medium,
		High
	}
}
