using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails
{
	public class TrailDto
	{
		public class RouteInstruction
		{
			public int Stage { get; set; }
			public string Description { get; set; } = "";
		}

		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public string Location { get; set; } = "";
		public int TimeInMinutes { get; set; }
		public int Length { get; set; }

		public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();
	}

	/// <summary>
	///     Определены правила валидации на основе FluentValidation пакета
	/// </summary>
	public class TrailValidator: AbstractValidator<TrailDto>
	{
		public TrailValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Имя не заполнено!");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Описание не заполнено!");
			RuleFor(x => x.Location).NotEmpty().WithMessage("Местонахождение (локация) не заполнено!");
			RuleFor(x => x.Length).GreaterThan(0).WithMessage("Укажите длину маршрута!");
			RuleFor(x => x.Route).NotEmpty().WithMessage("Укажите одну и более дорожную инструкцию!");
		}
	}
}
