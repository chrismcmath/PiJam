using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractablePiPieceController : PiPieceController {
    public static string PI_PIECE_CLICKED = "PI_PIECE_CLICKED";
    public void OnMouseDown() {
        if (_State == PiPieceState.CIRCLE || AvatarController.HasPi) {

            if (_State == PiPieceState.CIRCLE) {
                TriggerToggle();
                Messenger<PiPieceController, PiPieceState>.Broadcast(PI_PIECE_CLICKED, this as PiPieceController, PiPieceState.LINE);
            } else if (_State == PiPieceState.LINE) {
                StartCoroutine(TriggerToggleAfterWait());
                Messenger<PiPieceController, PiPieceState>.Broadcast(PI_PIECE_CLICKED, this as PiPieceController, PiPieceState.CIRCLE);
            }
        }
    }

    private IEnumerator TriggerToggleAfterWait() {
        yield return new WaitForSeconds(Consts.BeamPeriod);
        TriggerToggle();
    }

    private void TriggerToggle() {
        Toggle();
    }
}
