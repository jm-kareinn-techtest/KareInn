using System;

namespace BeerDirectory.Core.Services.Exceptions
{
	public class ApplicationModelNotFoundException : Exception
	{
		public ApplicationModelNotFoundException() : base("Application model not found.")
		{
		}

		public ApplicationModelNotFoundException(Exception e) : base("Application model not found.", e)
		{
		}
	}
}