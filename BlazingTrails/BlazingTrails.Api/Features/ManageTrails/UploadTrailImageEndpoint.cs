using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlazingTrails.Api.Features.ManageTrails
{
	/// <summary>
	///     Конечная точка для сохранения файлов изображений
	/// </summary>
	public class UploadTrailImageEndpoint: EndpointBaseAsync.WithRequest<int>.WithActionResult<string>
	{
		private readonly BlazingTrailsContext _database;

		public UploadTrailImageEndpoint(BlazingTrailsContext database)
		{
			_database = database;
		}

		[HttpPost(UploadTrailImageRequest.RouteTemplateApi)]
		public override async Task<ActionResult<string>> HandleAsync(int trailId, CancellationToken cancellationToken = new())
		{
			if (trailId < 1)
				throw new Exception("Идентификатор тропы не указан, либо меньше 1");

			Trail? trail = await _database.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
			if (trail is null)
			{
				return BadRequest("Запись о тропе не найдена");
			}

			IFormFile file = Request.Form.Files[0];
			if (file.Length == 0)
			{
				return BadRequest("Изображение не найдено!");
			}

			string filename = $"{Guid.NewGuid()}.jpg";
			string saveLocation = Path.Combine(Directory.GetCurrentDirectory(), BlazingTrailsApiConsts.ImageStaticDirectory, filename);

			//Меняем размеры загружаемого изображения на свои:
			var resizeOptions = new ResizeOptions
				{
					Mode = ResizeMode.Pad,
					Size = new Size(640, 426),
				};

			using (Image image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken))
			{
				image.Mutate(x => x.Resize(resizeOptions));
				await image.SaveAsJpegAsync(saveLocation, cancellationToken);
			}

			trail.Image = filename;
			await _database.SaveChangesAsync(cancellationToken);

			return Ok(trail.Image);
		}
	}
}
