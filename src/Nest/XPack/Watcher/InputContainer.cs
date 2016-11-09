using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<InputContainer>))]
	public interface IInputContainer
	{
		[JsonProperty("http")]
		IHttpInput Http { get; set; }

		[JsonProperty("search")]
		ISearchInput Search { get; set; }

		[JsonProperty("simple")]
		ISimpleInput Simple { get; set; }
	}

	[JsonObject]
	public class InputContainer : IInputContainer
	{
		public InputContainer(InputBase input)
		{
			if (input == null) return;

			input.WrapInContainer(this);
		}

		IHttpInput IInputContainer.Http { get; set; }
		ISearchInput IInputContainer.Search { get; set; }
		ISimpleInput IInputContainer.Simple { get; set; }
	}

	public class InputDescriptor : DescriptorBase<InputDescriptor, IInputContainer>, IInputContainer
	{
		IHttpInput IInputContainer.Http { get; set; }
		ISearchInput IInputContainer.Search { get; set; }
		ISimpleInput IInputContainer.Simple { get; set; }

		public InputDescriptor Search(Func<SearchInputDescriptor, ISearchInput> selector) =>
			Assign(a => a.Search = selector.Invoke(new SearchInputDescriptor()));

		public InputDescriptor Http(Func<HttpInputDescriptor, IHttpInput> selector) =>
			Assign(a => a.Http = selector.Invoke(new HttpInputDescriptor()));

		public InputDescriptor Simple(Func<SimpleInputDescriptor, ISimpleInput> selector) =>
			Assign(a => a.Simple = selector.Invoke(new SimpleInputDescriptor()));
	}
}
