﻿using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class SystemGraphSaveLoad
	{
		private SystemGraphEditor _editor;
		private SystemGraphView _graphView;

		public SystemGraphSaveLoad(SystemGraphEditor editor, SystemGraphView graphView)
		{
			_editor = editor;
			_graphView = graphView;
		}
		
		//save all
		public void Save(SDFSprite gen)
		{
			//make sure positions and such are updated.
			//this is because I'm too lazy to save on dragging around with an event handler.
			foreach (var node in _graphView.nodes)
			{
				if (node is BaseNodeView nodeView)
				{
					nodeView.PreSaveDataPopulate();
				}
			}
			
			//save Output Node
			SaveNodes(gen);
			SaveEdges(gen);
			
			EditorUtility.SetDirty(gen);
			AssetDatabase.SaveAssetIfDirty(gen);
		}

		private void SaveEdges(SDFSprite gen)
		{
			gen.Description.Connections = _graphView.edges.Where(e => e.input.node != null && e.output.node != null)
				.Select(e => { return GraphEdgeToSystemEdge(e); }).ToArray();
		}
		private void SaveNodes(SDFSprite gen)
		{
			gen.Description.Nodes = _graphView.nodes.Select(x => x as SystemNodeView).Where(x => x != null)
				.Select(n => n.SystemNode).ToArray();
		}

		private Connection GraphEdgeToSystemEdge(Edge edge)
		{
			if (edge.input.node is BaseNodeView input)
			{
				if (edge.output.node is BaseNodeView output)
				{
					return new Connection()
					{
						ToNode = input.Guid,
						ToIndex = edge.input.parent.IndexOf(edge.input),

						FromNode = output.Guid,
						FromIndex = edge.output.parent.IndexOf(edge.output)
					};
				}
			}
			return null;
		}
	}
}