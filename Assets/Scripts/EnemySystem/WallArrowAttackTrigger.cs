using UnityEngine;
using System.Collections;

public class WallArrowAttackTrigger : MonoBehaviour {

    [SerializeField]
    private WallArrowTrapController wallArrowController;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            wallArrowController.Attack();
    }
}
