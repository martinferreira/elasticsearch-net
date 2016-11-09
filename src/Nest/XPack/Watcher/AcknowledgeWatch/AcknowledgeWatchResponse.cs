using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAcknowledgeWatchResponse : IResponse
	{
		[JsonProperty("_status")]
		WatchStatus Status { get; }
	}

	public class AcknowledgeWatchResponse : ResponseBase, IAcknowledgeWatchResponse
	{
		public WatchStatus Status { get; internal set; }
	}

	[JsonObject]
	public class WatchStatus
	{
		[JsonProperty("version")]
		public int? Version { get; set; }

		[JsonProperty("actions")]
		public Dictionary<string, ActionStatus> Actions { get; set; }
	}

	public class ActionStatus
	{
		[JsonProperty("ack")]
		public WatchState Acknowledgement { get; set; }
	}

	[JsonObject]
	public class WatchState
	{
		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }
	}
}
