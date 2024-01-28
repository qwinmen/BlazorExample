using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails
{
	public class UploadTrailImageHandler: IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
	{
		private readonly HttpClient _httpClient;

		public UploadTrailImageHandler(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
		{
			Stream fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);
			using (var content = new MultipartFormDataContent())
			{
				content.Add(new StreamContent(fileContent), "image", request.File.Name);
				HttpResponseMessage response = await _httpClient.PostAsync(string.Format(UploadTrailImageRequest.RouteTemplate, request.TrailId), content,
					cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					string fileName = await response.Content.ReadAsStringAsync(cancellationToken);
					return new UploadTrailImageRequest.Response(fileName);
				}

				return new UploadTrailImageRequest.Response("");
			}
		}
	}
}
