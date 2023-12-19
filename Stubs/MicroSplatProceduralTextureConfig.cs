// Decompiled with JetBrains decompiler
// Type: MicroSplatProceduralTextureConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFC99A68-8D2B-435D-B53B-B46AED82CA49
// Assembly location: M:\7D2D\alpha20.6all\7DaysToDie_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class MicroSplatProceduralTextureConfig : ScriptableObject
{
    public MicroSplatProceduralTextureConfig.TableSize proceduralCurveTextureSize = MicroSplatProceduralTextureConfig.TableSize.k256;
    public List<Gradient> heightGradients = new List<Gradient>();
    public List<MicroSplatProceduralTextureConfig.HSVCurve> heightHSV = new List<MicroSplatProceduralTextureConfig.HSVCurve>();
    public List<Gradient> slopeGradients = new List<Gradient>();
    public List<MicroSplatProceduralTextureConfig.HSVCurve> slopeHSV = new List<MicroSplatProceduralTextureConfig.HSVCurve>();
    [HideInInspector]
    public List<MicroSplatProceduralTextureConfig.Layer> layers = new List<MicroSplatProceduralTextureConfig.Layer>();
    private Texture2D curveTex;
    private Texture2D paramTex;
    private Texture2D heightGradientTex;
    private Texture2D heightHSVTex;
    private Texture2D slopeGradientTex;
    private Texture2D slopeHSVTex;

    public void ResetToDefault()
    {
        this.layers = new List<MicroSplatProceduralTextureConfig.Layer>(3);
        this.layers.Add(new MicroSplatProceduralTextureConfig.Layer());
        this.layers.Add(new MicroSplatProceduralTextureConfig.Layer());
        this.layers.Add(new MicroSplatProceduralTextureConfig.Layer());
        this.layers[1].textureIndex = 1;
        this.layers[1].slopeActive = true;
        this.layers[1].slopeCurve = new AnimationCurve(new Keyframe[4]
        {
      new Keyframe(0.03f, 0.0f),
      new Keyframe(0.06f, 1f),
      new Keyframe(0.16f, 1f),
      new Keyframe(0.2f, 0.0f)
        });
        this.layers[0].slopeActive = true;
        this.layers[0].textureIndex = 2;
        this.layers[0].slopeCurve = new AnimationCurve(new Keyframe[2]
        {
      new Keyframe(0.13f, 0.0f),
      new Keyframe(0.25f, 1f)
        });
    }

    public Texture2D GetHeightGradientTexture()
    {
        int height = 32;
        int width = 128;
        if ((UnityEngine.Object)this.heightGradientTex == (UnityEngine.Object)null)
        {
            this.heightGradientTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            this.heightGradientTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color grey = Color.grey;
        for (int index = 0; index < this.heightGradients.Count; ++index)
        {
            for (int x = 0; x < width; ++x)
            {
                float time = (float)x / (float)width;
                Color color = this.heightGradients[index].Evaluate(time);
                this.heightGradientTex.SetPixel(x, index, color);
            }
        }
        for (int count = this.heightGradients.Count; count < 32; ++count)
        {
            for (int x = 0; x < width; ++x)
                this.heightGradientTex.SetPixel(x, count, grey);
        }
        this.heightGradientTex.Apply(false, false);
        return this.heightGradientTex;
    }

    public Texture2D GetHeightHSVTexture()
    {
        int height = 32;
        int width = 128;
        if ((UnityEngine.Object)this.heightHSVTex == (UnityEngine.Object)null)
        {
            this.heightHSVTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            this.heightHSVTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color grey = Color.grey;
        for (int index = 0; index < this.heightHSV.Count; ++index)
        {
            for (int x = 0; x < width; ++x)
            {
                Color color = grey;
                float time = (float)x / (float)width;
                color.r = (float)((double)this.heightHSV[index].H.Evaluate(time) * 0.5 + 0.5);
                color.g = (float)((double)this.heightHSV[index].S.Evaluate(time) * 0.5 + 0.5);
                color.b = (float)((double)this.heightHSV[index].V.Evaluate(time) * 0.5 + 0.5);
                this.heightHSVTex.SetPixel(x, index, color);
            }
        }
        for (int count = this.heightHSV.Count; count < 32; ++count)
        {
            for (int x = 0; x < width; ++x)
                this.heightHSVTex.SetPixel(x, count, grey);
        }
        this.heightHSVTex.Apply(false, false);
        return this.heightHSVTex;
    }

    public Texture2D GetSlopeGradientTexture()
    {
        int height = 32;
        int width = 128;
        if ((UnityEngine.Object)this.slopeGradientTex == (UnityEngine.Object)null)
        {
            this.slopeGradientTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            this.slopeGradientTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color grey = Color.grey;
        for (int index = 0; index < this.slopeGradients.Count; ++index)
        {
            for (int x = 0; x < width; ++x)
            {
                float time = (float)x / (float)width;
                Color color = this.slopeGradients[index].Evaluate(time);
                this.slopeGradientTex.SetPixel(x, index, color);
            }
        }
        for (int count = this.slopeGradients.Count; count < 32; ++count)
        {
            for (int x = 0; x < width; ++x)
                this.slopeGradientTex.SetPixel(x, count, grey);
        }
        this.slopeGradientTex.Apply(false, false);
        return this.slopeGradientTex;
    }

    public Texture2D GetSlopeHSVTexture()
    {
        int height = 32;
        int width = 128;
        if ((UnityEngine.Object)this.slopeHSVTex == (UnityEngine.Object)null)
        {
            this.slopeHSVTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            this.slopeHSVTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color grey = Color.grey;
        for (int index = 0; index < this.slopeHSV.Count; ++index)
        {
            for (int x = 0; x < width; ++x)
            {
                Color color = grey;
                float time = (float)x / (float)width;
                color.r = (float)((double)this.slopeHSV[index].H.Evaluate(time) * 0.5 + 0.5);
                color.g = (float)((double)this.slopeHSV[index].S.Evaluate(time) * 0.5 + 0.5);
                color.b = (float)((double)this.slopeHSV[index].V.Evaluate(time) * 0.5 + 0.5);
                this.slopeHSVTex.SetPixel(x, index, color);
            }
        }
        for (int count = this.slopeHSV.Count; count < 32; ++count)
        {
            for (int x = 0; x < width; ++x)
                this.slopeHSVTex.SetPixel(x, count, grey);
        }
        this.slopeHSVTex.Apply(false, false);
        return this.slopeHSVTex;
    }

    private float CompFilter(
      MicroSplatProceduralTextureConfig.Layer.Filter f,
      MicroSplatProceduralTextureConfig.Layer.CurveMode mode,
      float v)
    {
        float num = Mathf.Clamp01(Mathf.Pow(Mathf.Abs(v - f.center) * (1f / Mathf.Max(f.width, 0.0001f)), f.contrast));
        switch (mode)
        {
            case MicroSplatProceduralTextureConfig.Layer.CurveMode.BoostFilter:
                return 1f - num;
            case MicroSplatProceduralTextureConfig.Layer.CurveMode.HighPass:
                return (double)v >= (double)f.center ? 1f : 1f - num;
            case MicroSplatProceduralTextureConfig.Layer.CurveMode.LowPass:
                return (double)v <= (double)f.center ? 1f : 1f - num;
            case MicroSplatProceduralTextureConfig.Layer.CurveMode.CutFilter:
                return num;
            default:
                Debug.LogError((object)"Unhandled case in ProceduralTextureConfig::CompFilter");
                return 0.0f;
        }
    }

    public Texture2D GetCurveTexture()
    {
        int height = 32;
        int curveTextureSize = (int)this.proceduralCurveTextureSize;
        if ((UnityEngine.Object)this.curveTex != (UnityEngine.Object)null && this.curveTex.width != curveTextureSize)
        {
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object)this.curveTex);
            this.curveTex = (Texture2D)null;
        }
        if ((UnityEngine.Object)this.curveTex == (UnityEngine.Object)null)
        {
            this.curveTex = new Texture2D(curveTextureSize, height, TextureFormat.RGBA32, false, true);
            this.curveTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color white = Color.white;
        for (int index = 0; index < this.layers.Count; ++index)
        {
            for (int x = 0; x < curveTextureSize; ++x)
            {
                Color color = white;
                float num = (float)x / (float)curveTextureSize;
                if (this.layers[index].heightActive)
                    color.r = this.layers[index].heightCurveMode != MicroSplatProceduralTextureConfig.Layer.CurveMode.Curve ? this.CompFilter(this.layers[index].heightFilter, this.layers[index].heightCurveMode, num) : this.layers[index].heightCurve.Evaluate(num);
                if (this.layers[index].slopeActive)
                    color.g = this.layers[index].slopeCurveMode != MicroSplatProceduralTextureConfig.Layer.CurveMode.Curve ? this.CompFilter(this.layers[index].slopeFilter, this.layers[index].slopeCurveMode, num) : this.layers[index].slopeCurve.Evaluate(num);
                if (this.layers[index].cavityMapActive)
                    color.b = this.layers[index].cavityCurveMode != MicroSplatProceduralTextureConfig.Layer.CurveMode.Curve ? this.CompFilter(this.layers[index].cavityMapFilter, this.layers[index].cavityCurveMode, num) : this.layers[index].cavityMapCurve.Evaluate(num);
                if (this.layers[index].erosionMapActive)
                    color.a = this.layers[index].erosionCurveMode != MicroSplatProceduralTextureConfig.Layer.CurveMode.Curve ? this.CompFilter(this.layers[index].erosionFilter, this.layers[index].erosionCurveMode, num) : this.layers[index].erosionMapCurve.Evaluate(num);
                this.curveTex.SetPixel(x, index, color);
            }
        }
        this.curveTex.Apply(false, false);
        return this.curveTex;
    }

    public Texture2D GetParamTexture()
    {
        int height = 32;
        int width = 4;
        if ((UnityEngine.Object)this.paramTex == (UnityEngine.Object)null || this.paramTex.format != TextureFormat.RGBAHalf || this.paramTex.width != width)
        {
            this.paramTex = new Texture2D(width, height, TextureFormat.RGBAHalf, false, true);
            this.paramTex.hideFlags = HideFlags.HideAndDontSave;
        }
        Color color1 = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        for (int index = 0; index < this.layers.Count; ++index)
        {
            Color color2 = color1;
            Color color3 = color1;
            if (this.layers[index].noiseActive)
            {
                color2.r = this.layers[index].noiseFrequency;
                color2.g = this.layers[index].noiseRange.x;
                color2.b = this.layers[index].noiseRange.y;
                color2.a = this.layers[index].noiseOffset;
            }
            color3.r = this.layers[index].weight;
            color3.g = (float)this.layers[index].textureIndex;
            this.paramTex.SetPixel(0, index, color2);
            this.paramTex.SetPixel(1, index, color3);
            Vector4 biomeWeights = this.layers[index].biomeWeights;
            this.paramTex.SetPixel(2, index, new Color(biomeWeights.x, biomeWeights.y, biomeWeights.z, biomeWeights.w));
            Vector4 biomeWeights2 = this.layers[index].biomeWeights2;
            this.paramTex.SetPixel(3, index, new Color(biomeWeights2.x, biomeWeights2.y, biomeWeights2.z, biomeWeights2.w));
        }
        this.paramTex.Apply(false, false);
        return this.paramTex;
    }

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

        public MicroSplatProceduralTextureConfig.Layer Copy() => new MicroSplatProceduralTextureConfig.Layer()
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
}
