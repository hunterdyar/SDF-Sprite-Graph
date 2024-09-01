using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class RoundNodeView : BaseNodeView
	{
		private FloatField _radius;

		public RoundNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
		}

		protected override void GenerateSelf()
		{
			title = "Round";
			AddPort("Shape", Direction.Output, Port.Capacity.Single);
			AddPort("Shape", Direction.Input, Port.Capacity.Single);

			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}
			
			_radius = new FloatField("Radius");
			_radius.value = ((Round)_sdfNode).Radius;
			mainContainer.Add(_radius);
			

			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Round)_sdfNode).Radius = _radius.value;
		}
	}
}