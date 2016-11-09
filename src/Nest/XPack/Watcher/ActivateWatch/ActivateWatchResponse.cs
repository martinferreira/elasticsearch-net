using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IActivateWatchResponse : IResponse
	{
		ActivateWatchStatus Status { get; }
	}

	public class ActivateWatchResponse : ResponseBase, IActivateWatchResponse
	{
		[JsonProperty("_status")]
		public ActivateWatchStatus Status { get; internal set; }
	}

	[JsonObject]
	public class ActivateWatchStatus
	{
		[JsonProperty("state")]
		public WatchState State { get; internal set; }

		[JsonProperty("actions")]
		public Dictionary<string, ActionStatus> Actions { get; set; }
	}
}
