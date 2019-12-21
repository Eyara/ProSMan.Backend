﻿using System;

namespace ProSMan.Backend.Model
{
	public class BacklogTask
	{
		public BacklogTask()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Guid ProjectId { get; set; }

		public virtual Project Project { get; set; }
	}
}
