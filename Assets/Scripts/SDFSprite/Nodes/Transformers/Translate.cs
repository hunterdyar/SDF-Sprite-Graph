using Zoompy.Structures;

namespace Zoompy
{
	public class Translate : SDFNode
	{
		public int X;
		public int Y;

		public SDFLength XOffset;
		
		public override float Calculate(int x, int y, ref SDFDescription system)
		{
			return system.GetValueIntoNode(x-X, y-Y, this);
		}
	}
}