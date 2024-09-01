using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class LerpNodeView : BaseNodeView
	{
		private FloatField _tField;

		public LerpNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
		}

		protected override void GenerateSelf()
		{
			title = "Lerp";
			AddPort("A", Direction.Input, Port.Capacity.Single);
			AddPort("B", Direction.Input, Port.Capacity.Single);

			AddPort("Out", Direction.Output, Port.Capacity.Single);


			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}

			var n = (Lerp)_sdfNode;

			//todo: this can be a property field with binding.
			_tField = new FloatField("T Value");
			_tField.value = n.Value;
			mainContainer.Add(_tField);
			
			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Lerp)_sdfNode).Value = _tField.value;
		}
	}
}