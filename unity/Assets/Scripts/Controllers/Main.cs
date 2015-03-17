using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    public Animator FlashAnimator;
	
	void Update () {
        if (Input.GetKeyDown("r")) {
            Application.LoadLevel(Application.loadedLevel);
            FlashAnimator.SetTrigger("flash");
        }
	}
}
