// Decompiled with JetBrains decompiler
// Type: MicroSplatPropData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFC99A68-8D2B-435D-B53B-B46AED82CA49
// Assembly location: M:\7D2D\alpha20.6all\7DaysToDie_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MicroSplatPropData : ScriptableObject
{
    private const int sMaxTextures = 32;
    private const int sMaxAttributes = 32;
    public Color[] values = new Color[1024];
    private Texture2D tex;
    [HideInInspector]
    public AnimationCurve geoCurve = AnimationCurve.Linear(0.0f, 0.0f, 0.0f, 0.0f);
    private Texture2D geoTex;
    [HideInInspector]
    public AnimationCurve geoSlopeFilter = AnimationCurve.Linear(0.0f, 0.2f, 0.4f, 1f);
    private Texture2D geoSlopeTex;
    [HideInInspector]
    public AnimationCurve globalSlopeFilter = AnimationCurve.Linear(0.0f, 0.2f, 0.4f, 1f);
    private Texture2D globalSlopeTex;

    private void RevisionData()
    {
        if (this.values.Length == 256)
        {
            Color[] colorArray = new Color[1024];
            for (int index1 = 0; index1 < 16; ++index1)
            {
                for (int index2 = 0; index2 < 16; ++index2)
                    colorArray[index2 * 32 + index1] = this.values[index2 * 32 + index1];
            }
            this.values = colorArray;
        }
        else
        {
            if (this.values.Length != 512)
                return;
            Color[] colorArray = new Color[1024];
            for (int index3 = 0; index3 < 32; ++index3)
            {
                for (int index4 = 0; index4 < 16; ++index4)
                    colorArray[index4 * 32 + index3] = this.values[index4 * 32 + index3];
            }
            this.values = colorArray;
        }
    }

    public Color GetValue(int x, int y)
    {
        this.RevisionData();
        return this.values[y * 32 + x];
    }

    public void SetValue(int x, int y, Color c)
    {
        this.RevisionData();
        this.values[y * 32 + x] = c;
    }

    public void SetValue(int x, int y, int channel, float value)
    {
        this.RevisionData();
        int index = y * 32 + x;
        Color color = this.values[index];
        color[channel] = value;
        this.values[index] = color;
    }

    public void SetValue(int x, int y, int channel, Vector2 value)
    {
        this.RevisionData();
        int index = y * 32 + x;
        Color color = this.values[index];
        if (channel == 0)
        {
            color.r = value.x;
            color.g = value.y;
        }
        else
        {
            color.b = value.x;
            color.a = value.y;
        }
        this.values[index] = color;
    }

    public void SetValue(int textureIndex, MicroSplatPropData.PerTexFloat channel, float value)
    {
        int y;
        int channel1 = Mathf.RoundToInt((float)(((double)(y = (int)((double)channel / 4.0)) - (double)y) * 4.0));
        this.SetValue(textureIndex, y, channel1, value);
    }

    public void SetValue(int textureIndex, MicroSplatPropData.PerTexColor channel, Color value)
    {
        int y = (int)((double)channel / 4.0);
        this.SetValue(textureIndex, y, value);
    }

    public void SetValue(int textureIndex, MicroSplatPropData.PerTexVector2 channel, Vector2 value)
    {
        int y;
        int channel1 = Mathf.RoundToInt((float)(((double)(y = (int)((double)channel / 4.0)) - (double)y) * 4.0));
        this.SetValue(textureIndex, y, channel1, value);
    }

    public Texture2D GetTexture()
    {
        this.RevisionData();
        if ((Object)this.tex == (Object)null)
        {
            if (SystemInfo.SupportsTextureFormat(TextureFormat.RGBAFloat))
                this.tex = new Texture2D(32, 32, TextureFormat.RGBAFloat, false, true);
            else if (SystemInfo.SupportsTextureFormat(TextureFormat.RGBAHalf))
            {
                this.tex = new Texture2D(32, 32, TextureFormat.RGBAHalf, false, true);
            }
            else
            {
                Debug.LogError((object)"Could not create RGBAFloat or RGBAHalf format textures, per texture properties will be clamped to 0-1 range, which will break things");
                this.tex = new Texture2D(32, 32, TextureFormat.RGBA32, false, true);
            }
            this.tex.hideFlags = HideFlags.HideAndDontSave;
            this.tex.wrapMode = TextureWrapMode.Clamp;
            this.tex.filterMode = FilterMode.Point;
        }
        this.tex.SetPixels(this.values);
        this.tex.Apply();
        return this.tex;
    }

    public Texture2D GetGeoCurve()
    {
        if ((Object)this.geoTex == (Object)null)
        {
            this.geoTex = new Texture2D(256, 1, TextureFormat.RHalf, false, true);
            this.geoTex.hideFlags = HideFlags.HideAndDontSave;
        }
        for (int x = 0; x < 256; ++x)
        {
            float num = this.geoCurve.Evaluate((float)x / (float)byte.MaxValue);
            this.geoTex.SetPixel(x, 0, new Color(num, num, num, num));
        }
        this.geoTex.Apply();
        return this.geoTex;
    }

    public Texture2D GetGeoSlopeFilter()
    {
        if ((Object)this.geoSlopeTex == (Object)null)
        {
            this.geoSlopeTex = new Texture2D(256, 1, TextureFormat.Alpha8, false, true);
            this.geoSlopeTex.hideFlags = HideFlags.HideAndDontSave;
        }
        for (int x = 0; x < 256; ++x)
        {
            float num = this.geoSlopeFilter.Evaluate((float)x / (float)byte.MaxValue);
            this.geoSlopeTex.SetPixel(x, 0, new Color(num, num, num, num));
        }
        this.geoSlopeTex.Apply();
        return this.geoSlopeTex;
    }

    public Texture2D GetGlobalSlopeFilter()
    {
        if ((Object)this.globalSlopeTex == (Object)null)
        {
            this.globalSlopeTex = new Texture2D(256, 1, TextureFormat.Alpha8, false, true);
            this.globalSlopeTex.hideFlags = HideFlags.HideAndDontSave;
        }
        for (int x = 0; x < 256; ++x)
        {
            float num = this.globalSlopeFilter.Evaluate((float)x / (float)byte.MaxValue);
            this.globalSlopeTex.SetPixel(x, 0, new Color(num, num, num, num));
        }
        this.globalSlopeTex.Apply();
        return this.globalSlopeTex;
    }

    public enum PerTexVector2
    {
        SplatUVScale = 0,
        SplatUVOffset = 2,
    }

    public enum PerTexColor
    {
        Tint = 4,
        SSSRTint = 72, // 0x00000048
    }

    public enum PerTexFloat
    {
        InterpolationContrast = 5,
        NormalStrength = 8,
        Smoothness = 9,
        AO = 10, // 0x0000000A
        Metallic = 11, // 0x0000000B
        Brightness = 12, // 0x0000000C
        Contrast = 13, // 0x0000000D
        Porosity = 14, // 0x0000000E
        Foam = 15, // 0x0000000F
        DetailNoiseStrength = 16, // 0x00000010
        DistanceNoiseStrength = 17, // 0x00000011
        DistanceResample = 18, // 0x00000012
        DisplacementMip = 19, // 0x00000013
        GeoTexStrength = 20, // 0x00000014
        GeoTintStrength = 21, // 0x00000015
        GeoNormalStrength = 22, // 0x00000016
        GlobalSmoothMetalAOStength = 23, // 0x00000017
        DisplacementStength = 24, // 0x00000018
        DisplacementBias = 25, // 0x00000019
        DisplacementOffset = 26, // 0x0000001A
        GlobalEmisStength = 27, // 0x0000001B
        NoiseNormal0Strength = 28, // 0x0000001C
        NoiseNormal1Strength = 29, // 0x0000001D
        NoiseNormal2Strength = 30, // 0x0000001E
        WindParticulateStrength = 31, // 0x0000001F
        SnowAmount = 32, // 0x00000020
        GlitterAmount = 33, // 0x00000021
        GeoHeightFilter = 34, // 0x00000022
        GeoHeightFilterStrength = 35, // 0x00000023
        TriplanarMode = 36, // 0x00000024
        TriplanarContrast = 37, // 0x00000025
        StochatsicEnabled = 38, // 0x00000026
        Saturation = 39, // 0x00000027
        TextureClusterContrast = 40, // 0x00000028
        TextureClusterBoost = 41, // 0x00000029
        HeightOffset = 42, // 0x0000002A
        HeightContrast = 43, // 0x0000002B
        AntiTileArrayNormalStrength = 56, // 0x00000038
        AntiTileArrayDetailStrength = 57, // 0x00000039
        AntiTileArrayDistanceStrength = 58, // 0x0000003A
        DisplaceShaping = 59, // 0x0000003B
        UVRotation = 64, // 0x00000040
        TriplanarRotationX = 65, // 0x00000041
        TriplanarRotationY = 66, // 0x00000042
        FuzzyShadingCore = 68, // 0x00000044
        FuzzyShadingEdge = 69, // 0x00000045
        FuzzyShadingPower = 70, // 0x00000046
        SSSThickness = 75, // 0x0000004B
    }
}
