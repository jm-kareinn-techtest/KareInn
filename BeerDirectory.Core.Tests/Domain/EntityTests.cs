using System;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Domain.Exceptions;
using Xunit;

namespace BeerDirectory.Core.Tests.Domain
{
	public abstract class EntityTests<T> where T : Entity
	{
		protected abstract T GetNewEntity();
		
		[Fact]
		public void InitialiseIdentity_throws_if_Id_already_initialised()
		{
			// arrange
			var ent = GetNewEntity();
			ent.InitialiseIdentity(Guid.NewGuid());
			
			// act/assert
			Assert.Throws<EntityAlreadyInitialisedException>(() => ent.InitialiseIdentity(Guid.NewGuid()));
		}
		
		[Fact]
		public void InitialiseIdentity_sets_Id_if_not_already_initialised()
		{
			// arrange
			var ent = GetNewEntity();
			var id = Guid.NewGuid();
			
			// act
			ent.InitialiseIdentity(id);
			
			// assert
			Assert.Equal(id, ent.Id);
		}
	}
}