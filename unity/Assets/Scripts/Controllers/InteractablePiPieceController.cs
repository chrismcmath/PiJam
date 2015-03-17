using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractablePiPieceController : PiPieceController {
    public static string PI_PIECE_CLICKED = "PI_PIECE_CLICKED";
    public void OnMouseDown() {
        if (_State == PiPieceState.CIRCLE || AvatarController.HasPi) {
            Toggle();
            Messenger<PiPieceController>.Broadcast(PI_PIECE_CLICKED, this as PiPieceController);
        }
    }
}
