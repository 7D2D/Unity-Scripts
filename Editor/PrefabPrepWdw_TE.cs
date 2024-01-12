using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using PrefabTools_TE;

public class PrefabPrepWdw_TE : EditorWindow
{
    ObjectField _PrefabField;
    Button _btnBeginPrep;
    Toggle _ExpandBounds;
    TextField _BoundsMultiplier;
    TextField _RagdollTotalMass;

    [MenuItem("TE Tools/Prefab Prep")]
    public static void ShowWindow()
    {
        PrefabPrepWdw_TE wnd = GetWindow<PrefabPrepWdw_TE>();
        wnd.titleContent = new GUIContent("Prefab Prep");
        wnd.minSize = new Vector2(250f, 250f);
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/PrefabPrepWdw_TE.uxml");
        VisualElement eleFromUXML = visualTree.Instantiate();
        _btnBeginPrep = eleFromUXML.Q<Button>("btnBeginPrep");
        _btnBeginPrep.clickable.clickedWithEventInfo += OnBtnClickedPrep;
        _btnBeginPrep.SetEnabled(false);
        _PrefabField = eleFromUXML.Q<ObjectField>("objSelectedPrefab");
        _ExpandBounds = eleFromUXML.Q<Toggle>("toggleExpandBounds");
        _BoundsMultiplier = eleFromUXML.Q<TextField>("textBoundsMultiplier");
        _RagdollTotalMass = eleFromUXML.Q<TextField>("textRagdollMass");

        root.Add(eleFromUXML);
    }

    private void OnBtnClickedPrep(EventBase eventBase)
    {
        if (_PrefabField.value == null) return;

        PrefabPrep.Config.ExpandBounds = _ExpandBounds.value;
        float boundsMultiplier;
        if (!float.TryParse(_BoundsMultiplier.value, out boundsMultiplier))
            boundsMultiplier = 10f;

        PrefabPrep.Config.ExpandBoundsMultiplier = boundsMultiplier;

        float totalMass;
        if (!float.TryParse(_RagdollTotalMass.value, out totalMass))
            totalMass = 200f;

        PrefabPrep.Config.RBTotalMass = totalMass;

        PrefabPrep.PrepEntity();
    }

    private void Update()
    {
        _PrefabField.schedule.Execute(() =>
        {
            if (Selection.activeGameObject != null && Selection.activeGameObject.activeInHierarchy)
            {
                var curGO = Selection.activeGameObject;
                if (curGO.transform.parent == null)
                {
                    _PrefabField.value = curGO;
                    _btnBeginPrep.SetEnabled(true);
                    return;
                }
            }

            _PrefabField.value = null;
            _btnBeginPrep.SetEnabled(false);
        }).Every(100);
    }
}