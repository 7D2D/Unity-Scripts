using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using System;

namespace PrefabTools_TE
{
    /// <summary>
    /// 
    /// GameObject/Prep Entity for 7 Days
    /// 
    /// This script creates a new menu item when you right click on a prefab in the current scene hierarchy.
    /// The option also exists in the Unity window menu at the top under GameObject -> PrepEntity for 7 Days
    /// 
    /// Ensure you have selected the root game object of the prefab you want to prep
    /// 
    /// NOTE! **** - This script requires the FBX Exporter package to be installed in your Unity project ****
    ///              Go to Window -> Package Manager -> Packages: Unity Registry -> FBX Exporter
    /// 
    /// 
    /// I can be found in Guppycur's Unofficial 7 Days to Die modding community discord
    /// 
    /// Cheers,
    /// 
    /// TormentedEmu (aka V)
    /// 
    /// Discord tag: tormentedemu
    /// Discord invite link: https://discord.gg/DN33qqM7am
    /// 
    /// </summary>
    public class PrefabPrep : Editor
    {
        public class BoneInfo
        {
            public string name;

            public Transform anchor;
            public CharacterJoint joint;
            public BoneInfo parent;

            public float minLimit;
            public float maxLimit;
            public float swingLimit;

            public Vector3 axis;
            public Vector3 normalAxis;

            public float radiusScale;
            public Type colliderType;

            public ArrayList children = new ArrayList();
            public float density;
            public float summedMass;// The mass of this and all children bodies
        }

        public class Defs
        {
            public static readonly string AssetsFolder = "Assets";
            public static readonly string ExportedPrefabsPath = Path.Combine(Application.dataPath, "ExportedPrefabs");
            public static readonly string ExportExt = ".fbx";
            public static readonly string ExportPostFix = "_NewPrefab";
            public static readonly string ZombieFlesh = "ZombieFlesh";
            public static readonly string ZombieFleshFilter = "ZombieFlesh t:physicMaterial";
            public static readonly string PhysicMaterialExt = ".physicMaterial";
            public static readonly string PhysicMaterialFolder = "PhysicMaterial";
            public static readonly string PhysicsMatsPath = "Assets/PhysicMaterial/";
            public static readonly string ColliderName = "Collider";
            public static readonly string IconTag = "IconTag";
            public static readonly string Footsteps = "Footsteps";
            public static readonly string HeadGore = "HeadGore";
            public static readonly string LeftWeapon = "LeftWeapon";
            public static readonly string RightWeapon = "RightWeapon";
            public static readonly string Gunjoint = "Gunjoint";
            public static readonly string Origin = "Origin";
            public static readonly string Head = "Head";
            public static readonly string Hips = "Hips";
            public static readonly string Neck = "Neck";
            public static readonly string MiddleSpine = "Spine1";
            public static readonly string UpperArm = "Arm";
            public static readonly string LowerArm = "ForeArm";
            public static readonly string UpperLeg = "UpLeg";
            public static readonly string LowerLeg = "Leg";
            public static readonly string LeftSide = "Left";
            public static readonly string RightSide = "Right";
            public static readonly List<string> OriginNames = new List<string>(){
            "Origin",
            "origin",
            "Armature",
            "Root",
            "root",
            "Character1_Reference", };

            public static readonly List<string> HipBoneNames = new List<string>() {
            "Hips",
            "mixamorig:Hips",
            "pelvis",
            "Character1_Hips", };

            #region Bone Dictionary
            public static readonly Dictionary<string, string> BoneRenameDict = new Dictionary<string, string>()
            {
                ["pelvis"] = "Hips",
                ["spine"] = "Spine",
                ["spine_01"] = "Spine",
                ["Chest"] = "Spine1",
                ["spine_02"] = "Spine1",
                ["UpperChest"] = "Spine2",
                ["spine_03"] = "Spine2",
                ["neck_01"] = "Neck",
                ["head"] = "Head",
                ["head1"] = "Head",
                ["Head1"] = "Head",
                ["head01"] = "Head",
                ["Head01"] = "Head",
                ["head_1"] = "Head",
                ["Head_1"] = "Head",
                ["head_01"] = "Head",
                ["Head_01"] = "Head",
                ["clavicle_l"] = "LeftShoulder",
                ["LeftUpperArm"] = "LeftArm",
                ["upperarm_l"] = "LeftArm",
                ["LeftLowerArm"] = "LeftForeArm",
                ["lowerarm_l"] = "LeftForeArm",
                ["hand_l"] = "LeftHand",
                ["index_01_l"] = "LeftHandIndex1",
                ["index_02_l"] = "LeftHandIndex2",
                ["index_03_l"] = "LeftHandIndex3",
                ["middle_01_l"] = "LeftHandMiddle1",
                ["middle_02_l"] = "LeftHandMiddle2",
                ["middle_03_l"] = "LeftHandMiddle3",
                ["ring_01_l"] = "LeftHandRing1",
                ["ring_02_l"] = "LeftHandRing2",
                ["ring_03_l"] = "LeftHandRing3",
                ["pinky_01_l"] = "LeftHandPinky1",
                ["pinky_02_l"] = "LeftHandPinky2",
                ["pinky_03_l"] = "LeftHandPinky3",
                ["thumb_01_l"] = "LeftHandThumb1",
                ["thumb_02_l"] = "LeftHandThumb2",
                ["thumb_03_l"] = "LeftHandThumb3",

                ["clavicle_r"] = "RightShoulder",
                ["RightUpperArm"] = "RightArm",
                ["upperarm_r"] = "RightArm",
                ["RightLowerArm"] = "RightForeArm",
                ["lowerarm_r"] = "RightForeArm",
                ["hand_r"] = "RightHand",
                ["index_01_r"] = "RightHandIndex1",
                ["index_02_r"] = "RightHandIndex2",
                ["index_03_r"] = "RightHandIndex3",
                ["middle_01_r"] = "RightHandMiddle1",
                ["middle_02_r"] = "RightHandMiddle2",
                ["middle_03_r"] = "RightHandMiddle3",
                ["ring_01_r"] = "RightHandRing1",
                ["ring_02_r"] = "RightHandRing2",
                ["ring_03_r"] = "RightHandRing3",
                ["pinky_01_r"] = "RightHandPinky1",
                ["pinky_02_r"] = "RightHandPinky2",
                ["pinky_03_r"] = "RightHandPinky3",
                ["thumb_01_r"] = "RightHandThumb1",
                ["thumb_02_r"] = "RightHandThumb2",
                ["thumb_03_r"] = "RightHandThumb3",

                ["LeftUpperLeg"] = "LeftUpLeg",
                ["thigh_l"] = "LeftUpLeg",
                ["LeftLowerLeg"] = "LeftLeg",
                ["calf_l"] = "LeftLeg",
                ["foot_l"] = "LeftFoot",
                ["LeftToes"] = "LeftToeBase",
                ["ball_l"] = "LeftToeBase",
                ["RightUpperLeg"] = "RightUpLeg",
                ["thigh_r"] = "RightUpLeg",
                ["RightLowerLeg"] = "RightLeg",
                ["calf_r"] = "RightLeg",
                ["foot_r"] = "RightFoot",
                ["RightToes"] = "RightToeBase",
                ["ball_r"] = "RightToeBase",
            };
            #endregion
        }

