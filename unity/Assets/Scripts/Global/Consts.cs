using UnityEngine;
using System.Collections;

public class Consts : MonoBehaviour {
    public static string TAG_NEIGHBOUR_LOCATOR = "NeighbourLocator";
    public static string TAG_PIPIECE_COLLIDER = "PiPieceCollider";
    public static string LAYER_PLATFORM = "Platform";
    public static string LAYER_CHECKPOINT = "Checkpoint";

    public const float ToggleChainDelay = 0.2f;
    public const float ToggleChainDelayReset = 0.5f; //should always be 2 * ToggleChainDelay
    public const float PiPieceJumpForce = 10f;
    public const float PiPieceTransformPeriod = 0.0f;
    public const float BeamPeriod = 0.2f;
    public const float AvatarSpeed = 3f;

}
