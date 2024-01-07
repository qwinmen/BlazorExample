namespace BlazingTrails.Api.Persistence.Entities
{
	/// <summary>
	///     Тропа, маршрут
	/// </summary>
	public class Trail
	{
		/// <summary>
		///     Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Название маршрута
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		///     Описание
		/// </summary>
		public string Description { get; set; } = default!;

		/// <summary>
		///     Фото
		/// </summary>
		public string? Image { get; set; }

		/// <summary>
		///     Расположение
		/// </summary>
		public string Location { get; set; } = default!;

		/// <summary>
		///     Время прохождения
		/// </summary>
		public int TimeInMinutes { get; set; }

		/// <summary>
		///     Протяженность
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		///     Список точек на маршруте
		/// </summary>
		public ICollection<RouteInstruction> Route { get; set; } = default!;
	}
}
