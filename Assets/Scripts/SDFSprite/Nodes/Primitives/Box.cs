using Unity.Mathematics;

namespace Zoompy
{
	public class Box : SDFNode
	{
		public float Width;
		public float Height;
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			//todo: store as float2
			float2 p = new float2(x,y);
			float2 b = new float2(Width, Height);
			
			float2 d = math.abs(p) - b;
			return (float) (math.length(math.max(d, 0.0)) + math.min(math.max(d.x, d.y), 0.0));
		}
	}
}