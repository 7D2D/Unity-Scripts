using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Formats.Fbx.Exporter;

public class MeshBaker_TE : EditorWindow
{
    ObjectField _ObjField;
    Toggle _ExportFbx;
    Toggle _SavePrefab;
    TextField _ExportFbxPath;
    TextField _PrefabsPath;
    Button _BtnBake;

    public string FbxExportPath = "Assets/BakedMeshes/";
    public string PrefabSavePath = "Assets/SavedPrefabs/";
    public const string ExportExt = ".fbx";
    public const string PrefabExt = ".prefab";

    [MenuItem("TE Tools/Mesh Baker")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<MeshBaker_TE>("Mesh Baker");
        wnd.minSize = new Vector2(250f, 100f);
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MeshBakerWdw_TE.uxml");
        VisualElement uxmlElement = visualTree.Instantiate();
        root.Add(uxmlElement);
        _ObjField = root.Q<ObjectField>("objMesh");
        _BtnBake = root.Q<Button>("btnBake");
        _BtnBake.clickable.clickedWithEventInfo += OnBtnClickedBake;
        _ExportFbx = root.Q<Toggle>("toggleExportFbx");
        _SavePrefab = root.Q<Toggle>("toggleSavePrefab");
        _ExportFbxPath = root.Q<TextField>("textfieldFbxExportPath");
        _ExportFbxPath.value = FbxExportPath;
        _PrefabsPath = root.Q<TextField>("textfieldPrefabSavePath");
        _PrefabsPath.value = PrefabSavePath;
    }

    private void OnBtnClickedBake(EventBase evt)
    {
        if (_ObjField.value == null)
        {
            Debug.LogError("Add the desired mesh to the Parent Object field.");
            return;
        }

        BakeMesh(_ObjField.value as GameObject);
    }

    public void BakeMesh(GameObject obj)
    {
        var allMeshes = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (allMeshes == null || allMeshes.Length == 0)
        {
            Debug.LogError("No skinned meshes found on this object. Bake aborted.");
            return;
        }

        Mesh[] bakedMeshes = new Mesh[allMeshes.Length];
        GameObject newBakedMeshGO = new GameObject(obj.name);

        for (int index = 0; index < allMeshes.Length; index++)
        {
            bakedMeshes[index] = new Mesh();
            bakedMeshes[index].name = allMeshes[index].name;
            allMeshes[index].BakeMesh(bakedMeshes[index], false);

            GameObject newMesh = new GameObject(allMeshes[index].name);
            newMesh.AddComponent<MeshFilter>().mesh = bakedMeshes[index];
            newMesh.transform.parent = newBakedMeshGO.transform;
            newMesh.AddComponent<MeshRenderer>().sharedMaterials = allMeshes[index].sharedMaterials;
        }


        ExportAsFBX(newBakedMeshGO);
        SaveAsPrefab(newBakedMeshGO);
    }

    public void ExportAsFBX(GameObject obj)
    {
        if (obj == null || !_ExportFbx.value)
            return;

        var exportPath = _ExportFbxPath.value;
        Directory.CreateDirectory(exportPath);

        string fileName = obj.name + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ExportExt;
        string filePath = Path.Combine(exportPath, fileName);
        string ExportedFilePath = ModelExporter.ExportObject(filePath, obj);
        Debug.Log($"New FBX exported to path: {exportPath}");
    }

    public void SaveAsPrefab(GameObject obj)
    {
        if (obj == null || !_SavePrefab.value)
            return;

        var prefabPath = _PrefabsPath.value;
        Directory.CreateDirectory(prefabPath);

        string localPath = prefabPath + obj.name + PrefabExt;
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.AutomatedAction);
        Debug.Log($"Prefab saved to path: {localPath}");
    }
}

