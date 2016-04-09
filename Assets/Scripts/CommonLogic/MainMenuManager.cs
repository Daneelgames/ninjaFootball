using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    private Animator canvasAnimator;

	void Start () {
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        canvasAnimator.SetBool("NoInterface", true);
	}
}
