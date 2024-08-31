using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class OutputNode : SDFNode
	{ 
		public override string Name => "Output";
		public Color BackgroundColor = new Color(0f,0f,0f,0f);
		public Color ForegroundColor = Color.white;
		public int Width = 64;
		public int Height = 64;
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