        public class Layers
        {
            public static readonly int LargeEntityBlocker = 19;
        }

        public class Tags
        {
            public static readonly string LargeEntityBlocker = "LargeEntityBlocker";
            public static readonly string Mesh = "E_Mesh";
            public static readonly string Enemy = "E_Enemy";
            public static readonly string BipedRoot = "E_BP_BipedRoot";
            public static readonly string Body = "E_BP_Body";
            public static readonly string Head = "E_BP_Head";
            public static readonly string LeftUpLeg = "E_BP_LLeg";
            public static readonly string LeftLeg = "E_BP_LLowerLeg";
            public static readonly string RightUpLeg = "E_BP_RLeg";
            public static readonly string RightLeg = "E_BP_RLowerLeg";
            public static readonly string LeftArm = "E_BP_LArm";
            public static readonly string LeftForeArm = "E_BP_LLowerArm";
            public static readonly string RightArm = "E_BP_RArm";
            public static readonly string RightForeArm = "E_BP_RLowerArm";
            public static readonly string HeadGore = "L_HeadGore";
            public static readonly string LeftUpperArmGore = "L_LeftUpperArmGore";
            public static readonly string RightUpperArmGore = "L_RightUpperArmGore";
            public static readonly string LeftLowerArmGore = "L_LeftLowerArmGore";
            public static readonly string RightLowerArmGore = "L_RightLowerArmGore";
            public static readonly string LeftUpperLegGore = "L_LeftUpperLegGore";
            public static readonly string RightUpperLegGore = "L_RightUpperLegGore";
            public static readonly string LeftLowerLegGore = "L_LeftLowerLegGore";
            public static readonly string RightLowerLegGore = "L_RightLowerLegGore";
        }

        public class Constants
        {
            public static readonly Vector3 WorldRight = Vector3.right;
            public static readonly Vector3 WorldUp = Vector3.up;
            public static readonly Vector3 WorldForward = Vector3.forward;
            public static PhysicMaterial ZombieFlesh;
        }

        public class Config
        {
            public static bool ExpandBounds = true;
            public static float ExpandBoundsMultiplier = 10f;
            public static float RBTotalMass = 200f;
        }

        public GameObject CurrentPrefab;
        public Transform Origin;
        public Transform Hips;
        public Transform Spine1;
        public Transform Neck;
        public Transform Head;
        public Transform LeftArm;
        public Transform LeftForeArm;
        public Transform LeftHand;
        public Transform LeftUpLeg;
        public Transform LeftLeg;
        public Transform LeftFoot;
        public Transform RightUpLeg;
        public Transform RightLeg;
        public Transform RightFoot;
        public Transform RightArm;
        public Transform RightForeArm;
        public Transform RightHand;
        public Dictionary<Transform, BoneInfo> Bones = new Dictionary<Transform, BoneInfo>();
        public List<BoneInfo> Joints = new List<BoneInfo>();
        public string ExportedFilePath = string.Empty;
        List<SkinnedMeshRenderer> Meshes = new List<SkinnedMeshRenderer>();

        public PrefabPrep()
        {
        }

        public PrefabPrep(GameObject currentSelection)
        {
            CurrentPrefab = currentSelection;
        }

        [MenuItem("GameObject/Prep Entity for 7 Days")]
        public static void PrepEntity()
        {
            Debug.Log($"Starting: Prep Entity");

            var selEntity = Selection.activeGameObject;
            if (selEntity == null)
            {
                Debug.Log("No entity in the scene is currently selected.");
                return;
            }

            Debug.Log($"Selected entity: {selEntity}");

            var prefabParent = selEntity.transform.parent;
            if (prefabParent != null)
            {
                Debug.LogError($"Parent is not null: {prefabParent.name}.  Try again by selecting the root game object of the entity.");
                return;
            }

            FindZombiePhysicMaterial();

            var prefab = ScriptableObject.CreateInstance<PrefabPrep>();
            prefab.Prep(selEntity);

            Debug.Log("Prep Entity complete.");
        }

