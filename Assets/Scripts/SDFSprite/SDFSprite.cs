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
      public SDFDescription Description = new SDFDescription();
      [SerializeField] private FilterMode _filterMode = FilterMode.Point;


      [ContextMenu("Generate")]
      public void Generate()
      {
	      //create empty if needed
	      SaveTexture2D();

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
	         _spriteTexture.name = this.name + "_img";

#if UNITY_EDITOR
	         AssetDatabase.AddObjectToAsset(_spriteTexture, AssetDatabase.GetAssetPath(this));
	         AssetDatabase.SaveAssets();
#endif
         }

         //if size changes
            if (_spriteTexture.width != Description.OutputNode.Width ||
                _spriteTexture.height != Description.OutputNode.Height)
            {
               _spriteTexture.Reinitialize(Description.OutputNode.Width, Description.OutputNode.Height);
               _spriteTexture.filterMode = _filterMode;
#if UNITY_EDITOR
               AssetDatabase.SaveAssets();
#endif  
               
            }
            
#if UNITY_EDITOR
            EditorUtility.SetDirty(_spriteTexture);
            AssetDatabase.SaveAssetIfDirty(AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(_spriteTexture)));
#endif
      }
   }
}