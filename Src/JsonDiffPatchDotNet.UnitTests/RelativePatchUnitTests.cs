using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonDiffPatchDotNet.UnitTests
{
	[TestFixture]
	public class RelativePatchUnitTests
	{

		[Test]
		public void RelativePatch_ArrayAddRemove_Success()
		{
			var jdp = new JsonDiffPatch(new Options { ArrayDiff = ArrayDiffMode.Relative });
			var left = JObject.Parse(@"{ ""p"" : [1, 2, 3] }");
			var right = JObject.Parse(@"{ ""p"" : [2, 3, 4] }");

			var other = JObject.Parse(@"{ ""p"" : [2, 1, 6, 7] }");

			var patch = jdp.Diff(left, right);

			var patched = jdp.Patch(left, patch);
			var expectedPatched = JObject.Parse(@"{ ""p"" : [2, 3, 4] }");

			var patchedOther = jdp.Patch(other, patch);
			var expectedPatchedOther = JObject.Parse(@"{ ""p"" : [2, 6, 7, 4] }");

			Assert.True(JToken.DeepEquals(expectedPatched, patched));
			Assert.True(JToken.DeepEquals(expectedPatchedOther, patchedOther));
		}

		[Test]
		public void RelativePatch_NestedArrayAddRemove_Success()
		{
			var jdp = new JsonDiffPatch(new Options { ArrayDiff = ArrayDiffMode.Relative });
			var left = JObject.Parse(@"{ ""p"" : { ""names"": [""Foo"", ""Bar""], ""numbers"": [5, 10] } }");
			var right = JObject.Parse(@"{ ""p"" : { ""names"": [""Foo"", ""Baz""], ""numbers"": [10, 5, 1] } }");

			var other = JObject.Parse(@"{ ""p"" : { ""names"": [""Bar"", ""Boop""], ""numbers"": [5, 10, 1] } }");

			var patch = jdp.Diff(left, right);

			var patched = jdp.Patch(left, patch);
			var expectedPatched = JObject.Parse(@"{ ""p"" : { ""names"": [""Foo"", ""Baz""], ""numbers"": [5, 10, 1]} }");

			var patchedOther = jdp.Patch(other, patch);
			var expectedPatchedOther = JObject.Parse(@"{ ""p"" : { ""names"": [""Boop"", ""Baz""], ""numbers"": [5, 10, 1]} }");

			Assert.True(JToken.DeepEquals(expectedPatched, patched));
			Assert.True(JToken.DeepEquals(expectedPatchedOther, patchedOther));
		}

		[Test]
		public void RelativePatch_ArrayAddRemoveObject_Success()
		{
			var jdp = new JsonDiffPatch(new Options { ArrayDiff = ArrayDiffMode.Relative });
			var left = JObject.Parse(@"{ ""p"" : [ {""x"": 5}, {""x"": 10} ] }");
			var right = JObject.Parse(@"{ ""p"" : [ {""x"": 10}, {""x"": 15}, {""x"": 18} ] }");

			var other = JObject.Parse(@"{ ""p"" : [ {""x"": 15}, {""x"": 20} ] }");

			var patch = jdp.Diff(left, right);

			var patched = jdp.Patch(left, patch);
			var expectedPatched = JObject.Parse(@"{ ""p"" : [ {""x"": 10}, {""x"": 15}, {""x"": 18} ] }");

			var patchedOther = jdp.Patch(other, patch);
			var expectedPatchedOther = JObject.Parse(@"{ ""p"" : [ {""x"": 15}, {""x"": 20}, {""x"": 18}] }");

			Assert.True(JToken.DeepEquals(expectedPatched, patched));
			var j = patchedOther.ToString();
			Assert.True(JToken.DeepEquals(expectedPatchedOther, patchedOther));
		}
	}
}
