using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public partial interface IPutWatchRequest
	{
		[JsonProperty("metadata")]
		IDictionary<string, object> Metadata { get; set; }

		[JsonProperty("trigger")]
		ITriggerContainer Trigger { get; set; }

		[JsonProperty("input")]
		InputContainer Input { get; set; }

		[JsonProperty("throttle_period")]
		string ThrottlePeriod { get; set; }

		[JsonProperty("condition")]
		ConditionContainer Condition { get; set; }

		[JsonProperty("transform")]
		TransformContainer Transform { get; set; }

		[JsonProperty("actions")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, IAction> Actions { get; set; }
	}

	public partial class PutWatchRequest
	{
	}

	[DescriptorFor("XpackWatcherPutWatch")]
	public partial class PutWatchDescriptor
	{
	}

	[JsonObject]
	public interface IAction
	{
		[JsonProperty("transform")]
		TransformContainer Transform { get; set; }

		[JsonIgnore]
		string ThrottlePeriod { get; set; }
	}

	public abstract class Action : IAction
	{
		public TransformContainer Transform { get; set; }

		public string ThrottlePeriod { get; set; }
	}

	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TransformContainer>))]
	public interface ITransformContainer
	{
		[JsonProperty("search")]
		ISearchTransform Search { get; set; }

		[JsonProperty("script")]
		IScriptTransform Script { get; set; }

		[JsonProperty("chain")]
		IChainTransform Chain { get; set; }
	}

	[JsonObject]
	public class TransformContainer : ITransformContainer
	{
		public TransformContainer()
		{
		}

		public TransformContainer(TransformBase transform)
		{
			transform.ContainIn(this);
		}

		ISearchTransform ITransformContainer.Search { get; set; }
		IScriptTransform ITransformContainer.Script { get; set; }
		IChainTransform ITransformContainer.Chain { get; set; }
	}

	public class TransformDescriptor : TransformContainer
	{
		private ITransformContainer Self => this;

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector)
		{
			Self.Search = selector(new SearchTransformDescriptor());
			return this;
		}

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector)
		{
			Self.Script = selector(new ScriptTransformDescriptor());
			return this;
		}

		// TODO : Chain is an array of ITransform's
		public TransformDescriptor Chain()
		{
			return this;
		}
	}

	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeConverter<SearchTransform>))]
	public interface ISearchTransform : ITransform
	{
		[JsonProperty("search_type")]
		SearchType? SearchType { get; set; }
		[JsonProperty("indices")]
		Indices Indices { get; set; }
		[JsonProperty("indices_options")]
		IIndicesOptions IndicesOptions { get; set; }
		[JsonProperty("type")]
		Types Type { get; set; }
		[JsonProperty("body")]
		ISearchRequest Body { get; set; }
		[JsonProperty("template")]
		ISearchTemplateRequest Template { get; set; }
	}

	public class SearchTransform : TransformBase, ISearchTransform
	{
		public SearchType? SearchType { get; set; }
		public Indices Indices { get; set; }
		public IIndicesOptions IndicesOptions { get; set; }
		public Types Type { get; set; }
		public ISearchRequest Body { get; set; }
		public ISearchTemplateRequest Template { get; set; }

		internal override void ContainIn(ITransformContainer container)
		{
			container.Search = this;
		}
	}

	public class SearchTransformDescriptor : DescriptorBase<SearchTransformDescriptor, ISearchTransform>, ISearchTransform
	{
		SearchType? ISearchTransform.SearchType { get; set; }
		Indices ISearchTransform.Indices { get; set; }
		IIndicesOptions ISearchTransform.IndicesOptions { get; set; }
		Types ISearchTransform.Type { get; set; }
		ISearchRequest ISearchTransform.Body { get; set; }
		ISearchTemplateRequest ISearchTransform.Template { get; set; }

		public SearchTransformDescriptor SearchType(SearchType searchType) =>
			Assign(a => a.SearchType = searchType);

		public SearchTransformDescriptor Indices(Indices indices) =>
			Assign(a => a.Indices = indices);

		public SearchTransformDescriptor Indices<T>() =>
			Assign(a => a.Indices = typeof(T));

		public SearchTransformDescriptor IndicesOptions(Func<IndicesOptionsDescriptor, IIndicesOptions> selector) =>
			Assign(a => selector?.InvokeOrDefault(new IndicesOptionsDescriptor()));

		public SearchTransformDescriptor Type(Types type) => Assign(a => a.Type = type);

		public SearchTransformDescriptor Type<T>() => Assign(a => a.Type = typeof(T));

		public SearchTransformDescriptor Body<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(a => a.Body = selector?.InvokeOrDefault(new SearchDescriptor<T>()));

		public SearchTransformDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class =>
			Assign(a => a.Template = selector?.InvokeOrDefault(new SearchTemplateDescriptor<T>()));
	}

	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndicesOptions>))]
	public interface IIndicesOptions
	{
		[JsonProperty("expand_wildcards")]
		[JsonConverter(typeof(StringEnumConverter))]
		ExpandWildcards? ExpandWildcards { get; set; }

		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[JsonProperty("allow_no_indices")]
		bool? AllowNoIndices { get; set; }
	}

	[JsonObject]
	public class IndicesOptions
	{
		public ExpandWildcards? ExpandWildcards { get; set; }

		public bool? IgnoreUnavailable { get; set; }

		public bool? AllowNoIndices { get; set; }
	}

	public class IndicesOptionsDescriptor : DescriptorBase<IndicesOptionsDescriptor, IIndicesOptions>, IIndicesOptions
	{
		ExpandWildcards? IIndicesOptions.ExpandWildcards { get; set; }
		bool? IIndicesOptions.IgnoreUnavailable { get; set; }
		bool? IIndicesOptions.AllowNoIndices { get; set; }

		public IndicesOptionsDescriptor ExpandWildcards(ExpandWildcards expandWildcards) =>
			Assign(a => a.ExpandWildcards = expandWildcards);

		public IndicesOptionsDescriptor IgnoreUnavailable(bool ignoreUnavailable = true) =>
			Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		public IndicesOptionsDescriptor AllowNoIndices(bool allowNoIndices = true) =>
			Assign(a => a.AllowNoIndices = allowNoIndices);
	}
}
