using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IWatcherAuthentication
	{
		[JsonProperty("basic")]
		IWatcherBasicAuthentication Basic { get; set; }
	}

	public class WatcherAuthentication : IWatcherAuthentication
	{
		public IWatcherBasicAuthentication Basic { get; set; }
	}

	public class WatcherAuthenticationDescriptor
		: DescriptorBase<WatcherAuthenticationDescriptor, IWatcherAuthentication>, IWatcherAuthentication
	{
		IWatcherBasicAuthentication IWatcherAuthentication.Basic { get; set; }

		public WatcherAuthenticationDescriptor Basic(Func<WatcherBasicAuthenticationDescriptor, IWatcherBasicAuthentication> selector) =>
			Assign(a => a.Basic = selector.Invoke(new WatcherBasicAuthenticationDescriptor()));
	}

	[JsonObject]
	public interface IWatcherBasicAuthentication
	{
		[JsonProperty("username")]
		string Username { get; set; }

		[JsonProperty("password")]
		string Password { get; set; }
	}

	[JsonObject]
	public class WatcherBasicAuthentication : IWatcherBasicAuthentication
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class WatcherBasicAuthenticationDescriptor
		: DescriptorBase<WatcherBasicAuthenticationDescriptor, IWatcherBasicAuthentication>, IWatcherBasicAuthentication
	{
		string IWatcherBasicAuthentication.Username { get; set; }
		string IWatcherBasicAuthentication.Password { get; set; }

		public WatcherBasicAuthenticationDescriptor Username(string username) =>
			Assign(a => a.Username = username);

		public WatcherBasicAuthenticationDescriptor Password(string password) =>
			Assign(a => a.Username = password);
	}
}
