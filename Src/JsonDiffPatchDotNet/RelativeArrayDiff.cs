using Newtonsoft.Json.Linq;

namespace JsonDiffPatchDotNet
{
	public class RelativeArrayDiff
	{
		public readonly JObject Object;
		public readonly JArray ToAdd;
		public readonly JArray ToRemove;

		public RelativeArrayDiff()
		{
			this.Object = JObject.Parse($@"{{ ""{JsonDiffPatch.ArrayDiffToken}"": ""{JsonDiffPatch.ArrayDiffRelative}"" }}");

			this.ToAdd = new JArray();
			this.ToRemove = new JArray();

			this.Object.Add("add", this.ToAdd);
			this.Object.Add("remove", this.ToRemove);

		}
	}
}
