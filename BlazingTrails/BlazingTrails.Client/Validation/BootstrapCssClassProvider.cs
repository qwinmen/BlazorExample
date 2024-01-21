using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Validation
{
	/// <summary>
	///     Переключение css правил стилизации blazor компонент на bootstrap стили
	/// </summary>
	public class BootstrapCssClassProvider: FieldCssClassProvider
	{
		public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
		{
			//Есть ли ошибки в поле
			var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

			if (editContext.IsModified(fieldIdentifier))
			{
				return isValid ? " is-valid" : "is-invalid";
			}

			return isValid ? "" : "is-invalid";
		}
	}
}
