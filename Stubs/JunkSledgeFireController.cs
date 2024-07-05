using System;
using UnityEngine;

public class JunkSledgeFireController : MiniTurretFireController
{

	[Serializable]
	public enum ArmStates
	{
		Idle,
		Extending,
		Retracting,
	}

	public ArmStates ArmState;
	public Transform Arm1;
	public float Arm1StartZ;
	public float Arm1EndZ;
	public Transform Arm2;
	public float Arm2StartZ;
	public float Arm2EndZ;
	public float ExtentionTime;
	public float RetractionTime;

}