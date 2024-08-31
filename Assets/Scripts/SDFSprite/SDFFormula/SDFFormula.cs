using Unity.Mathematics;
using UnityEngine;

namespace Zoompy
{
	public static class SDFFormula
	{
		public static float Circle(int x, int y, float radius)
		{
			return Mathf.Sqrt(x * x + y * y) - radius;
		}

		public static float Rect(int x, int y, float width, float height)
		{
			float2 p = new float2(x, y);
			float2 b = new float2(width, height);
		//	float2 df = Abs()
			return 0;
		}

		private static float2 Abs(float2 a, float2 b)
		{
			return new float2(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));
		}
	}
}