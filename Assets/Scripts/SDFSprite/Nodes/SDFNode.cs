using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class SDFNode
	{
		public string guid;
		public string name;
		public Rect editorPosition;
		public Connection[] inputs;
		public Connection[] outputs;
	}
}

