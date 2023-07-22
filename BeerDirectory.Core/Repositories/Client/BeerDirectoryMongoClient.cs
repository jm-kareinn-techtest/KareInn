using System;
using BeerDirectory.Core.Domain.Entities;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories.Client
{
	public class BeerDirectoryMongoClient
	{
		public readonly IMongoClient Client;
		private readonly IMongoDatabase _database;
		
		public string DatabaseName { get; }
		
		public IMongoCollection<Beer> Beers { get; }

		public BeerDirectoryMongoClient(string connectionString, string dbName)
		{
			MongoDefaults.MaxConnectionIdleTime = TimeSpan.FromMinutes(1);
			MongoDefaults.MaxConnectionLifeTime = TimeSpan.FromMinutes(1);
			
			Client = new MongoClient(connectionString);
			DatabaseName = dbName;
			_database = Client.GetDatabase(dbName);

			RegisterMappings();

			Beers = _database.GetCollection<Beer>(nameof(Beer));
		}

		private static void RegisterMappings()
		{
			
		}
		
		public IMongoCollection<T> Set<T>()
		{
			return _database.GetCollection<T>(typeof(T).Name);
		}
	}
}