        public void Prep(GameObject selectedPrefab)
        {
            CurrentPrefab = selectedPrefab;
            CurrentPrefab.tag = Tags.Enemy;
            CheckHierarchy();
            CheckAnimator();
            CheckAttachedScripts();
            ProcessBones();
            CreateRagdoll();
        }

        public void CheckAnimator()
        {
            var animator = CurrentPrefab.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning($"No animator found on selected entity.  Creating new animator component.  Don't forget to add a controller!");
                animator = CurrentPrefab.AddComponent<Animator>();
            }
        }

        public void CheckAttachedScripts()
        {
            var aebScript = CurrentPrefab.GetComponent<AnimationEventBridge>();
            if (aebScript == null)
            {
                Debug.LogWarning($"No AnimationEventBridge found on selected entity. Adding script.");
                aebScript = CurrentPrefab.AddComponent<AnimationEventBridge>();
            }
        }

        /// <summary>
        /// Searches the Currently selected prefab for a direct child descendant
        /// </summary>
        /// <param name="name">The name of the child to search for</param>
        /// <returns>Transform of the child.  null if not found.</returns>
        public Transform FindDirectChild(string name)
        {
            return CurrentPrefab.transform.Find(name);
        }

        public Transform FindOrigin()
        {
            foreach (var name in Defs.OriginNames)
            {
                var origin = FindDirectChild(name);
                if (origin != null)
                {
                    origin.tag = Tags.BipedRoot;
                    return origin;
                }
            }
            return null;
        }

        public Transform FindHipBone()
        {
            Transform bone = null;
            if (Origin != null)
                bone = Origin;
            else
                bone = CurrentPrefab.transform;

            foreach (var hipName in Defs.HipBoneNames)
            {
                Transform hip = bone.Find(hipName);
                if (hip != null) return hip;
            }
            return null;
        }

        public void CheckHierarchy()
        {
            CheckMeshNames();
            CheckSkeleton();
            CheckLargeCollider();
            CheckIconTag();
            CheckFootsteps();
            CheckGoreObjects();
            CheckHandObjects();
            CheckMeshes();
        }

