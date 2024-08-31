using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class Circle : SDFNode, IPrimitive
	{
		public SDFForm Shape = SDFForm.Circle;
		public float Radius = 32;
		
		public float Calculate(int x, int y)
		{
			//ignore all inputs, we have none!
			switch (Shape)
			{
				case SDFForm.Circle:
					return SDFFormula.Circle(x, y, Radius);
				default:
					return 0;
			}
		}
	}
}