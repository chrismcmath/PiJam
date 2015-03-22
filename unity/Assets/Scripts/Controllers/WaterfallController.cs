using UnityEngine;
using System.Collections;

public class WaterfallController : MonoBehaviour {
    public string PrefabLocation = "Prefabs/waterfall_pi_piece";
    private float _Counter = 0f;

    public void Update() {
        if (_Counter > 0f) {
            _Counter -= Time.deltaTime;
        }

        if (_Counter <= 0f) {
            SpawnPP();
            _Counter = 2f;
        }
    }

    private void SpawnPP() {
        WaterfallPiPieceController[] waterfallPieces = GetComponentsInChildren<WaterfallPiPieceController>();
        if (waterfallPieces.Length > 0) {
            return;
        }

        GameObject prefab = Instantiate(Resources.Load<GameObject>(PrefabLocation));
        prefab.transform.parent = transform;
        prefab.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        prefab.transform.localPosition = Vector3.zero;
    }
}
