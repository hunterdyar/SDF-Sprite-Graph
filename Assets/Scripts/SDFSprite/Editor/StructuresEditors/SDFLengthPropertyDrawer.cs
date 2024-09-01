using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zoompy.Structures;

namespace SDFSprite.Editor
{
	[CustomPropertyDrawer(typeof(SDFLength))]
	public class SDFLengthPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var container = new VisualElement();
			container.style.flexDirection = FlexDirection.Row;
			
			var typeProp = property.FindPropertyRelative("Type");
			var valProp = property.FindPropertyRelative("Value");

			Label label = new Label(property.name);
			container.Add(label);
			
			EnumField typeField = new EnumField(SDFLengthType.Pixels);
			typeField.bindingPath = typeProp.propertyPath;
			typeField.Bind(typeProp.serializedObject);
			container.Add(typeField);
			
			FloatField valueField = new FloatField();
			valueField.bindingPath = valProp.propertyPath;
			valueField.Bind(valProp.serializedObject);
			container.Add(valueField);
			
			
			return container;
		}
	}
}