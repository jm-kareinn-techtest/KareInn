using System;

namespace BeerDirectory.Core.Repositories.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException() : base("Entity does not exist.")
		{
		}

		public EntityNotFoundException(Exception e) : base("Entity does not exist.", e)
		{
		}
	}
}