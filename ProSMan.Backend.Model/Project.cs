using ProSMan.Backend.Core;
using ProSMan.Backend.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace ProSMan.Backend.Model
{
	public class Project : IEntityBase<Guid>, IProject
	{
		public Project()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public Boolean IsDeleted { get; set; }

		public string UserId { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Category> Categories { get; set; }
		public virtual ICollection<Sprint> Sprints { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }
		public virtual ICollection<NonSprintTask> NonSprintTasks { get; set; }
	}
}
