using System;
using BeerDirectory.Core.Domain.Exceptions;

namespace BeerDirectory.Core.Domain.Entities
{
	public class Entity
	{
		public Guid Id { get; set; }

		public void InitialiseIdentity(Guid id)
		{
			if (!Id.Equals(Guid.Empty))
				throw new EntityAlreadyInitialisedException();
			Id = id;
		}
	}
}