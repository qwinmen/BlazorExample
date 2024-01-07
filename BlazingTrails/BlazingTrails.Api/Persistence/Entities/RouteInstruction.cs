namespace BlazingTrails.Api.Persistence.Entities
{
	/// <summary>
	///     Точка на маршруте
	/// </summary>
	public class RouteInstruction
	{
		/// <summary>
		///     Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Ссылка на тропу
		/// </summary>
		public int TrailId { get; set; }

		/// <summary>
		///     Номер точки
		/// </summary>
		public int Stage { get; set; }

		/// <summary>
		///     Описание точки
		/// </summary>
		public string Description { get; set; } = default!;

		/// <summary>
		///     Связь точки маршрута с тропой
		/// </summary>
		public Trail Trail { get; set; } = default!;
	}
}
