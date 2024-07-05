using System;
using UnityEngine;

[Serializable]
public class MeshDescription
{

  public string Name;
  public string Tag;
  // public VoxelMesh.EnumMeshType meshType;
  public bool bCastShadows;
  public bool bReceiveShadows;
  public bool bHasLODs;
  public bool bUseDebugStabilityShader;
  public bool bTerrain;
  public bool bTextureArray;
  public bool bSpecularIsBlack;
  public bool CreateTextureAtlas = true;
  public bool CreateSpecularMap = true;
  public bool CreateNormalMap = true;
  public bool CreateEmissionMap;
  public bool CreateHeightMap;
  public bool CreateOcclusionMap;
  public string MeshLayerName;
  public string ColliderLayerName;
  //public AssetReference PrimaryShader;
  //public AssetReference SecondaryShader;
  //public AssetReference DistantShader;
  //public MeshDescription.EnumRenderMode BlendMode;
  public Material BaseMaterial;
  public string TextureAtlasClass;
  public Texture TexDiffuse;
  public Texture TexNormal;
  public Texture TexSpecular;
  public Texture TexEmission;
  public Texture TexHeight;
  public Texture TexOcclusion;
  public Texture2D TexMask;
  public Texture2D TexMaskNormal;
  public TextAsset MetaData;

  // public TextureAtlas textureAtlas;

}