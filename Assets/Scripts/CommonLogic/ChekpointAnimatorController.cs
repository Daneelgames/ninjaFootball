using UnityEngine;
using System.Collections;

public class ChekpointAnimatorController : MonoBehaviour {

    [ReadOnly]
    public bool isActive;

    [ReadOnly]
    public PlayerMovement pm;

    [ReadOnly]
    public Animator animator;


    // Use this for initialization
    void Start() {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (gameObject == pm.activeCheckpoint.gameObject)
            isActive = true;

        else if (gameObject != pm.activeCheckpoint.gameObject)
            isActive = false;

        AnimatorBool();
    }

    void AnimatorBool()
    {
        animator.SetBool("Active", isActive);
    }

}
