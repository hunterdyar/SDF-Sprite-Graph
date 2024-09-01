using UnityEngine;

namespace Zoompy.Structures
{
	public enum SDFLengthType
	{
		Pixels,
		PercentWidth,
		PercentHeight,
		PercentReferencePercent,
		PercentReferencePixel
	}
	
	[System.Serializable]
	public struct SDFLength
	{
		public SDFLengthType Type;
		public float Value;

		public float Get(SDFDescription system)
		{
			switch (Type)
			{
				case SDFLengthType.Pixels:
					return Value;
				case SDFLengthType.PercentHeight:
					return Value * system.OutputNode.Height;
				case SDFLengthType.PercentWidth:
					return Value * system.OutputNode.Width;
				default:
					return Value;
			}
		}
	}
}