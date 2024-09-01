using System;
using Zoompy.Structures;

namespace Zoompy
{
	[System.Serializable]
	public class SimpleMerge : SDFNode
	{
		public Merge merge;
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			var inputs = system.GetConnectedNodeInputs(this);

			if (inputs.Count == 2)
			{
				float a = inputs[0].Calculate(x,y,ref system);
				float b = inputs[1].Calculate(x,y,ref system);
				switch (merge)
				{
					case Merge.Intersection:
						return Math.Max(a, b);
					case Merge.Subtraction:
						return Math.Max(a,-b);
					case Merge.Union:
					default:
						return Math.Min(a, b);
				}
			}
			
			if (inputs.Count == 1)
			{
				return inputs[0].Calculate(x,y,ref system);
			}
			
			return 0f;
		}
	}
}