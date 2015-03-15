using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiPieceController : MonoBehaviour {
    public GameObject Circle;
    public GameObject Line;

    public Collider2D LineBounds;
    public Collider2D CircleBounds;

    private bool _CanToggleViaChain = true;

    public void Update() {
    }

    public void OnMouseDown() {
        Toggle();
    }

    public void ToggleViaChain() {
        if (_CanToggleViaChain) {
            StartCoroutine(ToggleViaAfterWait());
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
        List<PiPieceController> neighbours = new List<PiPieceController>();

        Collider2D thisCollider = GetRelevantBoundsCollider();
        List<GameObject> colliderGOs = new List<GameObject>(GameObject.FindGameObjectsWithTag(Consts.TAG_PIPIECE_COLLIDER));
        foreach (GameObject c in colliderGOs) {
            if (thisCollider.bounds.Intersects(c.GetComponent<Collider2D>().bounds)) {
                neighbours.Add(c.GetComponentInParent<PiPieceController>());
            }
        }
        return neighbours;
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

    private Collider2D GetRelevantBoundsCollider() {
        if (Circle.activeSelf) {
            return CircleBounds;
        } else if (Line.activeSelf) {
            return LineBounds;
        }
        return null;
    }
}
