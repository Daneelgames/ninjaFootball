using UnityEngine;
using System.Collections;

public class TrapButtonController : MonoBehaviour {

    [SerializeField]
    private Animator trapAnimator;
    [SerializeField]
    private Animator buttonAnimator;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" || coll.tag == "Enemy")
        {
            buttonAnimator.SetTrigger("Action");
            trapAnimator.SetTrigger("Action");
        }
    }
}