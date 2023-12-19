using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSensorController : MonoBehaviour
{
    public AutoTurretYawLerp YawController;
    public AutoTurretPitchLerp PitchController;
  public Transform Cone;
  public Material ConeMaterial;
  public Color ConeColor;
  public bool IsOn;
  // public TileEntityPoweredTrigger TileEntity;
  public bool IsUserAccessing;
}
