// Decompiled with JetBrains decompiler
// Type: JBooth.MicroSplat.MicroSplatProceduralTextureUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FFC99A68-8D2B-435D-B53B-B46AED82CA49
// Assembly location: M:\7D2D\alpha20.6all\7DaysToDie_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace JBooth.MicroSplat
{
    public class MicroSplatProceduralTextureUtil
    {
        private static float PCFilter(
          int index,
          float height,
          float slope,
          float cavity,
          float flow,
          Vector3 worldPos,
          Vector2 uv,
          Color bMask,
          Color bMask2,
          out int texIndex,
          Vector3 pN,
          MicroSplatProceduralTextureConfig config,
          Texture2D procTexNoise,
          MicroSplatProceduralTextureUtil.NoiseUVMode noiseMode)
        {
            MicroSplatProceduralTextureConfig.Layer layer = config.layers[index];
            Vector2 vector2_1 = uv;
            Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            switch (noiseMode)
            {
                case MicroSplatProceduralTextureUtil.NoiseUVMode.UV:
                    color *= procTexNoise.GetPixelBilinear(worldPos.x * (1f / 500f) * layer.noiseFrequency + layer.noiseOffset, worldPos.z * (1f / 500f) * layer.noiseFrequency + layer.noiseOffset);
                    break;
                case MicroSplatProceduralTextureUtil.NoiseUVMode.World:
                    color = procTexNoise.GetPixelBilinear(vector2_1.x * layer.noiseFrequency + layer.noiseOffset, vector2_1.y * layer.noiseFrequency + layer.noiseOffset);
                    break;
                case MicroSplatProceduralTextureUtil.NoiseUVMode.Triplanar:
                    Vector2 vector2_2 = new Vector2(worldPos.z, worldPos.y) * (1f / 500f) * layer.noiseFrequency + new Vector2(layer.noiseOffset, layer.noiseOffset);
                    Vector2 vector2_3 = new Vector2(worldPos.x, worldPos.z) * (1f / 500f) * layer.noiseFrequency + new Vector2(layer.noiseOffset + 0.31f, layer.noiseOffset + 0.31f);
                    Vector2 vector2_4 = new Vector2(worldPos.x, worldPos.y) * (1f / 500f) * layer.noiseFrequency + new Vector2(layer.noiseOffset + 0.71f, layer.noiseOffset + 0.71f);
                    Color pixelBilinear1 = procTexNoise.GetPixelBilinear(vector2_2.x, vector2_2.y);
                    Color pixelBilinear2 = procTexNoise.GetPixelBilinear(vector2_3.x, vector2_3.y);
                    Color pixelBilinear3 = procTexNoise.GetPixelBilinear(vector2_4.x, vector2_4.y);
                    double x = (double)pN.x;
                    color = pixelBilinear1 * (float)x + pixelBilinear2 * pN.y + pixelBilinear3 * pN.z;
                    break;
            }
            color.r = (float)((double)color.r * 2.0 - 1.0);
            color.g = (float)((double)color.g * 2.0 - 1.0);
            float num1 = layer.heightCurve.Evaluate(height);
            float num2 = layer.slopeCurve.Evaluate(slope);
            float num3 = layer.cavityMapCurve.Evaluate(cavity);
            float num4 = layer.erosionMapCurve.Evaluate(flow);
            float num5 = num1 * (1f + Mathf.Lerp(layer.noiseRange.x, layer.noiseRange.y, color.r));
            float num6 = num2 * (1f + Mathf.Lerp(layer.noiseRange.x, layer.noiseRange.y, color.g));
            float num7 = num3 * (1f + Mathf.Lerp(layer.noiseRange.x, layer.noiseRange.y, color.b));
            float num8 = num4 * (1f + Mathf.Lerp(layer.noiseRange.x, layer.noiseRange.y, color.a));
            if (!layer.heightActive)
                num5 = 1f;
            if (!layer.slopeActive)
                num6 = 1f;
            if (!layer.cavityMapActive)
                num7 = 1f;
            if (!layer.erosionMapActive)
                num8 = 1f;
            bMask *= (Color)layer.biomeWeights;
            bMask2 *= (Color)layer.biomeWeights2;
            float num9 = Mathf.Max(Mathf.Max(Mathf.Max(bMask.r, bMask.g), bMask.b), bMask.a);
            float num10 = Mathf.Max(Mathf.Max(Mathf.Max(bMask2.r, bMask2.g), bMask2.b), bMask2.a);
            texIndex = layer.textureIndex;
            return Mathf.Clamp01(num5 * num6 * num7 * num8 * layer.weight * num9 * num10);
        }

        private static void PCProcessLayer(
          ref Vector4 weights,
          ref MicroSplatProceduralTextureUtil.Int4 indexes,
          ref float totalWeight,
          int curIdx,
          float height,
          float slope,
          float cavity,
          float flow,
          Vector3 worldPos,
          Vector2 uv,
          Color biomeMask,
          Color biomeMask2,
          Vector3 pN,
          MicroSplatProceduralTextureConfig config,
          Texture2D noiseMap,
          MicroSplatProceduralTextureUtil.NoiseUVMode noiseMode)
        {
            int texIndex = 0;
            float b = MicroSplatProceduralTextureUtil.PCFilter(curIdx, height, slope, cavity, flow, worldPos, uv, biomeMask, biomeMask2, out texIndex, pN, config, noiseMap, noiseMode);
            float num = Mathf.Min(totalWeight, b);
            totalWeight -= num;
            if ((double)num > (double)weights.x)
            {
                weights.w = weights.z;
                weights.z = weights.y;
                weights.y = weights.x;
                indexes.w = indexes.z;
                indexes.z = indexes.y;
                indexes.y = indexes.x;
                weights.x = num;
                indexes.x = texIndex;
            }
            else if ((double)num > (double)weights.y)
            {
                weights.w = weights.z;
                weights.z = weights.y;
                indexes.w = indexes.z;
                indexes.z = indexes.y;
                weights.y = num;
                indexes.y = texIndex;
            }
            else if ((double)num > (double)weights.z)
            {
                weights.w = weights.z;
                indexes.w = indexes.z;
                weights.z = num;
                indexes.z = texIndex;
            }
            else
            {
                if ((double)num <= (double)weights.w)
                    return;
                weights.w = num;
                indexes.w = texIndex;
            }
        }

        public static void Sample(
          Vector2 uv,
          Vector3 worldPos,
          Vector3 worldNormal,
          Vector3 up,
          MicroSplatProceduralTextureUtil.NoiseUVMode noiseUVMode,
          Material mat,
          MicroSplatProceduralTextureConfig config,
          out Vector4 weights,
          out MicroSplatProceduralTextureUtil.Int4 indexes)
        {
            weights = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            int count = config.layers.Count;
            Vector2 vector = (Vector2)mat.GetVector("_WorldHeightRange");
            float height = Mathf.Clamp01((worldPos.y - vector.x) / Mathf.Max(0.1f, vector.y - vector.x));
            float slope = 1f - Mathf.Clamp01((float)((double)Vector3.Dot(worldNormal, up) * 0.5 + 0.49000000953674316));
            float cavity = 0.5f;
            float flow = 0.5f;
            Texture2D texture2D1 = mat.HasProperty("_CavityMap") ? (Texture2D)mat.GetTexture("_CavityMap") : (Texture2D)null;
            if ((Object)texture2D1 != (Object)null)
            {
                Color pixelBilinear = texture2D1.GetPixelBilinear(uv.x, uv.y);
                cavity = pixelBilinear.g;
                flow = pixelBilinear.a;
            }
            indexes = new MicroSplatProceduralTextureUtil.Int4();
            indexes.x = 0;
            indexes.y = 1;
            indexes.z = 2;
            indexes.w = 3;
            float totalWeight = 1f;
            Texture2D texture2D2 = mat.HasProperty("_ProcTexBiomeMask") ? (Texture2D)mat.GetTexture("_ProcTexBiomeMask") : (Texture2D)null;
            Texture2D texture2D3 = mat.HasProperty("_ProcTexBiomeMask2") ? (Texture2D)mat.GetTexture("_ProcTexBiomeMask2") : (Texture2D)null;
            Color biomeMask = new Color(1f, 1f, 1f, 1f);
            Color biomeMask2 = new Color(1f, 1f, 1f, 1f);
            if ((Object)texture2D2 != (Object)null)
                biomeMask = texture2D2.GetPixelBilinear(uv.x, uv.y);
            if ((Object)texture2D3 != (Object)null)
                biomeMask2 = texture2D2.GetPixelBilinear(uv.x, uv.y);
            Vector3 pN = new Vector3(0.0f, 0.0f, 0.0f);
            if (noiseUVMode == MicroSplatProceduralTextureUtil.NoiseUVMode.Triplanar)
            {
                Vector3 vector3 = worldNormal;
                vector3.x = Mathf.Abs(vector3.x);
                vector3.y = Mathf.Abs(vector3.y);
                vector3.z = Mathf.Abs(vector3.z);
                pN.x = Mathf.Pow(vector3.x, 4f);
                pN.y = Mathf.Pow(vector3.y, 4f);
                pN.z = Mathf.Pow(vector3.z, 4f);
                float num = pN.x + pN.y + pN.z;
                pN.x /= num;
                pN.y /= num;
                pN.z /= num;
            }
            Texture2D noiseMap = mat.HasProperty("_ProcTexNoise") ? (Texture2D)mat.GetTexture("_ProcTexNoise") : (Texture2D)null;
            for (int curIdx = 0; curIdx < count; ++curIdx)
            {
                MicroSplatProceduralTextureUtil.PCProcessLayer(ref weights, ref indexes, ref totalWeight, curIdx, height, slope, cavity, flow, worldPos, uv, biomeMask, biomeMask2, pN, config, noiseMap, noiseUVMode);
                if ((double)totalWeight <= 0.0)
                    break;
            }
        }

        public enum NoiseUVMode
        {
            UV,
            World,
            Triplanar,
        }

        public struct Int4
        {
            public int x;
            public int y;
            public int z;
            public int w;
        }
    }
}
