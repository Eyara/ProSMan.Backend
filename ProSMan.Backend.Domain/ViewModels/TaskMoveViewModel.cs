﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class TaskMoveModel
    {
		public Guid Id { get; set; }
		public Guid SprintId { get; set; }
		public Guid CategoryId { get; set; }
    }
}
