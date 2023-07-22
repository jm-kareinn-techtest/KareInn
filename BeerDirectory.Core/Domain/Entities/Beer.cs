using System;
using BeerDirectory.Core.Domain.Enumerations;

namespace BeerDirectory.Core.Domain.Entities
{
	public class Beer : Entity
	{
		public string Name { get; private set; }
		public Style Style { get; private set; }
		public string Brewer { get; private set; }
		public double AlcoholByVolume { get; private set; }

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name can not be null or whitespace.");
			Name = name;
		}

		public void SetStyle(Style style)
		{
			Style = style ?? throw new ArgumentException("Style can not be null.");
		}

		public void SetBrewer(string brewer)
		{
			if (string.IsNullOrWhiteSpace(brewer))
				throw new ArgumentException("Brewer can not be null or whitespace.");
			Brewer = brewer;
		}

		public void SetAlcoholByVolume(double alcohol)
		{
			if (alcohol < 0)
				throw new ArgumentException("Alcohol can not be less than 0.");
			AlcoholByVolume = alcohol;
		}

		public class Builder
		{
			private readonly Beer _beer;

			public Builder()
			{
				_beer = new Beer();
			}

			public Builder WithId(Guid id)
			{
				_beer.InitialiseIdentity(id);
				return this;
			}

			public Builder WithName(string name)
			{
				_beer.Name = name;
				return this;
			}

			public Builder WithStyle(Style style)
			{
				_beer.Style = style;
				return this;
			}

			public Builder WithBrewer(string brewer)
			{
				_beer.Brewer = brewer;
				return this;
			}

			public Builder WithAlcohol(double alcohol)
			{
				_beer.AlcoholByVolume = alcohol;
				return this;
			}

			public Beer Build()
			{
				return _beer;
			}
		}
	}
}