using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zoompy.Generator.Editor.SystemGraph;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
	private SystemGraphEditor _graphEditor;
	private SystemGraphView _graphView;
	

	public void Configure(SystemGraphEditor editor, SystemGraphView view)
	{
		_graphEditor = editor;
		_graphView = view;
	}
	public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
	{
		List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
		{
			new SearchTreeGroupEntry(new GUIContent("Logic"), 0),
		};

		var objects = AssetDatabase.FindAssets("t:ComponentGenerator");
		foreach (string o in objects)
		{
			var path = AssetDatabase.GUIDToAssetPath(o);
			var cg = AssetDatabase.LoadAssetAtPath<Zoompy.SDFSprite>(path);
			if (cg != null)
			{
				if (cg == _graphView.SDFSprite)
				{
					//prevent infinite nesting.
					//one could still copy/paste into the nodes. please don't?
					continue;
				}
				var t = AddNodeSearch(cg.name, 1, cg);
				tree.Add(t);
			}
		}

		return tree;
	}

	private SearchTreeEntry AddNodeSearch(string name, int level, Zoompy.SDFSprite sdfSprite)
	{
		SearchTreeEntry ste = new SearchTreeEntry(new GUIContent(name))
		{
			level = level,
			userData = sdfSprite
		};
		return ste;
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
			case Zoompy.SDFSprite:
				var gen = searchTreeEntry.userData as Zoompy.SDFSprite;
				var node = _graphView.CreateNewSystemNodeView(pos, gen);
				_graphView.AddElement(node);
				return true;
		}

		return false;
	}
}
