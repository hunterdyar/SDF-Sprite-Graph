using UnityEditor.Experimental.GraphView;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class OutputNodeView : BaseNodeView
	{
		public OutputNodeView(SDFSprite parent, SDFNode node) : base(parent, node)
		{
			
		}

		protected override void GenerateSelf()
		{
			AddPort("Input", Direction.Input, Port.Capacity.Single);
			base.GenerateSelf();
		}
	}
}