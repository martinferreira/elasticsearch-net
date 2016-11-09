using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchInput>))]
	public interface ISearchInput : IInput
	{
		[JsonProperty("extract")]
		IEnumerable<string> Extract { get; set; }

		[JsonProperty("request")]
		ISearchInputRequest Request { get; set; }
	}

	public class SearchInput : InputBase, ISearchInput
	{
		public IEnumerable<string> Extract { get; set; }

		public ISearchInputRequest Request { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Search = this;
	}

	public class SearchInputDescriptor
		: DescriptorBase<SearchInputDescriptor, ISearchInput>, ISearchInput
	{
		IEnumerable<string> ISearchInput.Extract { get; set; }
		ISearchInputRequest ISearchInput.Request { get; set; }

		public SearchInputDescriptor Request(Func<SearchInputRequestDescriptor, ISearchInputRequest> selector) =>
			Assign(a => a.Request = selector?.InvokeOrDefault(new SearchInputRequestDescriptor()));

		public SearchInputDescriptor Extract(IEnumerable<string> extract) =>
			Assign(a => a.Extract = extract);

		public SearchInputDescriptor Extract(params string[] extract) =>
			Assign(a => a.Extract = extract);
	}
}
