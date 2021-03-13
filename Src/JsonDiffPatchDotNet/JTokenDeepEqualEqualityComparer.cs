using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonDiffPatchDotNet
{
	internal class JTokenDeepEqualEqualityComparer : IEqualityComparer<JToken>
	{
		public static readonly JTokenDeepEqualEqualityComparer Instance = new JTokenDeepEqualEqualityComparer();

		public bool Equals(JToken t1, JToken t2)
		{
			if (t1 == null && t1 == null) return true;
			else if (t1 == null || t1 == null) return false;

			return JToken.DeepEquals(t1, t2);
		}

		public int GetHashCode(JToken t)
		{
			return t.GetHashCode();
		}
	}
}
