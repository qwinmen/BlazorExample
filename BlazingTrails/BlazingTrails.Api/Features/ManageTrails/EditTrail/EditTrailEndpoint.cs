using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail
{
	public class EditTrailEndpoint: EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<bool>
	{
		private readonly BlazingTrailsContext _context;

		public EditTrailEndpoint(BlazingTrailsContext context)
		{
			_context = context;
		}

		[HttpPut(EditTrailRequest.RouteTemplate)]
		public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = new CancellationToken())
		{
			Trail? trail = await _context.Trails.Include(x => x.Route)
				.SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken);
			if (trail is null)
			{
				return BadRequest($"Тропа id:{request.Trail.Id} не найдена!");
			}

			trail.Name = request.Trail.Name;
			trail.Description = request.Trail.Description;
			trail.Location = request.Trail.Location;
			trail.TimeInMinutes = request.Trail.TimeInMinutes;
			trail.Length = request.Trail.Length;
			trail.Route = request.Trail.Route.Select(ri => new RouteInstruction
				{
					Stage = ri.Stage,
					Description = ri.Description,
					Trail = trail,
				}).ToList();

			if (request.Trail.ImageAction == ImageAction.Remove)
			{
				System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
				trail.Image = null;
			}

			await _context.SaveChangesAsync(cancellationToken);
			return Ok(true);
		}
	}
}
