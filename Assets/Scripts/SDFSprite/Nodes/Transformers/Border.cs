using UnityEngine;

namespace Zoompy
{
	public class Border : SDFNode
	{
		public float Width;
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			var input = system.GetConnectedNodeInputs(this);
			if (input.Count == 1)
			{
				var f = input[0].Calculate(x, y, ref system);
				return Mathf.Abs(f) - (Width/2f);
			}

			//error!
			return 0;
		}
	}
}