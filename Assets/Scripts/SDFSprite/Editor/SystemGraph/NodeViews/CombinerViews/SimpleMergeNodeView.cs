using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class SimpleMergeNodeView : BaseNodeView
	{
		public SimpleMergeNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
			//_merge = ((SimpleMerge)node).merge;
		}

		protected override void GenerateSelf()
		{
			AddPort("A", Direction.Input, Port.Capacity.Single);
			AddPort("B", Direction.Input, Port.Capacity.Single);
			AddPort("Combined", Direction.Output, Port.Capacity.Single);


			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}

			var sm = (SimpleMerge)_sdfNode;
			title = $"{sm.merge}";
			
			base.GenerateSelf();
		}
		
	}
}