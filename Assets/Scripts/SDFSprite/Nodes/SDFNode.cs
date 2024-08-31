using UnityEngine;

namespace Zoompy
{
	//The trouble is if we want runtime support, we need a way to seralize the data independent of the "Node" editor class.
	[System.Serializable]
	public class SDFNode
	{
		public string guid;
		public virtual string Name => "Node";
		public Rect editorPosition;
	}
}

