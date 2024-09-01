using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Zoompy
{
   [CreateAssetMenu(fileName = "SDFSprite", menuName = "SDFSprite/SDFSprite", order = 1)]
   public class SDFSprite : ScriptableObject
   {
      //todo: child asset. How did we do it in daily golf gen?
      private Texture2D _spriteTexture;
      private Sprite _sprite;
      
      public SDFDescription Description = new SDFDescription();
      [SerializeField] private FilterMode _filterMode = FilterMode.Point;


      [ContextMenu("Generate")]
      public void Generate()
      {
	      //create empty if needed
	      SaveTexture2D();
	      Description.ClearCache();
	      for (int x = 0; x < Description.OutputNode.Width; x++)
	      {
		      for (int y = 0; y < Description.OutputNode.Height; y++)
		      {
			      var n = Description.GetValue(x, y);
			      _spriteTexture.SetPixel(x, y, n < 0? Description.OutputNode.ForegroundColor : Description.OutputNode.BackgroundColor);
		      }
	      }
	      _spriteTexture.Apply();
	      
	      
	      //okay now actually save
	      SaveTexture2D();
      }
      public void SaveTexture2D()
      {
         _spriteTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GetAssetPath(this));

         if (_spriteTexture == null)
         {
	         _spriteTexture = new Texture2D(Description.OutputNode.Width, Description.OutputNode.Height);
	         _spriteTexture.filterMode = _filterMode;
	         _spriteTexture.name = this.name + "_tex";
#if UNITY_EDITOR
	         AssetDatabase.AddObjectToAsset(_spriteTexture, AssetDatabase.GetAssetPath(this));
	         AssetDatabase.SaveAssets();
#endif
         }

         if (_sprite == null)
         {
	         _sprite = Sprite.Create(_spriteTexture, new Rect(0, 0, _spriteTexture.width, _spriteTexture.height),
		         Vector2.zero);
	         _sprite.name = this.name + "_sprite";
	         AssetDatabase.AddObjectToAsset(_sprite, AssetDatabase.GetAssetPath(this));
	         AssetDatabase.SaveAssets();
         }

         //if size changes
            if (_spriteTexture.width != Description.OutputNode.Width ||
                _spriteTexture.height != Description.OutputNode.Height)
            {
               _spriteTexture.Reinitialize(Description.OutputNode.Width, Description.OutputNode.Height);
               _sprite = Sprite.Create(_spriteTexture, new Rect(0, 0, _spriteTexture.width, _spriteTexture.height),
	               Vector2.zero);
               EditorUtility.SetDirty(_sprite);
               _spriteTexture.filterMode = _filterMode;
               EditorUtility.SetDirty(_spriteTexture);

#if UNITY_EDITOR
               AssetDatabase.SaveAssets();
#endif  
               
            }
            
#if UNITY_EDITOR
            EditorUtility.SetDirty(_spriteTexture);
            EditorUtility.SetDirty(_sprite);
            AssetDatabase.SaveAssetIfDirty(AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(_spriteTexture)));
#endif
      }
   }
}