using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zoompy.Generator.Editor.SystemGraph
{
	public class OutputNodeView : BaseNodeView
	{
		private SerializedObject sdfSerializedObject;
		public OutputNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
			sdfSerializedObject = new SerializedObject(_SDFSprite);
		}

		protected override void GenerateSelf()
		{
			AddPort("Input", Direction.Input, Port.Capacity.Single);

			if (sdfSerializedObject == null)
			{
				sdfSerializedObject = new SerializedObject(_SDFSprite);
			}
			
			var width = sdfSerializedObject.FindProperty("Description.OutputNode.Width");
			var widthField = new PropertyField(width);
			widthField.Bind(width.serializedObject);
			mainContainer.Add(widthField);
			
			mainContainer.Add(new Label("test"));
			
			base.GenerateSelf();
		}
	}
}