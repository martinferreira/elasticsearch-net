using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IInput { }

	public abstract class InputBase : IInput
	{
		public static implicit operator InputContainer(InputBase input)
		{
			return input == null
				? null
				: new InputContainer(input);
		}

		internal abstract void WrapInContainer(IInputContainer container);
	}
}
