using UnityEngine;
using System.Collections;

public class SetAnimatorBoolInDialog : MonoBehaviour {
     
    [SerializeField]
    private string boolName;
    [SerializeField]
    private TypewriterText typewriter;
    [SerializeField]
    private Animator _animator;

	void Update () {
        if (typewriter.isInDialog)
            _animator.SetBool(boolName, true);
	}
}
