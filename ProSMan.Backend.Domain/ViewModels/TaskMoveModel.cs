using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class TaskMoveModel
    {
		public Guid Id { get; set; }
		public Guid SprintId { get; set; }
		public Guid CategoryId { get; set; }
    }
}
