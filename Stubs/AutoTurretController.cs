using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurretController : MonoBehaviour
{
    public AutoTurretYawLerp YawController;
    public AutoTurretPitchLerp PitchController;
    public AutoTurretFireController FireController;
    public Transform Laser;
    public Transform Cone;
    public Material ConeMaterial;
    public Color ConeColor;
    public bool IsOn;
    public bool IsUserAccessing;
}
