using UnityEngine;
using System.Collections;

public class Consts : MonoBehaviour {
    public static string TAG_NEIGHBOUR_LOCATOR = "NeighbourLocator";
    public static string TAG_PIPIECE_COLLIDER = "PiPieceCollider";
    public static string LAYER_PLATFORM = "Platform";

    public const float ToggleChainDelay = 0.1f;
    public const float ToggleChainDelayReset = 0.5f; //should always be 2 * ToggleChainDelay
    public const float PiPieceJumpForce = 1f;
    public const float AvatarSpeed = 3f;

}
