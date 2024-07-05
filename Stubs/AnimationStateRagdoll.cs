using UnityEngine;

public class AnimationStateRagdoll : ScriptableObject
{

	public DynamicRagdollFlags RagdollFlags;

	[Tooltip("Time period to stun")]
	public FloatRange StunTime;

}