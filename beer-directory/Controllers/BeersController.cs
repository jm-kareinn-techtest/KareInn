using BeerDirectory.Core.Services.Filters;
using BeerDirectory.Core.Services.Interfaces;
using BeerDirectory.Core.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beer_directory.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class BeersController : ControllerBase
{
	private readonly IBeerService _service;

	public BeersController(IBeerService service)
	{
		_service = service;
	}
		
	[HttpGet]
	public List<BeerModel> Get([FromQuery] BeerModelFilter filter)
	{
		var models = _service.Get(filter);
		return models;
	}

	[HttpPost]
	public ActionResult<BeerModel> Post(BeerModel model)
	{
		var result = _service.Add(model);
		return Ok(result);
	}
}