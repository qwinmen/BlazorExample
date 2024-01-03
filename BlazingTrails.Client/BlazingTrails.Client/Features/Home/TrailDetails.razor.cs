using Microsoft.AspNetCore.Components;

namespace BlazingTrails.Client.Features.Home
{
	//Допустимы два варианта написания кода, через @code в файле компонента .razor,
	//и второй - созданием копии именования компонента с окончанием .cs
	public partial class TrailDetails
	{
		private bool _isOpen;
		private Trail? _activeTrail;

		[Parameter, EditorRequired]
		public Trail? Trail { get; set; }

		//Выполняется каждый раз, при изменении параметров компонента
		protected override void OnParametersSet()
		{
			if (Trail != null)
			{
				_activeTrail = Trail;
				_isOpen = true;
			}
		}

		private void Close()
		{
			_activeTrail = null;
			_isOpen = false;
		}
	}
}
