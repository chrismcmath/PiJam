using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractablePiPieceController : PiPieceController {
    public void OnMouseDown() {
        Toggle();
    }
}
