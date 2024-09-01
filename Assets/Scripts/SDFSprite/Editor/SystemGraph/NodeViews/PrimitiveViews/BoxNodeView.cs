using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class BoxNodeView : BaseNodeView
	{
		private Vector2Field _size;

		public BoxNodeView(SDFSprite sdfSprite, SDFNode node) : base(sdfSprite, node)
		{
		}

		protected override void GenerateSelf()
		{
			title = "Box";
			AddPort("Shape", Direction.Output, Port.Capacity.Single);

			if (_sdfSerializedObject == null)
			{
				_sdfSerializedObject = new SerializedObject(_SDFSprite);
			}

			var box = (Box)_sdfNode;
			
			_size = new Vector2Field("Size");
			_size.value = new Vector2(box.Width, box.Height);
			mainContainer.Add(_size);

			base.GenerateSelf();
		}

		public override void Apply()
		{
			((Box)_sdfNode).Width = _size.value.x;
			((Box)_sdfNode).Height = _size.value.y;
		}
	}
}