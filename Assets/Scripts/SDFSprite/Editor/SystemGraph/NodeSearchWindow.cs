using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zoompy;
using Zoompy.Generator.Editor.SystemGraph;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
	private SdfSpriteGraphEditor _graphEditor;
	private SdfSpriteGraphView _graphView;
	

	public void Configure(SdfSpriteGraphEditor editor, SdfSpriteGraphView view)
	{
		_graphEditor = editor;
		_graphView = view;
	}
	public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
	{
		List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
		{
			new SearchTreeGroupEntry(new GUIContent("Nodes"), 0),
		};
		
		tree.Add(new SearchTreeEntry(new GUIContent("Output Node"))
		{
			level = 1,
			userData = new OutputNode(),
		});

		// var objects = AssetDatabase.FindAssets("t:SdfNode");
		// foreach (string o in objects)
		// {
		// 	var path = AssetDatabase.GUIDToAssetPath(o);
		// 	var cg = AssetDatabase.LoadAssetAtPath<Zoompy.SDFSprite>(path);
		// 	if (cg != null)
		// 	{
		// 		if (cg == _graphView.SDFSprite)
		// 		{
		// 			//prevent infinite nesting.
		// 			//one could still copy/paste into the nodes. please don't?
		// 			continue;
		// 		}
		// 		var t = AddNodeSearch(cg.name, 1, cg);
		// 		tree.Add(t);
		// 	}
		// }

		return tree;
	}
	
	public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
	{
		Vector2 mousePosition = _graphEditor.rootVisualElement.ChangeCoordinatesTo(
			_graphEditor.rootVisualElement.parent, context.screenMousePosition - _graphEditor.position.position
			);
		Vector2 graphMousePos = _graphView.contentViewContainer.WorldToLocal(mousePosition);

		return CheckForNodeType(SearchTreeEntry, graphMousePos);
	}

	private bool CheckForNodeType(SearchTreeEntry searchTreeEntry, Vector2 pos)
	{
		switch (searchTreeEntry.userData)
		{
			//todo: I ... think this is always true right now at least.
			//but in the future we could have groups or utility stuff.
			case SDFNode sdfNode:
				var node = _graphView.CreateNewSystemNodeView(pos, sdfNode);
				_graphView.AddElement(node);
				return true;
		}

		return false;
	}

	private IEnumerable<Type> GetSDFNodeImplementors()
	{
		var asm = Assembly.GetAssembly(typeof(SDFNode));
		var ci = typeof(SDFNode);
		return asm.GetTypes().Where(ci.IsAssignableFrom).ToList();
	}
}
