using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class CircleNodeView : BaseNodeView
	{
		private SerializedObject _sdfSerializedObject;//todo: move to base
		
		private FloatField _radius;
		public CircleNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
			_sdfSerializedObject = new SerializedObject(_SDFSprite);
		}

		protected override void GenerateSelf()
		{
			title = "Circle";
			AddPort("Shape", Direction.Output, Port.Capacity.Single);
			
			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}
			
			if (((Circle)_sdfNode).Shape == SDFForm.Circle)
			{
				_radius = new FloatField("Radius");
				_radius.value = ((Circle)_sdfNode).Radius;
				mainContainer.Add(_radius);
			}

			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Circle)_sdfNode).Radius = _radius.value;
		}
	}
}