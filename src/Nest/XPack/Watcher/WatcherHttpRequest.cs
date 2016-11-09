using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<WatcherHttpRequest>))]
	public interface IWatcherHttpRequest
	{
		/// <summary>
		/// The url scheme
		/// </summary>
		[JsonProperty("scheme")]
		[JsonConverter(typeof(StringEnumConverter))]
		ConnectionScheme? Scheme { get; set; }

		/// <summary>
		/// The port that the http service is listening on.
		/// This is required
		/// </summary>
		[JsonProperty("port")]
		int Port { get; set; }

		/// <summary>
		/// The host to connect to. This is required
		/// </summary>
		[JsonProperty("host")]
		string Host { get; set; }

		/// <summary>
		/// The url path. The path can be static text or contain mustache templates.
		/// Url query string parameters must be specified with <see cref="Params"/>
		/// </summary>
		[JsonProperty("path")]
		string Path { get; set; }

		/// <summary>
		/// The HTTP method. Defaults to <see cref="HttpMethod.Get"/>
		/// </summary>
		[JsonProperty("method")]
		[JsonConverter(typeof(StringEnumConverter))]
		HttpMethod? Method { get; set; }

		/// <summary>
		/// The HTTP request headers.
		/// The header values can be static text or include mustache templates.
		/// </summary>
		[JsonProperty("headers")]
		IDictionary<string, string> Headers { get; set; }

		/// <summary>
		/// The url query string parameters.
		/// The parameter values can be static text or contain mustache templates.
	    /// </summary>
		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		/// <summary>
		/// Sets the scheme, host, port and params all at once by specifying a real URL.
		/// May not be combined with <see cref="Scheme"/>, <see cref="Host"/>,
		/// <see cref="Port"/> and <see cref="Params"/>.
		/// As if parameters are set, specifying them individually might overwrite them.
		/// </summary>
		[JsonProperty("url")]
		string Url { get; set; }

		/// <summary>
		/// Authentication related HTTP headers.
		/// </summary>
		[JsonProperty("auth")]
		IWatcherAuthentication Authentication { get; set; }

		/// <summary>
		/// The proxy to use when connecting to the host.
		/// </summary>
		[JsonProperty("proxy")]
		IWatcherProxy Proxy { get; set; }

		/// <summary>
		/// The timeout for setting up the http connection.
		/// If the connection could not be set up within this time,
		/// the input will timeout and fail.
		/// </summary>
		[JsonProperty("connection_timeout")]
		Time ConnectionTimeout { get; set; }

		/// <summary>
		/// The timeout for reading data from http connection.
		/// If no response was received within this time,
		/// the input will timeout and fail.
		/// </summary>
		[JsonProperty("read_timeout")]
		Time ReadTimeout { get; set; }

		/// <summary>
		/// The HTTP request body.
		/// The body can be static text or include mustache templates.
		/// </summary>
		[JsonProperty("body")]
		string Body { get; set; }

		/// <summary>
		/// The expected content type the response body will contain.
		/// If the format is text, <see cref="HttpInput.Extract"/> cannot exist.
		/// Note that this overrides the header that is returned in the HTTP response.
		/// </summary>
		[JsonProperty("response_content_type")]
		ResponseContentType? ResponseContentType { get; set; }
	}

	public class WatcherHttpRequest : IWatcherHttpRequest
	{
		/// <inheritdoc />
		public ConnectionScheme? Scheme { get; set; }

		/// <inheritdoc />
		public int Port { get; set; }

		/// <inheritdoc />
		public string Host { get; set; }

		/// <inheritdoc />
		public string Path { get; set; }

		/// <inheritdoc />
		public HttpMethod? Method { get; set; }

		/// <inheritdoc />
		public IDictionary<string, string> Headers { get; set; }

		/// <inheritdoc />
		public IDictionary<string, object> Params { get; set; }

		/// <inheritdoc />
		public string Url { get; set; }

		/// <inheritdoc />
		public IWatcherAuthentication Authentication { get; set; }

		/// <inheritdoc />
		public IWatcherProxy Proxy { get; set; }

		/// <inheritdoc />
		public Time ConnectionTimeout { get; set; }

		/// <inheritdoc />
		public Time ReadTimeout { get; set; }

		/// <inheritdoc />
		public string Body { get; set; }

		/// <inheritdoc />
		public ResponseContentType? ResponseContentType { get; set; }
	}

	public class WatcherHttpRequestDescriptor
		: DescriptorBase<WatcherHttpRequestDescriptor, IWatcherHttpRequest>, IWatcherHttpRequest
	{
		ConnectionScheme? IWatcherHttpRequest.Scheme { get; set; }
		int IWatcherHttpRequest.Port { get; set; }
		string IWatcherHttpRequest.Host { get; set; }
		string IWatcherHttpRequest.Path { get; set; }
		HttpMethod? IWatcherHttpRequest.Method { get; set; }
		IDictionary<string, string> IWatcherHttpRequest.Headers { get; set; }
		IDictionary<string, object> IWatcherHttpRequest.Params { get; set; }
		IWatcherAuthentication IWatcherHttpRequest.Authentication { get; set; }
		string IWatcherHttpRequest.Body { get; set; }
		string IWatcherHttpRequest.Url { get; set; }
		ResponseContentType? IWatcherHttpRequest.ResponseContentType { get; set; }
		Time IWatcherHttpRequest.ReadTimeout { get; set; }
		Time IWatcherHttpRequest.ConnectionTimeout { get; set; }
		IWatcherProxy IWatcherHttpRequest.Proxy { get; set; }

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Authentication(Func<WatcherAuthenticationDescriptor, IWatcherAuthentication> authSelector) =>
			Assign(a => a.Authentication = authSelector(new WatcherAuthenticationDescriptor()));

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Body(string body) => Assign(a => a.Body = body);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor ConnectionTimeout(Time connectionTimeout) => Assign(a => a.ConnectionTimeout = connectionTimeout);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Headers(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> headersSelector) =>
			Assign(a => a.Headers = headersSelector(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Headers(Dictionary<string, string> headersDictionary) =>
			Assign(a => a.Headers = headersDictionary);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Host(string host) => Assign(a => a.Host = host);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Method(HttpMethod method) => Assign(a => a.Method = method);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Path(string path) => Assign(a => a.Path = path);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Params(Dictionary<string, object> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Port(int port) => Assign(a => a.Port = port);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Proxy(Func<WatcherProxyDescriptor, IWatcherProxy> proxySelector) =>
			Assign(a => a.Proxy = proxySelector.Invoke(new WatcherProxyDescriptor()));

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor ReadTimeout(Time readTimeout) => Assign(a => a.ReadTimeout = readTimeout);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor ResponseContentType(ResponseContentType responseContentType) =>
			Assign(a => a.ResponseContentType = responseContentType);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Scheme(ConnectionScheme scheme) => Assign(a => a.Scheme = scheme);

		/// <inheritdoc />
		public WatcherHttpRequestDescriptor Url(string url) => Assign(a => a.Url = url);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ConnectionScheme
	{
		[EnumMember(Value = "http")]
		Http,
		[EnumMember(Value = "https")]
		Https
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum HttpMethod
	{
		[EnumMember(Value = "head")]
		Head,
		[EnumMember(Value = "get")]
		Get,
		[EnumMember(Value = "post")]
		Post,
		[EnumMember(Value = "put")]
		Put,
		[EnumMember(Value = "delete")]
		Delete
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ResponseContentType
	{
		[EnumMember(Value = "json")]
		Json,
		[EnumMember(Value = "yaml")]
		Yaml,
		[EnumMember(Value = "text")]
		Text
	}

	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<WatcherProxy>))]
	public interface IWatcherProxy
	{
		string Host { get; set; }
		int? Port { get; set; }
	}

	public class WatcherProxy : IWatcherProxy
	{
		public string Host { get; set; }

		public int? Port { get; set; }
	}

	public class WatcherProxyDescriptor
		: DescriptorBase<WatcherProxyDescriptor, IWatcherProxy>, IWatcherProxy
	{
		string IWatcherProxy.Host { get; set; }
		int? IWatcherProxy.Port { get; set; }

		public WatcherProxyDescriptor Host(string host) => Assign(a => a.Host = host);

		public WatcherProxyDescriptor Port(int port) => Assign(a => a.Port = port);
	}
}
