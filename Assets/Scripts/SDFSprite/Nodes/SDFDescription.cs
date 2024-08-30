using System.Collections.Generic;
using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class SDFDescription
	{
		[HideInInspector]//just used for data wrangling in the editor
		public SDFSprite SDFSprite;
		
		//we can't save these as a tree-structure (as structs inside of OutputNode), because this is both edit and run-time data.
		//and edit-time data needs slop, extra leafs, and not perfect graphs.
		public OutputNode OutputNode;
		
		//todo: from sdfNode to appropriate baseView
		public SDFNode[] Nodes;
		public Connection[] Connections;
	}
}