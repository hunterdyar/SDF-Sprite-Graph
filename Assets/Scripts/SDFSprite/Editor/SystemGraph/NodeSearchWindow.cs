﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zoompy;

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
		
		//todo: generate these at least?
		tree.Add(new SearchTreeGroupEntry(new GUIContent("Primitives"),1));
		tree.Add(new SearchTreeEntry(new GUIContent("Circle"))
		{
			level = 2,
			userData = new Circle(),
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Box"))
		{
			level = 2,
			userData = new Zoompy.Box(),
		});
		
		tree.Add(new SearchTreeGroupEntry(new GUIContent("Transformations"), 1));
		tree.Add(new SearchTreeEntry(new GUIContent("Translate"))
		{
			level = 2,
			userData = new Zoompy.Translate(),
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Border (Outline)"))
		{
			level = 2,
			userData = new Zoompy.Border(),
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Round"))
		{
			level = 2,
			userData = new Zoompy.Round(),
		});

		tree.Add(new SearchTreeGroupEntry(new GUIContent("Combinations"), 1));
		tree.Add(new SearchTreeEntry(new GUIContent("Union"))
		{
			level = 2,
			userData = new Zoompy.SimpleMerge(){merge = Merge.Union},
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Intersection"))
		{
			level = 2,
			userData = new Zoompy.SimpleMerge() { merge = Merge.Intersection },
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Subtraction"))
		{
			level = 2,
			userData = new Zoompy.SimpleMerge() { merge = Merge.Subtraction },
		});
		tree.Add(new SearchTreeEntry(new GUIContent("Lerp"))
		{
			level = 2,
			userData = new Zoompy.Lerp() {  },
		});
		

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

	//todo: better name
	private bool CheckForNodeType(SearchTreeEntry searchTreeEntry, Vector2 pos)
	{
		switch (searchTreeEntry.userData)
		{
			//todo: I ... think this is always true right now at least.
			//but in the future we could have groups or utility stuff.
			case OutputNode:
				return false;
			// case Primitive primitive:
			// 	var prim = _graphView.CreateNewSystemNodeView(pos, primitive);
			// 	_graphView.AddElement(prim);
			// 	return true;
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
