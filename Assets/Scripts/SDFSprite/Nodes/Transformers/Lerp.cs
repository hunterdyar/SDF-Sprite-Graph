using UnityEngine;

namespace Zoompy
{
	public class Lerp : SDFNode
	{
		public float Value = 1f;
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			var inputs = system.GetConnectedNodeInputs(this);

			if (inputs.Count == 2)
			{
				float a = inputs[0].Calculate(x, y, ref system);
				float b = inputs[1].Calculate(x, y, ref system);
				return Mathf.Lerp(a,b,Value);
			}

			if (inputs.Count == 1)
			{
				float a = inputs[0].Calculate(x, y, ref system);
				return Mathf.Lerp(a, 0, Value);
			}

			return 0f;
		}
	}
}