        public void Unpack()
        {
            PrefabUtility.UnpackPrefabInstance(CurrentPrefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }

        public void CheckSkeleton()
        {
            bool needsExport = false;

            Origin = FindOrigin();
            Hips = FindHipBone();
            if (Hips == null)
                throw new Exception("Failed to find skeleton.  Unable to continue preparing the prefab entity.");

            if (Origin == null)
            {
                Origin = new GameObject(Defs.Origin).transform;
                Origin.tag = Tags.BipedRoot;
                Origin.parent = CurrentPrefab.transform;

                Unpack();
                Hips.parent = Origin;
                needsExport = true;
            }

            if (!Origin.name.Equals(Defs.Origin))
            {
                needsExport = true;
                Origin.name = Defs.Origin;
            }

            needsExport |= CheckSkeletonNaming();

            if (needsExport)
            {
                Debug.Log($"Exporting the prefab to a new fbx...");
                var originalName = CurrentPrefab.name;
                var newPrefabName = ExportPrefab();
                AssetRefresh();
                var tmpAsset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ExportedPrefabs/" + newPrefabName);
                GameObject newPrefab = PrefabUtility.InstantiatePrefab(tmpAsset) as GameObject;
                if (newPrefab != null)
                {
                    newPrefab.name = originalName + Defs.ExportPostFix;
                    Debug.Log($"Renamed the prefab to: {newPrefab.name}");
                    Selection.activeGameObject = newPrefab;
                    CurrentPrefab = newPrefab;
                    Origin = FindOrigin();
                    Hips = FindHipBone();
                }
            }
        }

        public bool CheckSkeletonNaming()
        {
            return RenameSkeleton();
        }

        public bool RenameSkeleton()
        {
            bool renamedBone = false;

            List<Transform> allBoneTransforms = Hips.GetComponentsInChildren<Transform>().ToList();

            foreach (var bone in allBoneTransforms)
            {
                if (bone.name.Contains("mixamorig:"))
                {
                    renamedBone = true;
                    bone.name = bone.name.Replace("mixamorig:", "");
                    Debug.Log($"Renaming mixamo bone to: {bone.name}");
                }
                else if (bone.name.Contains("Character1_"))
                {
                    renamedBone = true;
                    bone.name = bone.name.Replace("Character1_", "");
                    Debug.Log($"Renaming Character1_ bone to: {bone.name}");
                }

                if (Defs.BoneRenameDict.ContainsKey(bone.name))
                {
                    renamedBone = true;
                    var newName = Defs.BoneRenameDict[bone.name];
                    Debug.Log($"Renaming {bone.name} to {newName}");
                    bone.name = newName;
                }
            }

            return renamedBone;
        }

        /// <summary>
        /// Will rename all the meshes in this prefab to avoid naming conflicts with any skeleton/joint/bone name
        /// </summary>
        /// <param name="allMeshes"></param>
        public void CheckMeshNames()
        {
            foreach (var mesh in Meshes)
            {
                if (mesh.name.Contains("LOD")) // ignore renaming if the name already contains LOD
                    continue;

                if (!mesh.name.StartsWith("sm")) // add the prefix sm which is short for skinned mesh or static mesh
                    mesh.name = "sm" + mesh.name;
            }
        }

        public void CheckMeshBounds()
        {
            if (!Config.ExpandBounds) return;

            foreach (var mesh in Meshes)
            {
                Bounds b = mesh.bounds;
                b.Expand(Config.ExpandBoundsMultiplier);
                mesh.localBounds = b;
            }
        }

        public void CheckMeshes()
        {
            Meshes = CurrentPrefab.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();

            if (Meshes.Count > 0)
            {
                CheckMeshTags();
                CheckMeshNames();
                CheckMeshBounds();
            }
        }

        private void CheckMeshTags()
        {
            var firstMesh = Meshes.FirstOrDefault(m => m.gameObject.transform.parent != null && m.gameObject.transform.parent == CurrentPrefab.transform);
            if (firstMesh == null)
            {
                Debug.Log($"Moving mesh to game object root transform.");
                Unpack();
                firstMesh = Meshes.FirstOrDefault(m => m.transform.name.ToLower().Contains("body"));
                if (firstMesh == null)
                    firstMesh = Meshes.First();

                firstMesh.transform.parent = CurrentPrefab.transform;
            }

            firstMesh.tag = Tags.Mesh;
        }

        public void ProcessBones()
        {
            List<Transform> allTransforms = new List<Transform>();
            Origin.GetComponentsInChildren<Transform>(true, allTransforms);

            foreach (var child in allTransforms)
            {
                switch (child.name)
                {
                    case "Hips":
                        {
                            child.gameObject.tag = Tags.Body;
                            Hips = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = null;
                            bone.anchor = Hips;
                            bone.density = 2.5F;
                            Bones.TryAdd(Hips, bone);
                        }
                        break;

                    case "LeftUpLeg":
                        {
                            child.gameObject.tag = Tags.LeftUpLeg;
                            LeftUpLeg = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Hips];
                            bone.anchor = LeftUpLeg;
                            Bones.TryAdd(LeftUpLeg, bone);
                        }
                        break;

                    case "LeftLeg":
                        {
                            child.gameObject.tag = Tags.LeftLeg;
                            LeftLeg = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[LeftUpLeg];
                            bone.anchor = LeftLeg;
                            Bones.TryAdd(LeftLeg, bone);
                        }
                        break;

                    case "LeftFoot":
                        {
                            LeftFoot = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[LeftLeg];
                            bone.anchor = LeftFoot;
                            Bones.TryAdd(LeftFoot, bone);
                        }
                        break;

                    case "RightUpLeg":
                        {
                            child.gameObject.tag = Tags.RightUpLeg;
                            RightUpLeg = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Hips];
                            bone.anchor = RightUpLeg;
                            Bones.TryAdd(RightUpLeg, bone);
                        }
                        break;

                    case "RightLeg":
                        {
                            child.gameObject.tag = Tags.RightLeg;
                            RightLeg = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[RightUpLeg];
                            bone.anchor = RightLeg;
                            Bones.TryAdd(RightLeg, bone);
                        }
                        break;

                    case "RightFoot":
                        {
                            RightFoot = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[RightLeg];
                            bone.anchor = RightFoot;
                            Bones.TryAdd(RightFoot, bone);
                        }
                        break;

                    case "Spine1":
                        {
                            child.gameObject.tag = Tags.Body;
                            Spine1 = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Hips];
                            bone.anchor = Spine1;
                            Bones.TryAdd(Spine1, bone);
                        }
                        break;

                    case "HeadGore":
                        {
                            child.gameObject.tag = Tags.HeadGore;
                        }
                        break;

                    case "LeftArm":
                        {
                            child.gameObject.tag = Tags.LeftArm;
                            LeftArm = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Spine1];
                            bone.anchor = LeftArm;
                            Bones.TryAdd(LeftArm, bone);
                        }
                        break;

                    case "LeftForeArm":
                        {
                            child.gameObject.tag = Tags.LeftForeArm;
                            LeftForeArm = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[LeftArm];
                            bone.anchor = LeftForeArm;
                            Bones.TryAdd(LeftForeArm, bone);
                        }
                        break;


                    case "LeftHand":
                        {
                            LeftHand = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[LeftForeArm];
                            bone.anchor = LeftHand;
                            Bones.TryAdd(LeftHand, bone);
                        }
                        break;

