using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class SdfSpriteGraphView : GraphView
	{
		private SdfSpriteGraphEditor _editor;
		private string styleName = "";

		private NodeSearchWindow _searchWindow;
		public SDFSprite SDFSprite => _systemParent;
		SDFSprite _systemParent;
        public SDFDescription Description;
        
        //private SystemOutputNodeView _outputsNodeView;
        public SdfSpriteGraphView(SDFSprite parent, SdfSpriteGraphEditor graphEditorWindow)
        {
	        _editor = graphEditorWindow;
	        if (parent == null)
	        {
		        Debug.LogWarning("fuck");
	        }
	        
	        _systemParent = parent;
	        if (!string.IsNullOrEmpty(styleName))
	        {
		        StyleSheet style = Resources.Load<StyleSheet>(styleName);
		        styleSheets.Add(style);
	        }

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
			this.AddManipulator(new EdgeManipulator());
			
			GridBackground grid = new GridBackground();
			Insert(0,grid);
			grid.StretchToParentSize();

			AddSearchWindow();
			LoadNodeViewsFromData();
        }

        private void AddSearchWindow()
        {
	        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
	        _searchWindow.Configure(_editor,this);
	        nodeCreationRequest = context =>
		        SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }
        /// <summary>
        /// Creates the graph from serialized data.
        /// </summary>
        void LoadNodeViewsFromData()
        {
	        if (_systemParent == null)
	        {
		        return;
	        }

	        //output node, there always is one.
	        _systemParent.Description.OutputNode.guid = "output";
	        var outputNode = new OutputNodeView(_systemParent, _systemParent.Description.OutputNode);
	        AddElement(outputNode);
	        
	        //safety catch on first init. does "preSaveDataPopulate" handle this?
	        if (_systemParent.Description == null)
	        {
		        _systemParent.Description = new SDFDescription();
	        }
			
	        if (_systemParent.Description.Nodes == null)
	        {
		        _systemParent.Description.Nodes = Array.Empty<SDFNode>();
	        }
	        //
	        foreach (var sNode in _systemParent.Description.Nodes)
	        {
		        var node = BaseNodeView.GetViewForNode(_systemParent,sNode);
		        AddElement(node);
	        }

	      
	        
	        //all connections
	        foreach (var edge in _systemParent.Description.Connections)
	        {
		        var from = GetNode(edge.FromNode);
		        var to = GetNode(edge.ToNode);
		        if (from != null && to != null)
		        {
			        var fromPort = from.Query<Port>().Where(p=>p.direction == Direction.Output).AtIndex(edge.FromIndex);
			        var toPort = to.Query<Port>().Where(p => p.direction == Direction.Input).AtIndex(edge.ToIndex);
			        if (fromPort == null || toPort == null)
			        {
				        Debug.LogWarning("Bad edge data. reopen the window and re-save the graph, please.");
				        return;
			        }
			        var e = fromPort.ConnectTo(toPort);
			        if (e != null)
			        {
				        AddElement(e);
			        }
		        }
	        }
        }

        public BaseNodeView GetNode(string guid)
        {
	        return nodes.Select(x => { return (x as BaseNodeView); }).Where(x => x != null).First(x => x.Guid == guid);
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
	        List<Port> compatiblePorts = new List<Port>();
	        foreach (var port in ports)
	        {
		        if (startPort.node != port.node && startPort.direction != port.direction)
		        {
			        compatiblePorts.Add(port);
		        }
	        }

	        return compatiblePorts;
        }

        public Rect GetAllNodesBounds()
        {
	        Rect r = new Rect();
	        //grow the rext to encompas all nodes
	        foreach (var node in nodes)
	        {
		        var p = node.GetPosition();
		        r.xMax = Mathf.Max(r.xMax, p.xMax);
		        r.xMin = Mathf.Min(r.xMin, p.xMin);
		        r.yMax = Mathf.Max(r.yMax, p.yMax);
		        r.yMin = Mathf.Min(r.yMin, p.yMin);
	        }

	        return r;
        }

        public BaseNodeView CreateNewSystemNodeView(Vector2 pos, SDFNode node)
        {
	        //create the data.
	        //systemNode.System = system;
	        node.guid = Guid.NewGuid().ToString();

	        //for size
	        node.editorPosition = new Rect(pos, new Vector2(300, 250));
	        
	        //need the node in the array in order to draw it's property, because we are using index.
			_editor.Save();
	        BaseNodeView nv = BaseNodeView.GetViewForNode(_systemParent,node);

	        // This happens when we save.
	       // ComponentGenerator.InnerSystem.Nodes.Add(systemNode);
	        return nv;
        }
	}
}