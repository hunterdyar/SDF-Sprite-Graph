using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class SystemNodeView : BaseNodeView
	{
		public SDFNode SystemNode => _sdfNode;
		private SDFNode _sdfNode;

		public SystemNodeView(SDFNode node, SDFSprite parent) : base(parent)
		{
			_sdfNode = node;
			Init();
		}

		private void Init()
		{
			this.capabilities = this.capabilities & ~Capabilities.Collapsible;
			
			this.title = _sdfNode.name;
			this.name = _sdfNode.name;
			this.SetID(_sdfNode.guid);
			SetPosition(_sdfNode.editorPosition);
			RefreshExpandedState();
			RefreshPorts();
		}

		public override void PreSaveDataPopulate()
		{
			var pos = GetPosition();
			_sdfNode.editorPosition = pos;
		}

		// private void CreateInputPorts()
		// {
		// 	for (int i = 0; i < _sdfNode.System.numberInputs; i++)
		// 	{
		// 		var input = AddPort("Input  " + i.ToString(), Direction.Input, Port.Capacity.Single);
		// 	}
		// }
		//
		// private void CreateOutputPorts()
		// {
		// 	for (int i = 0; i < _sdfNode.System.numberOutputs; i++)
		// 	{
		// 		var output = AddPort("Output  " + i.ToString(), Direction.Output, Port.Capacity.Multi);
		// 	}
		//
		// }
		
		
	}
}