// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine;
// using UnityEngine.UIElements;
// using Zoompy;
//
// [CustomEditor(typeof(Zoompy.Generator.SDFSprite))]
// public class ComponentGeneratorEditor : Editor
// {
// 	public override VisualElement CreateInspectorGUI()
// 	{
// 		VisualElement container = new VisualElement();
//
// 		container.Add(new Label("Settings Reference"));
// 		var genSettingsProperty = serializedObject.FindProperty("_genSettings");
// 		var genSettingsElements = new PropertyField(genSettingsProperty);
// 		container.Add(genSettingsElements);
// 		
// 		container.Add(new Label("Inputs"));
//
// 		var numInputsProperty = serializedObject.FindProperty("numberInputs");
// 		var numInputsElements= new PropertyField(numInputsProperty);
// 		container.Add(numInputsElements);
//
// 		var numOutputsProperty = serializedObject.FindProperty("numberOutputs");
// 		var numOuputsElements = new PropertyField(numOutputsProperty);
// 		container.Add(numOuputsElements);
//
// 		//signalHooks (logic)
// 		var signaltypes = GetSignalHookImplementors();
// 		container.Add(new Label("Logic"));
// 		var propLogicNames = signaltypes.Where(prop => Attribute.IsDefined(prop, typeof(Zoompy.LogicAttribute))).Select(p=>p.Name).ToList();
// 		var propLogicPaths = signaltypes.Where(prop => Attribute.IsDefined(prop, typeof(Zoompy.LogicAttribute))).Select(p=>(Attribute.GetCustomAttribute(p,typeof(LogicAttribute)) as LogicAttribute).Path).ToList();
// 		propLogicNames.Insert(0,"None");
// 		propLogicPaths.Insert(0,"None");
// 		var logicBaseClassNameProperty = serializedObject.FindProperty("baseLogicClassName");
// 		int index = propLogicNames.IndexOf(logicBaseClassNameProperty.stringValue);
// 		if (index < 0)
// 		{
// 			//no correct name set yet.
// 			index = 0;
// 		}
// 		
// 		var logicNameDropdown = new DropdownField(propLogicPaths,index,
// 			Zoompy.Generator.ComponentGenerator.StripLogicSuffix,
// 			Zoompy.Generator.ComponentGenerator.StripLogicSuffix);
// 		logicNameDropdown.RegisterValueChangedCallback(e =>
// 		{
// 			//display the paths, but we want the names.
// 			var name = propLogicNames[propLogicPaths.IndexOf(e.newValue)];
// 			((Zoompy.Generator.ComponentGenerator)target).baseLogicClassName = name;
// 			serializedObject.ApplyModifiedProperties();
// 		});
// 		container.Add(logicNameDropdown);
// //
// 		var interactorTypes = GetInteractorImplementors();
// 		container.Add(new Label("Interactors"));
// 		var propInteractorNames = interactorTypes.Where(prop => Attribute.IsDefined(prop, typeof(Zoompy.InteractorAttribute)))
// 			.Select(p => p.Name).ToList();
// 		var propInteractorPaths = interactorTypes.Where(prop => Attribute.IsDefined(prop, typeof(Zoompy.InteractorAttribute)))
// 			.Select(p => (Attribute.GetCustomAttribute(p, typeof(InteractorAttribute)) as InteractorAttribute).Path).ToList();
// 		propInteractorNames.Insert(0,"None");
// 		propInteractorPaths.Insert(0,"None");
// 		var interactorBaseClassNameProperty = serializedObject.FindProperty("baseInteractorClassName");
// 		index = propInteractorNames.IndexOf(interactorBaseClassNameProperty.stringValue);
// 		if (index < 0)
// 		{
// 			//no correct name set yet.
// 			index = 0;
// 		}
//
// 		var interactorNameDropdown = new DropdownField(propInteractorPaths, index,
// 			Zoompy.Generator.ComponentGenerator.StripLogicSuffix,
// 			Zoompy.Generator.ComponentGenerator.StripLogicSuffix);
// 		interactorNameDropdown.RegisterValueChangedCallback(e =>
// 		{
// 			//display the paths, but we want the names.
// 			var name = propInteractorNames[propInteractorPaths.IndexOf(e.newValue)];
// 			((Zoompy.Generator.ComponentGenerator)target).baseInteractorClassName = name;
// 			serializedObject.ApplyModifiedProperties();
// 		});
// 		container.Add(interactorNameDropdown);
// 		
// 		//
// 		var isLeafProp = serializedObject.FindProperty("IsLeaf");
// 		var isLeafElement = new PropertyField(isLeafProp);
// 		container.Add(isLeafElement);
// 		
// 		//material
// 		container.Add(new Label("Visuals"));
// 		var overrideMatProperty = serializedObject.FindProperty("_overrideContainerMaterial");
// 		var overrideMatElements = new PropertyField(overrideMatProperty);
// 		container.Add(overrideMatElements);
//
// 		container.Add(new Label("Debug"));
// 		var innerSystemProp = serializedObject.FindProperty("InnerSystem");
// 		var innserSystemElement = new PropertyField(innerSystemProp);
// 		innserSystemElement.SetEnabled(false);
// 		container.Add(innserSystemElement);
// 		
// 		return container;
// 	}
//
// 	private IEnumerable<Type> GetSignalHookImplementors()
// 	{
// 		var asm = Assembly.GetAssembly(typeof(ISignalHook));
// 		var it = typeof(ISignalHook);
// 		return asm.GetLoadableTypes().Where(it.IsAssignableFrom).ToList();
// 	}
//
// 	private IEnumerable<Type> GetInteractorImplementors()
// 	{
// 		var asm = Assembly.GetAssembly(typeof(IComponentInteractor));
// 		var ci = typeof(IComponentInteractor);
// 		return asm.GetLoadableTypes().Where(ci.IsAssignableFrom).ToList();
// 	}
// }