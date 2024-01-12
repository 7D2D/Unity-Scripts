using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

//https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildCompression.html

//Create a folder (right click in the Assets folder and go to Create>Folder), and name it “Editor” if it doesn’t already exist
//Place this script in the Editor folder

public static class MultiPlatformExportAssetBundles //Modified by Dayuppy - Based off the original by Sphereii/Xyth - https://github.com/7D2D/Templates-and-Utilities/blob/29758ef38db5dc291004c3b5facce826a45b6df9/MultiPlatformExportAssetBundles.zip
{
    [MenuItem("Assets/Build Unity3D AssetBundle From Selection/LZ4 Chunk-Compressed (Recommended) - Medium Compression - Fast to Load")]
    private static void ExportResource01()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if(path.Length != 0)
        {
			// Include the following Graphic APIs:
            PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64, new GraphicsDeviceType[] { GraphicsDeviceType.Direct3D11, GraphicsDeviceType.OpenGLCore, GraphicsDeviceType.Vulkan });

			// Build the resource file from the active selection:
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

			#pragma warning disable CS0618 // Type or member is obsolete: Thanks to Eric Beaudoin for this bit
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
            Selection.objects = selection;
			#pragma warning restore CS0618
        }
    }

    [MenuItem("Assets/Build Unity3D AssetBundle From Selection/LZMA Compressed (Not Recommended) - Highest Compression - Slowest to Load")] //Added by Dayuppy - Recommended Option
    private static void ExportResource02()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if(path.Length != 0)
        {
            PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64, new GraphicsDeviceType[] { GraphicsDeviceType.Direct3D11, GraphicsDeviceType.OpenGLCore, GraphicsDeviceType.Vulkan });

            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

			#pragma warning disable CS0618
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
            Selection.objects = selection;
			#pragma warning restore CS0618
        }
    }

    [MenuItem("Assets/Build Unity3D AssetBundle From Selection/Uncompressed (Not Recommended) - No Compression - Fast to Load")]
    private static void ExportResource03()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if(path.Length != 0)
        {
            PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64, new GraphicsDeviceType[] { GraphicsDeviceType.Direct3D11, GraphicsDeviceType.OpenGLCore, GraphicsDeviceType.Vulkan });

            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

			#pragma warning disable CS0618
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64);
            Selection.objects = selection;
			#pragma warning restore CS0618
        }
    }
}
