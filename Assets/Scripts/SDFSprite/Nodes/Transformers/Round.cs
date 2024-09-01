using UnityEngine;

namespace Zoompy
{
	public class Round : SDFNode
	{
		public float Radius;

		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			var input = system.GetConnectedNodeInputs(this);
			if (input.Count == 1)
			{
				return input[0].Calculate(x, y, ref system) - Radius;
			}

			//error!
			return 0;
		}
	}
}