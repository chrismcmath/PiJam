using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterfallPiPieceController : InteractablePiPieceController {
    public void Update() {
        if (transform.localPosition.y < -50f) {
            Destroy(gameObject);
        }
    }
}
