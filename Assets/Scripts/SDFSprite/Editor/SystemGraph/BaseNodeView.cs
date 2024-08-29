using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class BaseNodeView : UnityEditor.Experimental.GraphView.Node
    {
        public int Index => _index;
        protected int _index = 0;
        public string Guid => guid;
        public string guid;
        
        protected SdfSpriteGraphView _graphView;
        protected SdfSpriteGraphEditor _graphEditor;
        
        protected Vector2 defaultNodeSize = new Vector2(200, 250);
        
		public Action<BaseNodeView> OnNodeSelected;
        protected SDFSprite _parent;
        public BaseNodeView(SDFSprite parent)
        {
            _parent = parent;
            // style.left = node.position.x;
            // style.top = node.position.y;
            guid = GUID.Generate().ToString();
            this.viewDataKey = guid;
        }

        public Port AddPort(string name, Direction nodeDir, Port.Capacity capacity = Port.Capacity.Single)
        {
             var port = InstantiatePort(Orientation.Horizontal, nodeDir, capacity, typeof(byte));
             port.name = name;
             port.portName = name;
            // port.title = name;
             if (nodeDir == Direction.Output)
             {
                 outputContainer.Add(port);
             }
             else if(nodeDir == Direction.Input)
             {
                 inputContainer.Add(port);
             }

             return port;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            //node.position = newPos.position;
        }

        public virtual void PreSaveDataPopulate()
        {
            
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

        public void SetID(string g)
        {
            this.guid = g;
        }
	}
}