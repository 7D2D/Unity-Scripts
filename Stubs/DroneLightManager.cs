using System;
using UnityEngine;

public class DroneLightManager : MonoBehaviour
{

	[Serializable]
	public class LightEffect
	{
		public bool startsOn;
		public Material material;
		public GameObject[] linkedObjects;
	}

	public LightEffect[] LightEffects;

}