using UnityEngine;
using System.Collections;

public class CheckpointModel : MonoBehaviour {
    public static CheckpointController LatestCheckpoint;

    public void Awake() {
        //DontDestroyOnLoad(transform.gameObject);
    }
}
