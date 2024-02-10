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
		/// </summary>
		public const string RouteTemplateApi = "/api/trails/{trailId}/images";

		public static string RouteTemplateFormat(int t) => RouteTemplateApi.Replace("{trailId}", t.ToString());
	}
}
