using System;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroSplat
{
	public class TextureArrayConfig : MonoBehaviour
	{

		public bool diffuseIsLinear;
		public bool antiTileArray;
		public bool emisMetalArray;
		public bool traxArray;
		public TextureArrayConfig.TextureMode textureMode;
		public TextureArrayConfig.ClusterMode clusterMode;
		public TextureArrayConfig.PackingMode packingMode;
		public TextureArrayConfig.PBRWorkflow pbrWorkflow;
		public int hash;
		public Texture2DArray splatArray;
		public Texture2DArray diffuseArray;
		public Texture2DArray normalSAOArray;
		public Texture2DArray smoothAOArray;
		public Texture2DArray specularArray;
		public Texture2DArray diffuseArray2;
		public Texture2DArray normalSAOArray2;
		public Texture2DArray smoothAOArray2;
		public Texture2DArray specularArray2;
		public Texture2DArray diffuseArray3;
		public Texture2DArray normalSAOArray3;
		public Texture2DArray smoothAOArray3;
		public Texture2DArray specularArray3;
		public Texture2DArray emisArray;
		public Texture2DArray emisArray2;
		public Texture2DArray emisArray3;
		public TextureArrayGroup defaultTextureSettings;
		public PlatformTextureOverride platformOverrides;
		public int sourceTextureSize;
		public TextureArrayConfig.AllTextureChannel allTextureChannelHeight;
		public TextureArrayConfig.AllTextureChannel allTextureChannelSmoothness;
		public TextureArrayConfig.AllTextureChannel allTextureChannelAO;
		public List<TextureArrayConfig.TextureEntry> sourceTextures;
		public List<TextureArrayConfig.TextureEntry> sourceTextures2;
		public List<TextureArrayConfig.TextureEntry> sourceTextures3;

		public enum AllTextureChannel
		{
			R,
			G,
			B,
			A,
			Custom,
		}

		public enum TextureChannel
		{
			R,
			G,
			B,
			A,
		}

		public enum Compression
		{
			AutomaticCompressed,
			ForceDXT,
			ForcePVR,
			ForceETC2,
			ForceASTC,
			ForceCrunch,
			Uncompressed,
		}

		public enum TextureSize
		{
			k32 = 32, // 0x00000020
			k64 = 64, // 0x00000040
			k128 = 128, // 0x00000080
			k256 = 256, // 0x00000100
			k512 = 512, // 0x00000200
			k1024 = 1024, // 0x00000400
			k2048 = 2048, // 0x00000800
			k4096 = 4096, // 0x00001000
		}

		[Serializable]
		public class TextureArraySettings
		{
			public TextureArrayConfig.TextureSize textureSize;
			public TextureArrayConfig.Compression compression;
			public FilterMode filterMode;

			[Range(0.0f, 16f)]
			public int Aniso = 1;

			public TextureArraySettings(
				TextureArrayConfig.TextureSize s,
				TextureArrayConfig.Compression c,
				FilterMode f,
				int a = 1)
			{
				this.textureSize = s;
				this.compression = c;
				this.filterMode = f;
				this.Aniso = a;
			}

		}

		public enum PBRWorkflow
		{
			Metallic,
			Specular,
		}

		public enum PackingMode
		{
			Fastest,
			Quality,
		}

		public enum SourceTextureSize
		{
			Unchanged = 0,
			k32 = 32, // 0x00000020
			k256 = 256, // 0x00000100
		}

		public enum TextureMode
		{
			Basic,
			PBR,
		}

		public enum ClusterMode
		{
			None,
			TwoVariations,
			ThreeVariations,
		}

		[Serializable]
		public class TextureArrayGroup
		{
			public TextureArrayConfig.TextureArraySettings diffuseSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings normalSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Trilinear);
			public TextureArrayConfig.TextureArraySettings smoothSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings antiTileSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings emissiveSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings specularSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings traxDiffuseSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings traxNormalSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
			public TextureArrayConfig.TextureArraySettings decalSplatSettings = new TextureArrayConfig.TextureArraySettings(TextureArrayConfig.TextureSize.k1024, TextureArrayConfig.Compression.AutomaticCompressed, FilterMode.Bilinear);
		}

		[Serializable]
		public class PlatformTextureOverride
		{
			public TextureArrayConfig.TextureArrayGroup settings = new TextureArrayConfig.TextureArrayGroup();
		}

		[Serializable]
		public class TextureEntry
		{
			public Texture2D diffuse;
			public Texture2D height;
			public TextureArrayConfig.TextureChannel heightChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D normal;
			public Texture2D smoothness;
			public TextureArrayConfig.TextureChannel smoothnessChannel = TextureArrayConfig.TextureChannel.G;
			public bool isRoughness;
			public Texture2D ao;
			public TextureArrayConfig.TextureChannel aoChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D emis;
			public Texture2D metal;
			public TextureArrayConfig.TextureChannel metalChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D specular;
			public Texture2D noiseNormal;
			public Texture2D detailNoise;
			public TextureArrayConfig.TextureChannel detailChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D distanceNoise;
			public TextureArrayConfig.TextureChannel distanceChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D traxDiffuse;
			public Texture2D traxHeight;
			public TextureArrayConfig.TextureChannel traxHeightChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D traxNormal;
			public Texture2D traxSmoothness;
			public TextureArrayConfig.TextureChannel traxSmoothnessChannel = TextureArrayConfig.TextureChannel.G;
			public bool traxIsRoughness;
			public Texture2D traxAO;
			public TextureArrayConfig.TextureChannel traxAOChannel = TextureArrayConfig.TextureChannel.G;
			public Texture2D splat;

		}
	
	}
}