using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Shared.Features.ManageTrails
{
	public record UploadTrailImageRequest(int TrailId, IBrowserFile File)
		: IRequest<UploadTrailImageRequest.Response>
	{
		public record Response(string ImageName);

		/// <summary>
		///     Шаблон маршрута для запроса.
		///     Examlpe use: string.Format(UploadTrailImageRequest.RouteTemplate, TrailId)
		/// </summary>
		public const string RouteTemplate = "/api/trails/{0}/images";
	}
}
