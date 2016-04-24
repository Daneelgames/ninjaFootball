using UnityEngine;
using System.Collections;

public class TrapButtonController : MonoBehaviour {

    [SerializeField]
    private Animator trapAnimator;
    [SerializeField]
    private Animator buttonAnimator;
    [SerializeField]
    private float cooldownMax = 1f;
    private float cooldowncur = 1f;

    private AudioSource trapAudio;
    private AudioSource buttonAudio;

    private void Start()
    {
        cooldowncur = cooldownMax;
        buttonAudio = GetComponent<AudioSource>();
        trapAudio = trapAnimator.gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (cooldowncur <= 0)
        {
            if (coll.tag == "Player" || coll.tag == "Enemy")
            {
                buttonAnimator.SetTrigger("Action");
                trapAnimator.SetTrigger("Action");
                cooldowncur = cooldownMax;
                trapAudio.Play();
                buttonAudio.Play();
            }
        }
    }

    void Update()
    {
        if (cooldowncur > 0)
            cooldowncur -= 1 * Time.deltaTime;
    }
}