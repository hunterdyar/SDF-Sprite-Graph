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
		
		public List<SDFNode> GetConnectedNodeInputs(SDFNode node)
		{
			//can probably optimize linq query a lot
			//todo: cache! this happens for every pixel.
			var to = Connections.Where(x => x.ToNode == node.guid).Select(x => x.FromNode).ToList();
			return Nodes.Where(x => to.Contains(x.guid)).ToList();
		}

		public SDFNode GetNode(string guid)
		{
			return Nodes.FirstOrDefault(x => x.guid==guid);
		}

		public float GetValueIntoNode(int x, int y, SDFNode node)
		{
			//get the value
			var from = GetConnectedNodeInputs(node);

			foreach (var sdfNode in from)
			{
				//if it is a primitive, it has no inputs.
				if (sdfNode is IPrimitive primitive)
				{
					//todo: there is no default merge
					return primitive.Calculate(x, y);
				}
				
				//if it is a transformation, it we first get the value of it's inputs, then transform them.
				//if it is a combination, we get the value of it's inputs and apply the appropriate operation.
			}
			

			return 0;
		}

		public float GetValue(int x, int y)
		{
			//Apply origin type transformation.
			var p = OutputNode.GetAppliedPosition(x, y);
			return GetValueIntoNode(p.x, p.y, OutputNode);
		}
	}
}