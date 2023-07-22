using System;
using System.Collections.Generic;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Services;
using BeerDirectory.Core.Services.Filters;
using BeerDirectory.Core.Services.Interfaces;
using BeerDirectory.Core.Services.Models;
using BeerDirectory.Core.Tests.Services.TestImplementations;

namespace BeerDirectory.Core.Tests.Services
{
	public class BeerServiceTests : ApplicationModelServiceTests<BeerModel, Beer, BeerModelFilter>
	{
		private readonly IBeerService _service;
		private readonly FakeBeerRepository _repository;
		private readonly List<Beer> _entities;
		private readonly List<DateTime> _saveTimestamps;

		public BeerServiceTests()
		{
			_entities = new List<Beer>();
			_saveTimestamps = new List<DateTime>();
			_repository = new FakeBeerRepository(_entities, _saveTimestamps);
			_service = new BeerService(_repository);
			InitialiseBaseTests(_service, _repository, _entities, _saveTimestamps);
		}
		
		public override Beer GetNewEntity(Guid id)
		{
			return new Beer.Builder().Build();
		}
	}
}