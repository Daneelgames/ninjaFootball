using UnityEngine;
using System.Collections;

public class SetAnimBoolOnStart : MonoBehaviour {

    [SerializeField]
    private string boolName;
    [SerializeField]
    private bool isTrue;

    [SerializeField]
    private Animator _animator;

	void Start () {
        _animator.SetBool(boolName, isTrue);
	}
	
}
