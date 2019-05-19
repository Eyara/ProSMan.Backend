using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface ISprint
    {
		Guid Id { get; set; }
		string Name { get; set; }
		DateTime FromDate { get; set; }
		DateTime ToDate { get; set; }
		bool IsFinished { get; set; }
	}
}
