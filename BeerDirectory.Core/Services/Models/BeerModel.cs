using System;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Domain.Enumerations;
using BeerDirectory.Core.Services.Interfaces;

namespace BeerDirectory.Core.Services.Models
{
	public class BeerModel : IApplicationServiceModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Style Style { get; set; }
		public string Brewer { get; set; }
		public double AlcoholByVolume { get; set; }

		public static explicit operator BeerModel(Beer entity)
		{
			return new BeerModel
			{
				Id = entity.Id,
				Name = entity.Name,
				Style = entity.Style,
				Brewer = entity.Brewer,
				AlcoholByVolume = entity.AlcoholByVolume
			};
		}
	}
}