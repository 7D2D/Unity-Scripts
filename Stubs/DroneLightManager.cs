using System;
using UnityEngine;

public class DroneLightManager : MonoBehaviour
{
    // Token: 0x06001C4D RID: 7245 RVA: 0x000BF880 File Offset: 0x000BDA80
    public void InitMaterials(string key)
    {
        DroneLightManager.LightEffect lightEffect = this.getLightEffect(key);
        if (lightEffect == null)
        {
            Debug.LogWarning("Failed to find drone light with name: " + key, this);
            return;
        }
        for (int i = 0; i < lightEffect.linkedObjects.Length; i++)
        {
            lightEffect.linkedObjects[i].SetActive(true);
        }
        for (int j = 0; j < base.transform.childCount; j++)
        {
            SkinnedMeshRenderer component = base.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>();
            if (component)
            {
                Material[] materials = component.materials;
                for (int k = materials.Length - 1; k >= 0; k--)
                {
                    if (materials[k].name.Replace(" (Instance)", "") == lightEffect.material.name)
                    {
                        materials[k].SetColor("_EmissionColor", lightEffect.material.GetColor("_EmissionColor"));
                        break;
                    }
                }
            }
        }
    }

    // Token: 0x06001C4E RID: 7246 RVA: 0x000BF96C File Offset: 0x000BDB6C
    public void DisableMaterials(string key)
    {
        DroneLightManager.LightEffect lightEffect = this.getLightEffect(key);
        if (lightEffect == null)
        {
            Debug.LogWarning("Failed to find drone light with name: " + key, this);
            return;
        }
        for (int i = 0; i < lightEffect.linkedObjects.Length; i++)
        {
            lightEffect.linkedObjects[i].SetActive(false);
        }
        for (int j = 0; j < base.transform.childCount; j++)
        {
            SkinnedMeshRenderer component = base.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>();
            if (component)
            {
                Material[] materials = component.materials;
                for (int k = materials.Length - 1; k >= 0; k--)
                {
                    if (materials[k].name.Replace(" (Instance)", "") == lightEffect.material.name)
                    {
                        materials[k].SetColor("_EmissionColor", Color.black);
                        break;
                    }
                }
            }
        }
    }

    // Token: 0x06001C4F RID: 7247 RVA: 0x000BFA48 File Offset: 0x000BDC48
    private DroneLightManager.LightEffect getLightEffect(string key)
    {
        for (int i = 0; i < this.LightEffects.Length; i++)
        {
            if (this.LightEffects[i].material.name == key)
            {
                return this.LightEffects[i];
            }
        }
        return null;
    }

    // Token: 0x0400109E RID: 4254
    public DroneLightManager.LightEffect[] LightEffects; 
    //public DroneLightManager.LightEffect[] LightEffects = new DroneLightManager.LightEffect[];
    // Token: 0x02000F40 RID: 3904
    [Serializable]
    public class LightEffect
    {
        // Token: 0x04006DA2 RID: 28066
        public bool startsOn;

        // Token: 0x04006DA3 RID: 28067
        public Material material;

        // Token: 0x04006DA4 RID: 28068
        public GameObject[] linkedObjects;
    }
}