using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails
{
	public class AddTrailEndpoint
		: EndpointBaseAsync.WithRequest<AddTrailRequest>.WithActionResult<int>
	{
		private readonly BlazingTrailsContext _database;

		public AddTrailEndpoint(BlazingTrailsContext database)
		{
			_database = database;
		}

		[HttpPost(AddTrailRequest.RouteTemplate)]
		public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = new CancellationToken())
		{
			var trail = new Trail
				{
					Name = request.Trail.Name,
					Description = request.Trail.Description,
					Location = request.Trail.Location,
					TimeInMinutes = request.Trail.TimeInMinutes,
					Length = request.Trail.Length,
				};

			await _database.Trails.AddAsync(trail, cancellationToken);
			var routeInstruction = request.Trail.Route.Select(x => new RouteInstruction
				{
					Stage = x.Stage,
					Description = x.Description,
					Trail = trail,
				});
			await _database.RouteInstructions.AddRangeAsync(routeInstruction, cancellationToken);
			//Сохраняем в базу:
			await _database.SaveChangesAsync(cancellationToken);

			return Ok(trail.Id);
		}
	}
}
