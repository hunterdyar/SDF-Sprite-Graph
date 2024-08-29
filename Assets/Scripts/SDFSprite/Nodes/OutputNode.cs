using System.Drawing;
using UnityEditor.Experimental.GraphView;

namespace Zoompy
{
	[System.Serializable]
	public class OutputNode : SDFNode
	{
		public Color BackgroundColor;
		public Color ForegroundColor;
		public int Width;
		public int Height;
		public Connection Input;
	}
}