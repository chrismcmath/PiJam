using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiPieceNeighbourDetector : MonoBehaviour {
    public List<PiPieceController> Neighbours = new List<PiPieceController>();

    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == Consts.TAG_NEIGHBOUR_LOCATOR) {
            PiPieceController piPiece = coll.GetComponentInParent<PiPieceController>();
            AddNeighbour(piPiece);
        }
    }

    public void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == Consts.TAG_NEIGHBOUR_LOCATOR) {
            PiPieceController piPiece = coll.GetComponentInParent<PiPieceController>();
            RemoveNeighbour(piPiece);
        }
    }

    private void AddNeighbour(PiPieceController piPiece) { 
        if (piPiece != null) {
            if (!Neighbours.Contains(piPiece)) {
                Neighbours.Add(piPiece);
            }
        }
    }

    private void RemoveNeighbour(PiPieceController piPiece) {
        if (piPiece != null) {
            if (Neighbours.Contains(piPiece)) {
                Neighbours.Remove(piPiece);
            }
        }
    }
}
