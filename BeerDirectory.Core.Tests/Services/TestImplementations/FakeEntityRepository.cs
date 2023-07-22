using System;
using System.Collections.Generic;
using System.Linq;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Exceptions;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Repositories.Interfaces.Filters;

namespace BeerDirectory.Core.Tests.Services.TestImplementations
{
	public class FakeEntityRepository<T> : IEntityRepository<T, IEntityFilter<T>>
		where T : Entity
	{
		protected readonly List<T> _entities;
		private readonly List<DateTime> _saveTimestamps;
		public IEntityFilter<T> LastFilterUsed { get; set; }
		public DateTime LastSaveTimestamp => _saveTimestamps.First();

		public FakeEntityRepository(List<T> entities, List<DateTime> saveTimestamps)
		{
			_entities = entities;
			_saveTimestamps = saveTimestamps;
		}
		
		public T GetById(Guid id)
		{
			var ent = _entities.FirstOrDefault(x => x.Id == id);
			if (ent == default(T))
				throw new EntityNotFoundException();
			return ent;
		}

		public IEnumerable<T> Get(IEntityFilter<T> filter)
		{
			LastFilterUsed = filter;
			IEnumerable<T> ents = _entities.ToList();
			ents = ents.Skip(filter.Skip);

			if (filter.Take > 0)
				ents = ents.Take(filter.Take);

			return ents.ToList();
		}

		public void Add(T entity)
		{
			_entities.Add(entity);
		}

		public void SaveChanges()
		{
			_entities.ForEach(SetIdOnSave);
			_saveTimestamps.Add(DateTime.Now);
		}

		public IEntityFilter<T> GetFilter()
		{
			throw new NotImplementedException();
		}

		public long GetCount()
		{
			return _entities.Count;
		}
		
		private static void SetIdOnSave(Entity entity)
		{
			if (entity.Id != Guid.Empty)
				return;
			entity.InitialiseIdentity(Guid.NewGuid());
		}
	}
}