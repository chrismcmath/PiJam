using UnityEngine;
using System.Collections;

public class PiPieceController : MonoBehaviour {

    public GameObject Circle;
    public GameObject Line;

    public void Update() {
    }

    public void OnMouseDown() {
        Debug.Log("PiPieceController OnMouseDown");
        Toggle();
    }

    private void Toggle() {
        Circle.SetActive(!Circle.activeSelf);
        Line.SetActive(!Line.activeSelf);
    }

}