                    case "RightArm":
                        {
                            child.gameObject.tag = Tags.RightArm;
                            RightArm = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Spine1];
                            bone.anchor = RightArm;
                            Bones.TryAdd(RightArm, bone);
                        }
                        break;

                    case "RightForeArm":
                        {
                            child.gameObject.tag = Tags.RightForeArm;
                            RightForeArm = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[RightArm];
                            bone.anchor = RightForeArm;
                            Bones.TryAdd(RightForeArm, bone);
                        }
                        break;

                    case "RightHand":
                        {
                            RightHand = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[RightForeArm];
                            bone.anchor = RightHand;
                            Bones.TryAdd(RightHand, bone);
                        }
                        break;

                    case "Neck":
                        {
                            Neck = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.anchor = Neck;
                            Bones.TryAdd(Neck, bone);
                        }
                        break;

                    case "Head":
                        {
                            child.gameObject.tag = Tags.Head;
                            Head = child.transform;
                            BoneInfo bone = new BoneInfo();
                            bone.name = child.name;
                            bone.parent = Bones[Spine1];
                            bone.anchor = Head;
                            Bones.TryAdd(Head, bone);
                        }
                        break;
                }
            }
        }

        public string ExportPrefab()
        {
            if (CurrentPrefab == null)
                return string.Empty;

            Directory.CreateDirectory(Defs.ExportedPrefabsPath);

            string fileName = CurrentPrefab.name + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Defs.ExportExt;
            string filePath = Path.Combine(Defs.ExportedPrefabsPath, fileName);
            ExportedFilePath = ModelExporter.ExportObject(filePath, CurrentPrefab);
            Debug.Log($"New Prefab exported to path: {ExportedFilePath}");
            return fileName;
        }

        public void AssetRefresh()
        {
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ImportRecursive);
        }

        public void CheckLargeCollider()
        {
            var largeCol = FindDirectChild(Defs.ColliderName);
            if (largeCol == null)
            {
                Debug.Log($"Creating Large entity collider object");
                largeCol = new GameObject(Defs.ColliderName).transform;
                largeCol.parent = CurrentPrefab.transform;
            }

            var capCol = largeCol.GetComponent<CapsuleCollider>();
            if (capCol == null)
            {
                capCol = CreateLargeEntityBlocker(largeCol.gameObject);
            }
            capCol.tag = Tags.LargeEntityBlocker;
            capCol.gameObject.layer = Layers.LargeEntityBlocker;
        }

        public CapsuleCollider CreateLargeEntityBlocker(GameObject colliderObject)
        {
            var capCol = colliderObject.AddComponent<CapsuleCollider>();
            capCol.center = new Vector3(0f, 1f, 0f);
            capCol.height = 1.6f;
            capCol.radius = 0.2f;
            return capCol;
        }

        public void CheckIconTag()
        {
            var iconTagTransform = FindDirectChild(Defs.IconTag);
            if (iconTagTransform == null)
            {
                Debug.Log($"Creating {Defs.IconTag} object");
                iconTagTransform = new GameObject(Defs.IconTag).transform;
                iconTagTransform.SetPositionAndRotation(new Vector3(0f, 2f, 0f), Quaternion.identity);
                iconTagTransform.parent = CurrentPrefab.transform;
            }
        }

        public void CheckFootsteps()
        {
            var footsteps = Origin.Find(Defs.Footsteps);
            if (footsteps == null)
            {
                Debug.Log($"Creating {Defs.Footsteps} object");
                footsteps = new GameObject(Defs.Footsteps).transform;
                footsteps.SetPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                footsteps.parent = Origin;
            }
        }

        public void CheckGoreObjects()
        {
            var spine2 = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2");
            if (spine2 != null)
            {
                var headGore = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/HeadGore");
                if (headGore == null)
                {
                    headGore = new GameObject(Defs.HeadGore).transform;
                    headGore.parent = spine2;
                    headGore.localPosition = Vector3.zero;
                    headGore.localRotation = Quaternion.identity;
                }
            }
        }

        public void CheckHandObjects()
        {
            var leftHand = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
            if (leftHand != null)
            {
                var leftWeapon = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand/LeftWeapon");
                if (leftWeapon == null)
                {
                    leftWeapon = new GameObject(Defs.LeftWeapon).transform;
                    leftWeapon.parent = leftHand;
                    leftWeapon.localPosition = Vector3.zero;
                    leftWeapon.localRotation = Quaternion.identity;
                }
            }

            var rightHand = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
            if (rightHand != null)
            {
                var rightWeapon = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand/RightWeapon");
                if (rightWeapon == null)
                {
                    rightWeapon = new GameObject(Defs.RightWeapon).transform;
                    rightWeapon.parent = rightHand;
                    rightWeapon.localPosition = Vector3.zero;
                    rightWeapon.localRotation = Quaternion.identity;
                }

                var gunjoint = FindDirectChild("Origin/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand/Gunjoint");
                if (gunjoint == null)
                {
                    gunjoint = new GameObject(Defs.Gunjoint).transform;
                    gunjoint.parent = rightHand;
                    gunjoint.localPosition = Vector3.zero;
                    gunjoint.localRotation = Quaternion.identity;
                }
            }
        }

        public void CreateRagdoll()
        {
            CleanupBones();

            AddHipsCollider();
            AddSpineCollider();
            
            AddHeadCollider();

            AddCapsuleCollider(LeftArm, LeftForeArm, 0.11f, Tags.LeftArm);
            AddCapsuleCollider(RightArm, RightForeArm, 0.11f, Tags.RightArm);

            AddCapsuleCollider(LeftForeArm, LeftHand, 0.1f, Tags.LeftForeArm);
            AddCapsuleCollider(RightForeArm, RightHand, 0.1f, Tags.RightForeArm);

            AddCapsuleCollider(LeftUpLeg, LeftLeg, 0.11f, Tags.LeftUpLeg);
            AddCapsuleCollider(RightUpLeg, RightLeg, 0.11f, Tags.RightUpLeg);

            AddCapsuleCollider(LeftLeg, LeftFoot, 0.1f, Tags.LeftLeg);
            AddCapsuleCollider(RightLeg, RightFoot, 0.1f, Tags.RightLeg);

            PrepareJoints();
            BuildBodies(); // build rigidbodies
            BuildJoints(); // build character joints
            CalculateMass(); // redistribute mass across all rigidbodies
            
        }

        public void AddHeadCollider()
        {
            if (Head.GetComponent<Collider>())
                DestroyImmediate(Head.GetComponent<Collider>());

            float radius = Vector3.Distance(LeftArm.position, RightArm.position);
            radius /= 4.5f;

            CapsuleCollider cap = Undo.AddComponent<CapsuleCollider>(Head.gameObject);
            cap.radius = radius;
            cap.center = Vector3.zero;
            cap.direction = 1;
            cap.height = GetDistance(Head.position, Neck.position) * 2;
            cap.material = Constants.ZombieFlesh;
        }

        public void AddCapsuleCollider(Transform boneFrom, Transform boneTo, float _radius, string _tag)
        {
            float distanceAB = Vector3.Distance(boneFrom.position, boneTo.position);
            var cCol = boneFrom.gameObject.AddComponent<CapsuleCollider>();
            cCol.height = distanceAB;
            cCol.radius = _radius;
            cCol.material = Constants.ZombieFlesh;
            cCol.tag = _tag;
            cCol.center = new Vector3(0f, distanceAB * 0.5f, 0f);
        }

        public void AddCapsuleColliderChild(Transform boneFrom, Transform boneTo, float _radius, string _tag)
        {
            Vector3 directionAB = boneTo.position - boneFrom.position;
            Vector3 midpoint = GetHalfwayPos(boneFrom.position, boneTo.position);
            float distanceAB = Vector3.Distance(boneFrom.position, boneTo.position);
            Transform capsuleTransform = new GameObject(Defs.ColliderName, new System.Type[] { typeof(CapsuleCollider) }).transform;
            capsuleTransform.position = midpoint;
            capsuleTransform.tag = _tag;
            var cCol = capsuleTransform.GetComponent<CapsuleCollider>();
            cCol.height = distanceAB;
            cCol.radius = _radius;
            cCol.material = Constants.ZombieFlesh;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, directionAB.normalized);
            capsuleTransform.rotation = rotation * capsuleTransform.rotation;
            capsuleTransform.parent = boneFrom;
            capsuleTransform.localScale = Vector3.one;
        }

        public void CleanupBones()
        {
            foreach (KeyValuePair<Transform, BoneInfo> kvp in Bones)
            {
                Transform transform = kvp.Key;
                BoneInfo bone = kvp.Value;

                if (!bone.anchor)
                    continue;

                Component[] joints = bone.anchor.GetComponentsInChildren(typeof(Joint));
                foreach (Joint joint in joints)
                    Undo.DestroyObjectImmediate(joint);

                Component[] bodies = bone.anchor.GetComponentsInChildren(typeof(Rigidbody));
                foreach (Rigidbody body in bodies)
                    Undo.DestroyObjectImmediate(body);

                Component[] colliders = bone.anchor.GetComponentsInChildren(typeof(Collider));
                foreach (Collider collider in colliders)
                    Undo.DestroyObjectImmediate(collider);
            }
        }

        public void BuildBodies()
        {
            foreach (BoneInfo bone in Joints)
            {
                if (bone.anchor == null)
                {
                    Debug.LogError($"Bone anchor is null: {bone.name}");
                    throw new Exception("Error");
                }

                var rb = Undo.AddComponent<Rigidbody>(bone.anchor.gameObject);
                rb.mass = bone.density;
            }
        }

        public void PrepareJoints()
        {
            AddMirroredJoint(Defs.UpperLeg, LeftUpLeg, RightUpLeg, Defs.Hips, Constants.WorldRight, Constants.WorldForward, -20, 70, 30, typeof(CapsuleCollider), 0.3F, 1.5F);
            AddMirroredJoint(Defs.LowerLeg, LeftLeg, RightLeg, Defs.UpperLeg, Constants.WorldRight, Constants.WorldForward, -80, 0, 0, typeof(CapsuleCollider), 0.25F, 1.5F);

            AddJoint(Defs.MiddleSpine, Spine1, Defs.Hips, Constants.WorldRight, Constants.WorldForward, -20, 20, 10, typeof(BoxCollider), 1, 2.5F);

            AddMirroredJoint(Defs.UpperArm, LeftArm, RightArm, Defs.MiddleSpine, Constants.WorldUp, Constants.WorldForward, -70, 10, 50, typeof(CapsuleCollider), 0.25F, 1.0F);
            AddMirroredJoint(Defs.LowerArm, LeftForeArm, RightForeArm, Defs.UpperArm, Constants.WorldForward, Constants.WorldUp, -90, 0, 0, typeof(CapsuleCollider), 0.20F, 1.0F);

            AddJoint(Defs.Head, Head, Defs.MiddleSpine, Constants.WorldRight, Constants.WorldForward, -40, 25, 25, typeof(CapsuleCollider), 1, 1.0F);

            Joints.Add(Bones[Hips]);
        }

        public BoneInfo FindBone(string name)
        {
            foreach (KeyValuePair<Transform, BoneInfo> kvp in Bones)
            {
                BoneInfo bone = kvp.Value;

                if (bone.name == name)
                {
                    return bone;
                }
            }

            return null;
        }

        public void AddMirroredJoint(string name, Transform leftAnchor, Transform rightAnchor, string parent, Vector3 worldTwistAxis, Vector3 worldSwingAxis, float minLimit, float maxLimit, float swingLimit, Type colliderType, float radiusScale, float density)
        {
            AddJoint(Defs.LeftSide + name, leftAnchor, parent, worldTwistAxis, worldSwingAxis, minLimit, maxLimit, swingLimit, colliderType, radiusScale, density);
            AddJoint(Defs.RightSide + name, rightAnchor, parent, worldTwistAxis, worldSwingAxis, minLimit, maxLimit, swingLimit, colliderType, radiusScale, density);
        }

        public void AddJoint(string name, Transform anchor, string parent, Vector3 worldTwistAxis, Vector3 worldSwingAxis, float minLimit, float maxLimit, float swingLimit, Type colliderType, float radiusScale, float density)
        {
            BoneInfo bone = new BoneInfo();
            bone.name = name;
            bone.anchor = anchor;
            bone.axis = worldTwistAxis;
            bone.normalAxis = worldSwingAxis;
            bone.minLimit = minLimit;
            bone.maxLimit = maxLimit;
            bone.swingLimit = swingLimit;
            bone.density = density;
            bone.colliderType = colliderType;
            bone.radiusScale = radiusScale;

            if (FindBone(parent) != null)
                bone.parent = FindBone(parent);
            else if (name.StartsWith(Defs.LeftSide))
                bone.parent = FindBone(Defs.LeftSide + parent);
            else if (name.StartsWith(Defs.RightSide))
                bone.parent = FindBone(Defs.RightSide + parent);


            if (bone.parent == null)
                return;

            bone.parent.children.Add(bone);
            Joints.Add(bone);
        }

        public void BuildJoints()
        {
            foreach (BoneInfo bone in Joints)
            {
                if (bone.parent == null)
                    continue;

                CharacterJoint joint = Undo.AddComponent<CharacterJoint>(bone.anchor.gameObject);
                bone.joint = joint;

                // Setup connection and axis
                joint.axis = CalculateDirectionAxis(bone.anchor.InverseTransformDirection(bone.axis));
                joint.swingAxis = CalculateDirectionAxis(bone.anchor.InverseTransformDirection(bone.normalAxis));
                joint.anchor = Vector3.zero;
                joint.connectedBody = bone.parent.anchor.GetComponent<Rigidbody>();
                joint.enablePreprocessing = false; // turn off to handle degenerated scenarios, like spawning inside geometry.

                // Setup limits
                SoftJointLimit limit = new SoftJointLimit();
                limit.contactDistance = 0; // default to zero, which automatically sets contact distance.

                limit.limit = bone.minLimit;
                joint.lowTwistLimit = limit;

                limit.limit = bone.maxLimit;
                joint.highTwistLimit = limit;

                limit.limit = bone.swingLimit;
                joint.swing1Limit = limit;

                limit.limit = 0;
                joint.swing2Limit = limit;
            }
        }

        public void CalculateMassRecurse(BoneInfo bone)
        {
            float mass = bone.anchor.GetComponent<Rigidbody>().mass;

            foreach (BoneInfo child in bone.children)
            {
                CalculateMassRecurse(child);
                mass += child.summedMass;
            }

            bone.summedMass = mass;
        }

        public void CalculateMass()
        {
            // Calculate allChildMass by summing all bodies
            BoneInfo hipsBone = Bones[Hips];

            CalculateMassRecurse(hipsBone);

            // Rescale the mass so that the whole character weights totalMass
            float massScale = Config.RBTotalMass / hipsBone.summedMass;

            foreach (BoneInfo bone in Joints)
            {
                bone.anchor.GetComponent<Rigidbody>().mass *= massScale;
            }

            // Recalculate allChildMass by summing all bodies
            CalculateMassRecurse(hipsBone);
        }

        public Bounds GetBreastBounds(Transform relativeTo)
        {
            // Pelvis bounds
            Bounds bounds = new Bounds();
            bounds.Encapsulate(relativeTo.InverseTransformPoint(LeftUpLeg.position));
            bounds.Encapsulate(relativeTo.InverseTransformPoint(RightUpLeg.position));
            bounds.Encapsulate(relativeTo.InverseTransformPoint(LeftArm.position));
            bounds.Encapsulate(relativeTo.InverseTransformPoint(RightArm.position));
            Vector3 size = bounds.size;
            size[SmallestComponent(bounds.size)] = size[LargestComponent(bounds.size)] / 2.0F;
            bounds.size = size;
            return bounds;
        }

        public void AddHipsCollider()
        {
            var boxCol = Hips.gameObject.AddComponent<BoxCollider>();
            boxCol.tag = Tags.Body;
            Bounds bounds = new Bounds();
            bounds.Encapsulate(Hips.InverseTransformPoint(new Vector3(LeftArm.position.x, Spine1.position.y, LeftArm.position.z)));
            bounds.Encapsulate(Hips.InverseTransformPoint(new Vector3(RightArm.position.x, Spine1.position.y, RightArm.position.z)));
            bounds.Encapsulate(Hips.InverseTransformPoint(new Vector3(LeftArm.position.x, LeftUpLeg.position.y, LeftArm.position.z)));
            bounds.Encapsulate(Hips.InverseTransformPoint(new Vector3(RightArm.position.x, RightUpLeg.position.y, RightArm.position.z)));
            Vector3 size = bounds.size;
            size.z = size.x / 1.6F;
            size.x *= 0.9f;
            boxCol.size = size;
            boxCol.material = Constants.ZombieFlesh;
            boxCol.center = bounds.center;
        }

        public void AddSpineCollider()
        {
            float len = GetDistance(Spine1.position, Neck.position);
            var boxCol = Spine1.gameObject.AddComponent<BoxCollider>();// boxTransform.GetComponent<BoxCollider>();
            Bounds bounds = new Bounds();
            bounds.Encapsulate(Spine1.InverseTransformPoint(new Vector3(LeftArm.position.x, Neck.position.y, LeftArm.position.z)));
            bounds.Encapsulate(Spine1.InverseTransformPoint(new Vector3(RightArm.position.x, Neck.position.y, RightArm.position.z)));
            Vector3 size = bounds.size;
            size.z = size.x / 1.5F;
            boxCol.tag = Tags.Body;
            boxCol.size = size;
            boxCol.material = Constants.ZombieFlesh;
            boxCol.center = new Vector3(0f, len * 0.5f, 0f);
        }

        public void AddBreastColliders()
        {
            // Middle spine/Spine1 and pelvis/Hips
            if (Spine1 != null && Hips != null)
            {
                Bounds bounds;
                BoxCollider box;

                // Hips
                bounds = Clip(GetBreastBounds(Hips), Hips, Spine1, false);
                box = Undo.AddComponent<BoxCollider>(Hips.gameObject);
                box.center = bounds.center;
                box.size = bounds.size;
                box.material = Constants.ZombieFlesh;

                // Middle spine bounds
                bounds = Clip(GetBreastBounds(Spine1), Spine1, Spine1, true);
                bounds.Encapsulate(Spine1.InverseTransformPoint(Neck.position));
                box = Undo.AddComponent<BoxCollider>(Spine1.gameObject);
                box.center = bounds.center;
                box.size = bounds.size;
                box.material = Constants.ZombieFlesh;
            }
            // Only pelvis/Hips
            else
            {
                Bounds bounds = new Bounds();
                bounds.Encapsulate(Hips.InverseTransformPoint(LeftUpLeg.position));
                bounds.Encapsulate(Hips.InverseTransformPoint(RightUpLeg.position));
                bounds.Encapsulate(Hips.InverseTransformPoint(Neck.position));
                bounds.Encapsulate(Hips.InverseTransformPoint(Neck.position));

                Vector3 size = bounds.size;
                size[SmallestComponent(bounds.size)] = size[LargestComponent(bounds.size)] / 2.0F;

                BoxCollider box = Undo.AddComponent<BoxCollider>(Hips.gameObject);
                box.center = bounds.center;
                box.size = size;
                box.material = Constants.ZombieFlesh;
            }
        }

        public static void CalculateDirection(Vector3 point, out int direction, out float distance)
        {
            // Calculate longest axis
            direction = 0;
            if (Mathf.Abs(point[1]) > Mathf.Abs(point[0]))
                direction = 1;
            if (Mathf.Abs(point[2]) > Mathf.Abs(point[direction]))
                direction = 2;

            distance = point[direction];
        }

        public static Vector3 CalculateDirectionAxis(Vector3 point)
        {
            int direction = 0;
            float distance;
            CalculateDirection(point, out direction, out distance);
            Vector3 axis = Vector3.zero;
            if (distance > 0)
                axis[direction] = 1.0F;
            else
                axis[direction] = -1.0F;
            return axis;
        }

        public static int SmallestComponent(Vector3 point)
        {
            int direction = 0;
            if (Mathf.Abs(point[1]) < Mathf.Abs(point[0]))
                direction = 1;
            if (Mathf.Abs(point[2]) < Mathf.Abs(point[direction]))
                direction = 2;
            return direction;
        }

        public static int LargestComponent(Vector3 point)
        {
            int direction = 0;
            if (Mathf.Abs(point[1]) > Mathf.Abs(point[0]))
                direction = 1;
            if (Mathf.Abs(point[2]) > Mathf.Abs(point[direction]))
                direction = 2;
            return direction;
        }

        public static Bounds Clip(Bounds bounds, Transform relativeTo, Transform clipTransform, bool below)
        {
            int axis = LargestComponent(bounds.size);

            if (Vector3.Dot(Constants.WorldUp, relativeTo.TransformPoint(bounds.max)) > Vector3.Dot(Constants.WorldUp, relativeTo.TransformPoint(bounds.min)) == below)
            {
                Vector3 min = bounds.min;
                min[axis] = relativeTo.InverseTransformPoint(clipTransform.position)[axis];
                bounds.min = min;
            }
            else
            {
                Vector3 max = bounds.max;
                max[axis] = relativeTo.InverseTransformPoint(clipTransform.position)[axis];
                bounds.max = max;
            }
            return bounds;
        }

        public static Vector3 GetHalfwayPos(Vector3 a, Vector3 b)
        {
            return Vector3.Lerp(a, b, 0.5f);
        }

        public static float GetDistance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        public static void FindZombiePhysicMaterial()
        {
            if (Constants.ZombieFlesh == null)
            {
                if (!AssetDatabase.IsValidFolder(Defs.PhysicsMatsPath))
                    AssetDatabase.CreateFolder(Defs.AssetsFolder, Defs.PhysicMaterialFolder);

                var zFleshMats = AssetDatabase.FindAssets(Defs.ZombieFleshFilter, new[] { Defs.PhysicsMatsPath });

                if (zFleshMats == null || zFleshMats.Length == 0)
                {
                    Constants.ZombieFlesh = new PhysicMaterial(Defs.ZombieFlesh)
                    {
                        dynamicFriction = 2.24f,
                        staticFriction = 1.44f,
                        bounciness = 0.18f,
                        frictionCombine = PhysicMaterialCombine.Multiply,
                        bounceCombine = PhysicMaterialCombine.Average
                    };

                    AssetDatabase.CreateAsset(Constants.ZombieFlesh, Defs.PhysicsMatsPath + Defs.ZombieFlesh + Defs.PhysicMaterialExt);
                    Debug.Log($"Created a new PhysicMaterial asset: {Defs.ZombieFlesh}");
                    return;
                }

                foreach (var f in zFleshMats)
                {
                    Constants.ZombieFlesh = AssetDatabase.LoadAssetAtPath<PhysicMaterial>(AssetDatabase.GUIDToAssetPath(f));
                    if (Constants.ZombieFlesh != null)
                        return;
                }
            }
        }
    }

}
