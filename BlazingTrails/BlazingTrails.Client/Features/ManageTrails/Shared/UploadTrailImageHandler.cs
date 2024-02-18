using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.Shared
{
	public class UploadTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
	{
		private readonly HttpClient _httpClient;

		public UploadTrailImageHandler(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
		{
			if (request.TrailId < 1)
				throw new Exception(UploadTrailImageRequest.RouteTemplateFormat(request.TrailId));

			Stream fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);
			using (var content = new MultipartFormDataContent())
			{
				content.Add(new StreamContent(fileContent), "image", request.File.Name);
				HttpResponseMessage response = await _httpClient.PostAsync(
					UploadTrailImageRequest.RouteTemplateFormat(request.TrailId),
					content,
					cancellationToken);
				if (response.IsSuccessStatusCode)
				{
					string fileName = await response.Content.ReadAsStringAsync(cancellationToken);
					return new UploadTrailImageRequest.Response(fileName);
				}

				throw new Exception(response.StatusCode.ToString());
			}
		}
	}
}
