using ProSMan.Backend.Core.Enums;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class TaskMoveModel
    {
		public Guid Id { get; set; }
		public int TimeEstimate { get; set; }
		public Priority Priority { get; set; }
		public Guid SprintId { get; set; }
		public Guid CategoryId { get; set; }
    }
}
