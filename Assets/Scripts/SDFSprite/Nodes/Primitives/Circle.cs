using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class Circle : SDFNode
	{
		public SDFForm Shape = SDFForm.Circle;
		public float Radius = 32;
		
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			return SDFFormula.Circle(x, y, Radius);
		}
	}
}