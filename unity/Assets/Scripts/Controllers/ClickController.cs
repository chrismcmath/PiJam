using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {
    public static string PLAYER_INPUT = "PLAYER_INPUT";

    public void OnMouseDown() {
        CastRayToWorld();
    }

    private void CastRayToWorld() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        Vector3 point = ray.origin + (ray.direction);    
        Messenger<Vector3>.Broadcast(PLAYER_INPUT, point);
    }
}
