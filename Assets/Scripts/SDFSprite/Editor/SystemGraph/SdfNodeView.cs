using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Zoompy.Generator.Editor.SystemGraph
{
	/// <summary>
	/// Editor, Visual representation of each Node.
	/// We are using one class for all of our nodes. This is sloppy and I don't like it.
	/// </summary>
	public class SdfNodeView : BaseNodeView
	{
		public SDFNode SDFNode => _sdfNode;
		private readonly SDFNode _sdfNode;

		public SdfNodeView(SDFNode node, SDFSprite parent) : base(parent)
		{
			_sdfNode = node;
			Init();
		}

		private void Init()
		{
			this.capabilities = this.capabilities & ~Capabilities.Collapsible;
			//
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