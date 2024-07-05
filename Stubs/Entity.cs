// Decompiled with JetBrains decompiler
// Type: Entity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34A370E1-6747-4EB1-949F-E72B029CEC58
// Assembly location: M:\SteamLibrary\steamapps\common\7 Days To Die\7DaysToDie_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Scripting;

#nullable disable
[Preserve]
public class Entity : MonoBehaviour
{
  public bool RootMotion;
  public bool HasDeathAnim;
  // public World world;
  public Transform PhysicsTransform;
  public Transform RootTransform;
  public Transform ModelTransform;
  public Bounds boundingBox;
  public int entityId;
  public int clientEntityId;
  public bool onGround;
  public bool isCollided;
  public bool isCollidedHorizontally;
  public bool isCollidedVertically;
  public Vector3 prevRotation;
  public Vector3 rotation;
  public Quaternion qrotation = Quaternion.identity;
  public Vector3 position;
  public Vector3 prevPos;
  public Vector3 targetPos;
  //public Vector3i chunkPosAddedEntityTo;
  //public Vector3i serverPos;
  //public Vector3i serverRot;
  public Vector3[] lastTickPos = new Vector3[5];
  public Vector3 motion;
  public bool IsMovementReplicated = true;
  public bool IsStuck;
  public bool IsEntityUpdatedInUnloadedChunk;
  public bool addedToChunk;
  public bool isSwimming;
  public float inWaterLevel;
  public float inWaterPercent;
  public bool isHeadUnderwater;
  public float stepHeight;
  public float ySize;
  public float distanceWalked;
  public float distanceSwam;
  public float distanceClimbed;
  public float fallDistance;
  public float entityCollisionReduction = 0.9f;
  public bool isEntityRemote;
  //public GameRandom rand;
  public static float updatePositionLerpTimeScale = 8f;
  public static float updateRotationLerpTimeScale = 8f;
  public static float tickPositionMoveTowardsMaxDistance = 3f;
  public static float tickPositionLerpMultiplier = 0.5f;
  public int entityClass;
  public float lifetime;
  public int count;
  public int belongsPlayerId;
  public bool bWillRespawn;
  public ulong WorldTimeBorn;
  //public DataItem<bool> IsFlyMode = new DataItem<bool>();
  //public DataItem<bool> IsGodMode = new DataItem<bool>();
  //public DataItem<bool> IsNoCollisionMode = new DataItem<bool>();
  //public EntityFlags entityFlags;
  //public EntityType entityType;
  public float lootDropProb;
  public string lootListOnDeath;
  public string lootListAlive;
  //public TileEntityLootContainer lootContainer;
  public float speedForward;
  public float speedStrafe;
  public float speedVertical;
  public int MovementState;
  public bool bDead;
  //public EModelBase emodel;
  //public CharacterControllerAbstract m_characterController;
  public float projectedMove;
  public bool IsRotateToGroundFlat;
  public bool IsDespawned;
  public bool bIsChunkObserver;
  //public NavObject NavObject;
  public Entity AttachedToEntity;
  public const int cPhysicsMasterTickRate = 2;
  public bool usePhysicsMaster;
  public bool isPhysicsMaster;
  public bool spawnByAllowShare;
  public int spawnById = -1;
  public string spawnByName;
  //public EnumRemoveEntityReason unloadReason;

  public struct MoveHitSurface
  {
    public Vector3 hitPoint;
    public Vector3 lastHitPoint;
    public Vector3 normal;
    public Vector3 lastNormal;
  }

  public enum EnumPositionUpdateMovementType
  {
    Lerp,
    MoveTowards,
    Instant,
  }
}
