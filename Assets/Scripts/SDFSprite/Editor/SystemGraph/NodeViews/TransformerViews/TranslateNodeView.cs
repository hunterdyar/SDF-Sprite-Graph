using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class TranslateNodeView : BaseNodeView
	{
		private IntegerField _xField;
		private IntegerField _yField;

		public TranslateNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
		}

		protected override void GenerateSelf()
		{
			title = "Translate";
			AddPort("In", Direction.Input, Port.Capacity.Single);
			AddPort("Out", Direction.Output, Port.Capacity.Single);


			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}

			var n = (Translate)_sdfNode;

			_xField = new IntegerField("Translate X");
			_xField.value = n.X;
			mainContainer.Add(_xField);

			_yField = new IntegerField("Translate Y");
			_yField.value = n.X;
			mainContainer.Add(_yField);

			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Translate)_sdfNode).X = _xField.value;
			((Translate)_sdfNode).Y = _yField.value;
		}
	}
}