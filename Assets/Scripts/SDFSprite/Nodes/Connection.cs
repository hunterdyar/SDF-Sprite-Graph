using UnityEngine;

namespace Zoompy
{
	[System.Serializable]
	public class Connection 
	{
		//todo: this could be references to SystemNode instead of GUIDs.
		public string FromNode;
		public int FromIndex;
		public string ToNode;
		public int ToIndex;
	}
}