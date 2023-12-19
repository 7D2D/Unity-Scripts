using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightLOD : MonoBehaviour
{
    public GameObject LitRootObject;
    public Transform[] RefIlluminatedMaterials;
    public Transform RefFlare;
    public float MaxDistance = 30f;
    public float DistanceScale = 1f;
    public float FlareBrightnessFactor = 1f;
    public bool bPlayerPlacedLight;
    public bool bSwitchedOn;
    public bool bToggleable = true;
    public Light otherLight;
    public Color EmissiveColor = Color.white;
    public float StateRate = 1f;
    public float FluxDelay = 1f;
    public bool lightStateEnabled;
    public float priority;
}
