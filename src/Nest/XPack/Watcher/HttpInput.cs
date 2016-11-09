using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInput>))]
	public interface IHttpInput : IInput
	{
		[JsonProperty("extract")]
		IEnumerable<string> Extract { get; set; }

		[JsonProperty("request")]
		IWatcherHttpRequest Request { get; set; }
	}

	public class HttpInput : InputBase, IHttpInput
	{
		public IEnumerable<string> Extract { get; set; }

		public IWatcherHttpRequest Request { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Http = this;
	}

	public class HttpInputDescriptor : DescriptorBase<HttpInputDescriptor, IHttpInput>, IHttpInput
	{
		IEnumerable<string> IHttpInput.Extract { get; set; }
		IWatcherHttpRequest IHttpInput.Request { get; set; }

		public HttpInputDescriptor Request(Func<WatcherHttpRequestDescriptor, IWatcherHttpRequest> httpRequestSelector) =>
			Assign(a => a.Request = httpRequestSelector(new WatcherHttpRequestDescriptor()));

		public HttpInputDescriptor Extract(IEnumerable<string> extract) =>
			Assign(a => a.Extract = extract);

		public HttpInputDescriptor Extract(params string[] extract) =>
			Assign(a => a.Extract = extract);
	}
}
