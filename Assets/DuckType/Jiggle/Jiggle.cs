using UnityEngine;

namespace Assets.DuckType.Jiggle
{
	public class Jiggle : MonoBehaviour
	{

		public bool UpdateWithPhysics;
		public bool UseCenterOfMass = true;
		public Vector3 CenterOfMass = new Vector3(1f, 0.0f, 0.0f);
		public float CenterOfMassInertia = 1f;
		public bool AddWind;
		public Vector3 WindDirection = new Vector3(1f, 0.0f, 0.0f);
		public float WindStrength = 1f;
		public bool AddNoise;
		public float NoiseStrength = 1f;
		public float NoiseScale = 1f;
		public float NoiseSpeed = 1f;
		public float RotationInertia = 1f;
		public float Gravity;
		public float SpringStrength = 0.4f;
		public float Dampening = 0.4f;
		public bool BlendToOriginalRotation;
		public bool Hinge;
		public float HingeAngle;
		public bool UseAngleLimit;
		public float AngleLimit = 180f;
		public bool UseSoftLimit;
		public float SoftLimitInfluence = 0.5f;
		public float SoftLimitStrength = 0.5f;
		public bool ShowViewportGizmos = true;
		public float GizmoScale = 0.1f;

	}
}