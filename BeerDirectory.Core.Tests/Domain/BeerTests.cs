using System;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Domain.Enumerations;
using Xunit;

namespace BeerDirectory.Core.Tests.Domain
{
	public class BeerTests : EntityTests<Beer>
	{
		protected override Beer GetNewEntity()
		{
			return new Beer.Builder().Build();
		}

		[Fact]
		public void SetName_throws_ArgumentException_if_value_is_null_or_whitespace()
		{
			// arrange
			var ent = GetNewEntity();
			
			// act/assert
			Assert.Throws<ArgumentException>(() => ent.SetName(null));
			Assert.Throws<ArgumentException>(() => ent.SetName(""));
		}

		[Fact]
		public void SetName_sets_Name_to_value()
		{
			// arrange
			var ent = GetNewEntity();
			var name = Guid.NewGuid().ToString();

			// act
			ent.SetName(name);
			
			// assert
			Assert.Equal(name, ent.Name);
		}

		[Fact]
		public void SetStyle_throws_ArgumentException_if_value_is_null()
		{
			// arrange
			var ent = GetNewEntity();
			
			// act/assert
			Assert.Throws<ArgumentException>(() => ent.SetStyle(null));
		}

		[Fact]
		public void SetStyle_sets_Style_to_value()
		{
			// arrange
			var ent = GetNewEntity();
			var style = Style.Porter;
			
			// act
			ent.SetStyle(style);
			
			// assert
			Assert.Equal(style, ent.Style);
		}

		[Fact]
		public void SetBrewer_throws_ArgumentException_if_value_is_null_or_whitespace()
		{
			// arrange
			var ent = GetNewEntity();
			
			// act/assert
			Assert.Throws<ArgumentException>(() => ent.SetBrewer(null));
			Assert.Throws<ArgumentException>(() => ent.SetBrewer(""));
		}

		[Fact]
		public void SetBrewer_sets_Brewer_to_value()
		{
			// arrange
			var ent = GetNewEntity();
			var brewer = Guid.NewGuid().ToString();
			
			// act
			ent.SetBrewer(brewer);
			
			// assert
			Assert.Equal(brewer, ent.Brewer);
		}

		[Fact]
		public void SetAlcoholByVolume_throws_ArgumentException_if_value_less_than_0()
		{
			// arrange
			var ent = GetNewEntity();
			
			// act/assert
			Assert.Throws<ArgumentException>(() => ent.SetAlcoholByVolume(-1));
		}
		
		[Fact]
		public void SetAlcoholByVolume_sets_AlcoholByVolume_to_value()
		{
			// arrange
			var ent = GetNewEntity();
			const double alcohol = 3.6;
			
			// act
			ent.SetAlcoholByVolume(alcohol);
			
			// assert
			Assert.Equal(alcohol, ent.AlcoholByVolume);
		}
	}
}