using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiPieceController : MonoBehaviour {
    public GameObject Circle;
    public GameObject Line;

    public PiPieceNeighbourDetector LineNeighbourhoodDetector;
    public PiPieceNeighbourDetector CircleNeighbourhoodDetector;

    private bool _CanToggleViaChain = true;

    public void Update() {
    }

    public void OnMouseDown() {
        Toggle();
    }

    public void ToggleViaChain() {
        if (_CanToggleViaChain) {
            Toggle();
            ChainHaitus();
        }
    }

    private void Toggle() {
        Chain(); //Need to chain first to get the current neighbours

        Circle.SetActive(!Circle.activeSelf);
        Line.SetActive(!Line.activeSelf);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Consts.PiPieceJumpForce, ForceMode2D.Impulse);
    }

    private void Chain() {
        ChainHaitus();
        List<PiPieceController> neighbours = GetNeighbours();

        foreach (PiPieceController piPiece in neighbours) {
            piPiece.ToggleViaChain();
        }
    }

    private List<PiPieceController> GetNeighbours() {
        if (Circle.activeSelf) {
            return CircleNeighbourhoodDetector.Neighbours;
        } else if (Line.activeSelf) {
            return LineNeighbourhoodDetector.Neighbours;
        }
        return null;
    }

    private void ChainHaitus() {
        _CanToggleViaChain = false;
        StartCoroutine(ResetCanToggleViaChainAfterWait());
    }

    private IEnumerator ToggleViaAfterWait() {
        yield return new WaitForSeconds(Consts.ToggleChainDelay);
        Toggle();
    }

    private IEnumerator ResetCanToggleViaChainAfterWait() {
        yield return new WaitForSeconds(Consts.ToggleChainDelayReset);
        _CanToggleViaChain = true;
    }
}
