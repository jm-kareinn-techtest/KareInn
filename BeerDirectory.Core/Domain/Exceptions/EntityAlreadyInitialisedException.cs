using System;

namespace BeerDirectory.Core.Domain.Exceptions
{
	public class EntityAlreadyInitialisedException : Exception
	{
		public EntityAlreadyInitialisedException() : base("Id has already been initialised for this Entity.")
		{
			
		}
	}
}