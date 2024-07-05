using System;
using System.Collections.Generic;
using UnityEngine;

public class MicroSplatProceduralTextureConfig : MonoBehaviour
{

	public enum TableSize
	{
		k64 = 64, // 0x00000040
		k128 = 128, // 0x00000080
		k256 = 256, // 0x00000100
		k512 = 512, // 0x00000200
		k1024 = 1024, // 0x00000400
		k2048 = 2048, // 0x00000800
		k4096 = 4096, // 0x00001000
	}

	[Serializable]
	public class Layer
	{
		public float weight = 1f;
		public int textureIndex;
		public bool noiseActive;
		public float noiseFrequency = 1f;
		public float noiseOffset;
		public Vector2 noiseRange = new Vector2(0.0f, 1f);
		public Vector4 biomeWeights = new Vector4(1f, 1f, 1f, 1f);
		public Vector4 biomeWeights2 = new Vector4(1f, 1f, 1f, 1f);
		public bool heightActive;
		public AnimationCurve heightCurve = AnimationCurve.Linear(0.0f, 1f, 1f, 1f);
		public MicroSplatProceduralTextureConfig.Layer.Filter heightFilter = new MicroSplatProceduralTextureConfig.Layer.Filter();
		public bool slopeActive;
		public AnimationCurve slopeCurve = AnimationCurve.Linear(0.0f, 1f, 1f, 1f);
		public MicroSplatProceduralTextureConfig.Layer.Filter slopeFilter = new MicroSplatProceduralTextureConfig.Layer.Filter();
		public bool erosionMapActive;
		public AnimationCurve erosionMapCurve = AnimationCurve.Linear(0.0f, 1f, 1f, 1f);
		public MicroSplatProceduralTextureConfig.Layer.Filter erosionFilter = new MicroSplatProceduralTextureConfig.Layer.Filter();
		public bool cavityMapActive;
		public AnimationCurve cavityMapCurve = AnimationCurve.Linear(0.0f, 1f, 1f, 1f);
		public MicroSplatProceduralTextureConfig.Layer.Filter cavityMapFilter = new MicroSplatProceduralTextureConfig.Layer.Filter();
		public MicroSplatProceduralTextureConfig.Layer.CurveMode heightCurveMode;
		public MicroSplatProceduralTextureConfig.Layer.CurveMode slopeCurveMode;
		public MicroSplatProceduralTextureConfig.Layer.CurveMode erosionCurveMode;
		public MicroSplatProceduralTextureConfig.Layer.CurveMode cavityCurveMode;

		public MicroSplatProceduralTextureConfig.Layer Copy()
		{
			return new MicroSplatProceduralTextureConfig.Layer()
			{
				weight = this.weight,
				textureIndex = this.textureIndex,
				noiseActive = this.noiseActive,
				noiseFrequency = this.noiseFrequency,
				noiseOffset = this.noiseOffset,
				noiseRange = this.noiseRange,
				biomeWeights = this.biomeWeights,
				biomeWeights2 = this.biomeWeights2,
				heightActive = this.heightActive,
				slopeActive = this.slopeActive,
				erosionMapActive = this.erosionMapActive,
				cavityMapActive = this.cavityMapActive,
				heightCurve = new AnimationCurve(this.heightCurve.keys),
				slopeCurve = new AnimationCurve(this.slopeCurve.keys),
				erosionMapCurve = new AnimationCurve(this.erosionMapCurve.keys),
				cavityMapCurve = new AnimationCurve(this.cavityMapCurve.keys),
				cavityMapFilter = this.cavityMapFilter,
				heightFilter = this.heightFilter,
				slopeFilter = this.slopeFilter,
				erosionFilter = this.erosionFilter,
				heightCurveMode = this.heightCurveMode,
				slopeCurveMode = this.slopeCurveMode,
				erosionCurveMode = this.erosionCurveMode,
				cavityCurveMode = this.cavityCurveMode
			};
		}

		[Serializable]
		public class Filter
		{
			public float center = 0.5f;
			public float width = 0.1f;
			public float contrast = 1f;
		}

		public enum CurveMode
		{
			Curve,
			BoostFilter,
			HighPass,
			LowPass,
			CutFilter,
		}
	}

	[Serializable]
	public class HSVCurve
	{
		public AnimationCurve H = AnimationCurve.Linear(0.0f, 0.5f, 1f, 0.5f);
		public AnimationCurve S = AnimationCurve.Linear(0.0f, 0.0f, 1f, 0.0f);
		public AnimationCurve V = AnimationCurve.Linear(0.0f, 0.0f, 1f, 0.0f);
	}

	public MicroSplatProceduralTextureConfig.TableSize proceduralCurveTextureSize = MicroSplatProceduralTextureConfig.TableSize.k256;
	public List<Gradient> heightGradients = new List<Gradient>();
	public List<MicroSplatProceduralTextureConfig.HSVCurve> heightHSV = new List<MicroSplatProceduralTextureConfig.HSVCurve>();
	public List<Gradient> slopeGradients = new List<Gradient>();
	public List<MicroSplatProceduralTextureConfig.HSVCurve> slopeHSV = new List<MicroSplatProceduralTextureConfig.HSVCurve>();
	public List<MicroSplatProceduralTextureConfig.Layer> layers = new List<MicroSplatProceduralTextureConfig.Layer>();

}