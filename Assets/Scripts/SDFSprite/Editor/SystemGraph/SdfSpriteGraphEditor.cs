using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zoompy
{
	public class SdfSpriteGraphEditor : EditorWindow
	{
		private SDFSprite _currentComponentContainer;
		private SdfSpriteGraphView _graphView;

		public SdfSpriteGraphSaveLoad saveLoad;
		//toolbar
		private Toolbar _toolbar;
		private Label _systemNameLabel;
		
		[OnOpenAsset(1)]
		public static bool ShowWindow(int instanceID, int line)
		{
			UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceID);
			if (item is SDFSprite sprite)
			{
				SdfSpriteGraphEditor window = (SdfSpriteGraphEditor)GetWindow(typeof(SdfSpriteGraphEditor));
				window.titleContent = new GUIContent( $"SDF Sprite Editor ({item.name})");
				window._currentComponentContainer = item as SDFSprite;
				window.minSize = new Vector2(150, 150);
				window.Load();
			}

			return false;
		}

		private void OnEnable()
		{
			if (_currentComponentContainer != null)
			{
				ConstructGraphView();
			}

			GenerateToolbar();
			Load();               
		}
		
		private void GenerateToolbar()
		{
			_toolbar = new Toolbar();
			//Save Button
			Button saveBtn = new Button()
			{
				text = "Save",
			};
			saveBtn.clicked += () => { Save(); };
			_toolbar.Add(saveBtn);

			//Generate Button
			Button genBtn = new Button()
			{
				text = "Apply",
			};
			genBtn.clicked += () =>
			{
				Save();
				_currentComponentContainer.Generate();
			};
			_toolbar.Add(genBtn);
			
			//label
			if (_systemNameLabel == null)
			{
				_systemNameLabel = new Label();
			}
			_toolbar.Add(_systemNameLabel);
			
			//
			rootVisualElement.Add(_toolbar); 
		}

		void Load()
		{
			if (_systemNameLabel == null)
			{
				_systemNameLabel = new Label();
			}

			if (_currentComponentContainer != null)
			{
				_systemNameLabel.text = _currentComponentContainer.name;
				ConstructGraphView();
				saveLoad = new SdfSpriteGraphSaveLoad(this, _graphView);
			}
		}
		public void Save()
		{
			if (_currentComponentContainer != null)
			{
				saveLoad.Save(_currentComponentContainer);
			}
		}

		private void ConstructGraphView()
		{
			if (_graphView != null)
			{
				rootVisualElement.Remove(_graphView);
			}
			
			_graphView = new SdfSpriteGraphView(_currentComponentContainer,this);
			rootVisualElement.Add(_graphView);
			_graphView.StretchToParentSize();
			_graphView.style.width = new StyleLength(Length.Percent(100));
			_graphView.style.height = new StyleLength(Length.Percent(100));
			_graphView.style.top = new StyleLength(new Length(16));
		}
		
	}
}