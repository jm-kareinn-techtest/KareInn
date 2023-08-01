# Beer Directory

## Quickstart
Before starting, be sure to update npm from the `beer-directory\ClientApp` directory: `npm ci && npm run build`

Before running tests, either start the docker version of the db server from the root directory with `docker compose up`, or ensure the connection string found in `BeerDirectory.Core.Tests\Repositories\BeerRepositoryTests.cs` is accurate for your environment.

## Notes
The Angular app uses a naive state management system and would ideally be better off using something like the redux pattern and ngRx if this were a more complex application.

The text-based search encountered errors missing a text index in the database for use, and so had the filter temporarily removed. To enable this, please add a text index on the Beer collection in Mongo, and uncomment line 36 in `BeerDirectory.Core.Repositories.Filters.BeerFilter`

Ideally, I'd swap out the manual db docker or hard connection string for use of the `Ductus.FluentDocker` nuget package, and have it create a database for use in testing, and tear down afterwards, removing the left over information between tests. Not using this is simply a time constraint.