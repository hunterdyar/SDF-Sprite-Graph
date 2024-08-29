using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoompy
{
   [CreateAssetMenu(fileName = "SDFSprite", menuName = "SDFSprite/SDFSprite", order = 1)]
   public class SDFSprite : ScriptableObject
   {
      //todo: child asset. How did we do it in daily golf gen?
      private Texture2D _spriteTexture;
      public SDFDescription Description = new SDFDescription();
   }
}