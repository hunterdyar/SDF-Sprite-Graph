using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy
{
	public class BaseNodeView : UnityEditor.Experimental.GraphView.Node
    {
        public string Guid => guid;
        public string guid;
        
        protected SdfSpriteGraphView _graphView;
        protected SdfSpriteGraphEditor _graphEditor;
        
        protected Vector2 defaultNodeSize = new Vector2(200, 250);
        
		public Action<BaseNodeView> OnNodeSelected;
        protected SDFSprite _SDFSprite;
        public SDFNode SDFNode => _sdfNode;
        protected readonly SDFNode _sdfNode;

        protected SerializedObject _sdfSerializedObject;

        public BaseNodeView(SDFSprite sdfSprite, SDFNode node)
        {
            _sdfNode = node;
            _SDFSprite = sdfSprite;
            guid = GUID.Generate().ToString();
            this.viewDataKey = guid;
            _sdfSerializedObject = new SerializedObject(sdfSprite);
            Init();
        }

        private void Init()
        {
            this.capabilities = this.capabilities & ~Capabilities.Collapsible;
            this.title = _sdfNode.Name;
            this.name = _sdfNode.Name;
            this.SetID(_sdfNode.guid);
            SetPosition(_sdfNode.editorPosition);
            RefreshExpandedState();
            GenerateSelf();
            RefreshPorts();
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
            var pos = GetPosition();
            _sdfNode.editorPosition = pos;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

        protected virtual void GenerateSelf()
        {
            
        }

        public void SetID(string g)
        {
            this.guid = g;
        }

        public virtual void Apply()
        {
            
        }

        public static BaseNodeView GetViewForNode(SDFSprite systemParent, SDFNode node)
        {
            return node switch
            {
                Circle => new CircleNodeView(systemParent, node),
                Translate => new TranslateNodeView(systemParent, node),
                SimpleMerge => new SimpleMergeNodeView(systemParent,node),
                Lerp => new LerpNodeView(systemParent,node),
                _ => new BaseNodeView(systemParent, node)
            };
        }

    }
}