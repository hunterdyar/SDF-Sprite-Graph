using System.Collections.Generic;
using System.Linq;
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
		[SerializeReference]
		public SDFNode[] Nodes;
		public Connection[] Connections;
		private Dictionary<string, List<SDFNode>> _nodes = new Dictionary<string, List<SDFNode>>();
		public List<SDFNode> GetConnectedNodeInputs(SDFNode node)
		{
			//can probably optimize linq query a lot
			//todo: cache! this happens for every pixel.
			if (_nodes.ContainsKey(node.guid))
			{
				return _nodes[node.guid];
			}
			var to = Connections.Where(x => x.ToNode == node.guid).Select(x => x.FromNode).ToList();
			var n = Nodes.Where(x => to.Contains(x.guid)).ToList();
			_nodes.Add(node.guid, n);
			return n;
		}

		public SDFNode GetNode(string guid)
		{
			return Nodes.FirstOrDefault(x => x.guid==guid);
		}

		public void ClearCache()
		{
			_nodes.Clear();
		}

		public float GetValueIntoNode(int x, int y, SDFNode node)
		{
			//get the value
			var from = GetConnectedNodeInputs(node);
			
			SDFDescription d = this;
			
			//default behaviour, first result or min them together?
			float f = Mathf.Infinity;
			for (var i = 0; i < from.Count; i++)
			{
				f = Mathf.Min(from[i].Calculate(x, y, ref d), f);
			}
			
			

			return f;
		}

		public float GetValue(int x, int y)
		{
			//Apply origin type transformation.
			var p = OutputNode.GetAppliedPosition(x, y);
			return GetValueIntoNode(p.x, p.y, OutputNode);
		}
	}
}