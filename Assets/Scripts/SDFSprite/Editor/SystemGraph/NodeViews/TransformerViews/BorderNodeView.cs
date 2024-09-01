
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class BorderNodeView : BaseNodeView
	{
		private FloatField _widthField;

		public BorderNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
		}

		protected override void GenerateSelf()
		{
			title = "Border";
			AddPort("Shape", Direction.Output, Port.Capacity.Single);
			AddPort("Shape", Direction.Input, Port.Capacity.Single);

			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}

			_widthField = new FloatField("Width");
			_widthField.value = ((Border)_sdfNode).Width;
			mainContainer.Add(_widthField);

			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Border)_sdfNode).Width = _widthField.value;
		}
	}
}