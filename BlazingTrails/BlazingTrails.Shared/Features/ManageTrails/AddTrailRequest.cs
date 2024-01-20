using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails
{
	/// <summary>
	///     record тип считается лучшим для обьектов передачи данных (Dto), т.к. неизменяются
	/// </summary>
	/// <param name="Trail"></param>
	public record AddTrailRequest(TrailDto Trail): IRequest<AddTrailRequest.Response>
	{
		/// <summary>
		///     Данные ответа на запрос
		/// </summary>
		/// <param name="TrailId"></param>
		public record Response(int TrailId);

		/// <summary>
		///     Адрес конечной точки (end-point) для API запроса
		/// </summary>
		public const string RouteTemplate = "/api/trails";
	}

	/// <summary>
	///     Валидатор запроса
	/// </summary>
	public class AddTrailRequestValidator: AbstractValidator<AddTrailRequest>
	{
		public AddTrailRequestValidator()
		{
			//Можно применить правила валидации, написанные ранее, через SetValidator:
			RuleFor(x => x.Trail).SetValidator(new TrailValidator());
		}
	}
}
