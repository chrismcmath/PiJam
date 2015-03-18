using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterfallPiPieceController : InteractablePiPieceController {
    private float _Counter = 20f;
    public void Update() {
        if (transform.localPosition.y < -17f) {
            Destroy(gameObject);
        }

        _Counter -= Time.deltaTime;
        if (_Counter < 0f) {
            Destroy(gameObject);
        }
    }
}
