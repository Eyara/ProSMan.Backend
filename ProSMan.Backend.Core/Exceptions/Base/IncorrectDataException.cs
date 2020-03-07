using ProSMan.Backend.Core.Constants;
using System;
namespace ProSMan.Backend.Core.Exceptions.Base
{
	public class IncorrectDataException : Exception
	{
		public IncorrectDataException(string message = ExceptionConstants.IncorrectDataMessage) : base(message) { }
	}
}
