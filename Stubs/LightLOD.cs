using UnityEngine;

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
	public bool isHeld;
	public Light otherLight;
	public bool EmissiveFromLightColorOn;
	public Color EmissiveColor = Color.white;
	public float StateRate = 1f;
	public float FluxDelay = 1f;
	public LightStateType lightStateStart;
	public bool lightStateEnabled;
	public float priority;

}