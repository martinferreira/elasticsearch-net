using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class WatchId : IUrlParameter
	{
		private readonly string _watchId;

		public WatchId(string watchId)
		{
			if (watchId == null)
				throw new ArgumentNullException(nameof(watchId));

			if (watchId.Length == 0)
				throw new ArgumentException("must have a length", nameof(watchId));

			this._watchId = watchId;
		}

		public string GetString(IConnectionConfigurationValues settings) => _watchId;

		public override string ToString() => _watchId;

		public static implicit operator WatchId(string watchId) => new WatchId(watchId);
	}
}
