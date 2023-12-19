// Decompiled with JetBrains decompiler
// Type: JBooth.MicroSplat.TextureArrayConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFC99A68-8D2B-435D-B53B-B46AED82CA49
// Assembly location: M:\7D2D\alpha20.6all\7DaysToDie_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroSplat
{
    [CreateAssetMenu(menuName = "MicroSplat/Texture Array Config", order = 1)]
    [ExecuteInEditMode]
    public class TextureArrayConfig : ScriptableObject
    {
        public bool diffuseIsLinear;
        [HideInInspector]
        public bool antiTileArray;
        [HideInInspector]
        public bool emisMetalArray;
        public bool traxArray;
        [HideInInspector]
        public TextureArrayConfig.TextureMode textureMode = TextureArrayConfig.TextureMode.PBR;
        [HideInInspector]
        public TextureArrayConfig.ClusterMode clusterMode;
        [HideInInspector]
        public TextureArrayConfig.PackingMode packingMode;
        [HideInInspector]
        public TextureArrayConfig.PBRWorkflow pbrWorkflow;
        [HideInInspector]
        public int hash;
        private static List<TextureArrayConfig> sAllConfigs = new List<TextureArrayConfig>();
        [HideInInspector]
        public Texture2DArray splatArray;
        [HideInInspector]
        public Texture2DArray diffuseArray;
        [HideInInspector]
        public Texture2DArray normalSAOArray;
        [HideInInspector]
        public Texture2DArray smoothAOArray;
        [HideInInspector]
        public Texture2DArray specularArray;
        [HideInInspector]
        public Texture2DArray diffuseArray2;
        [HideInInspector]
        public Texture2DArray normalSAOArray2;
        [HideInInspector]
        public Texture2DArray smoothAOArray2;
        [HideInInspector]
        public Texture2DArray specularArray2;
        [HideInInspector]
        public Texture2DArray diffuseArray3;
        [HideInInspector]
        public Texture2DArray normalSAOArray3;
        [HideInInspector]
        public Texture2DArray smoothAOArray3;
        [HideInInspector]
        public Texture2DArray specularArray3;
        [HideInInspector]
        public Texture2DArray emisArray;
        [HideInInspector]
        public Texture2DArray emisArray2;
        [HideInInspector]
        public Texture2DArray emisArray3;
        public TextureArrayConfig.TextureArrayGroup defaultTextureSettings = new TextureArrayConfig.TextureArrayGroup();
        public List<TextureArrayConfig.PlatformTextureOverride> platformOverrides = new List<TextureArrayConfig.PlatformTextureOverride>();
        public TextureArrayConfig.SourceTextureSize sourceTextureSize;
        [HideInInspector]
        public TextureArrayConfig.AllTextureChannel allTextureChannelHeight = TextureArrayConfig.AllTextureChannel.G;
        [HideInInspector]
        public TextureArrayConfig.AllTextureChannel allTextureChannelSmoothness = TextureArrayConfig.AllTextureChannel.G;
        [HideInInspector]
        public TextureArrayConfig.AllTextureChannel allTextureChannelAO = TextureArrayConfig.AllTextureChannel.G;
        [HideInInspector]
        public List<TextureArrayConfig.TextureEntry> sourceTextures = new List<TextureArrayConfig.TextureEntry>();
        [HideInInspector]
        public List<TextureArrayConfig.TextureEntry> sourceTextures2 = new List<TextureArrayConfig.TextureEntry>();
        [HideInInspector]
        public List<TextureArrayConfig.TextureEntry> sourceTextures3 = new List<TextureArrayConfig.TextureEntry>();

        public bool IsScatter() => false;

        public bool IsDecal() => false;

        private void Awake() => TextureArrayConfig.sAllConfigs.Add(this);

        private void OnDestroy() => TextureArrayConfig.sAllConfigs.Remove(this);

        public static TextureArrayConfig FindConfig(Texture2DArray diffuse)
        {
            for (int index = 0; index < TextureArrayConfig.sAllConfigs.Count; ++index)
            {
                if ((UnityEngine.Object)TextureArrayConfig.sAllConfigs[index].diffuseArray == (UnityEngine.Object)diffuse)
                    return TextureArrayConfig.sAllConfigs[index];
            }
            return (TextureArrayConfig)null;
        }

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

            public void Reset()
            {
                this.diffuse = (Texture2D)null;
                this.height = (Texture2D)null;
                this.normal = (Texture2D)null;
                this.smoothness = (Texture2D)null;
                this.specular = (Texture2D)null;
                this.ao = (Texture2D)null;
                this.isRoughness = false;
                this.detailNoise = (Texture2D)null;
                this.distanceNoise = (Texture2D)null;
                this.metal = (Texture2D)null;
                this.emis = (Texture2D)null;
                this.heightChannel = TextureArrayConfig.TextureChannel.G;
                this.smoothnessChannel = TextureArrayConfig.TextureChannel.G;
                this.aoChannel = TextureArrayConfig.TextureChannel.G;
                this.distanceChannel = TextureArrayConfig.TextureChannel.G;
                this.detailChannel = TextureArrayConfig.TextureChannel.G;
                this.traxDiffuse = (Texture2D)null;
                this.traxNormal = (Texture2D)null;
                this.traxHeight = (Texture2D)null;
                this.traxSmoothness = (Texture2D)null;
                this.traxAO = (Texture2D)null;
                this.traxHeightChannel = TextureArrayConfig.TextureChannel.G;
                this.traxSmoothnessChannel = TextureArrayConfig.TextureChannel.G;
                this.traxAOChannel = TextureArrayConfig.TextureChannel.G;
                this.splat = (Texture2D)null;
            }

            public bool HasTextures(TextureArrayConfig.PBRWorkflow wf) => wf == TextureArrayConfig.PBRWorkflow.Specular ? (UnityEngine.Object)this.diffuse != (UnityEngine.Object)null || (UnityEngine.Object)this.height != (UnityEngine.Object)null || (UnityEngine.Object)this.normal != (UnityEngine.Object)null || (UnityEngine.Object)this.smoothness != (UnityEngine.Object)null || (UnityEngine.Object)this.specular != (UnityEngine.Object)null || (UnityEngine.Object)this.ao != (UnityEngine.Object)null : (UnityEngine.Object)this.diffuse != (UnityEngine.Object)null || (UnityEngine.Object)this.height != (UnityEngine.Object)null || (UnityEngine.Object)this.normal != (UnityEngine.Object)null || (UnityEngine.Object)this.smoothness != (UnityEngine.Object)null || (UnityEngine.Object)this.metal != (UnityEngine.Object)null || (UnityEngine.Object)this.ao != (UnityEngine.Object)null;
        }
    }
}
