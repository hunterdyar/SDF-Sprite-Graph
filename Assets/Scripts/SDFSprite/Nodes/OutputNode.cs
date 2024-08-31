using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class OutputNode : SDFNode
	{ 
		public override string Name => "Output";
		public Color BackgroundColor;
		public Color ForegroundColor;
		public int Width;
		public int Height;
		public Zoompy.Origin Origin;

		public (int x, int y) GetAppliedPosition(int x, int y)
		{
			switch (Origin)
			{
				case Origin.BottomLeft:
					return (x, y);
				case Origin.Center:
					return (x - Width / 2, y - Height / 2);
			}

			return (x, y);
		}
	}
}