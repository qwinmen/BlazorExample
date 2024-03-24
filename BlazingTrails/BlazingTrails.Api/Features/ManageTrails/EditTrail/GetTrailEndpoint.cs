using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail
{
	public class GetTrailEndpoint: EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailRequest.Response>
	{
		private readonly BlazingTrailsContext _context;

		public GetTrailEndpoint(BlazingTrailsContext context)
		{
			_context = context;
		}

		[HttpGet(GetTrailRequest.RouteTemplate)]
		public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId,
			CancellationToken cancellationToken = new CancellationToken())
		{
			Trail? trail = await _context.Trails.Include(x => x.Route)
				.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
			if (trail is null)
			{
				return BadRequest($"Тропа id:{trailId} не найдена!");
			}

			GetTrailRequest.Response response = new GetTrailRequest.Response(new GetTrailRequest.Trail(trail.Id,
				trail.Name, trail.Location, trail.Image, trail.TimeInMinutes, trail.Length,
				trail.Description, trail.Route.Select(ri => new GetTrailRequest.RouteInstruction(ri.Id,
					ri.Stage, ri.Description))
			));

			return Ok(response);
		}
	}
}
