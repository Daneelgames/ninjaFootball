using UnityEngine;
using System.Collections;

public class CSCangRunsAway : MonoBehaviour {

    [SerializeField]
    private float waitBeforeStop;
    [SerializeField]
    private float cangSpeed = 1F;

    private Animator canvasAnimator;
    private GameObject player;
    private PlayerMovement _pm;
    private GameObject cangInstance;
    private bool moveCang;
    private Animator cangAnimator;
    
    void Start()
    {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        canvasAnimator.SetBool("NoInterface", false);
        cangInstance = GameObject.Find("DarkCang_3(Clone)");
        cangAnimator = cangInstance.GetComponentInChildren<Animator>();
        StartCoroutine(CutScene());
	}

    void Update()
    {
        if (moveCang)
            cangInstance.transform.Translate(Vector3.left * cangSpeed * Time.deltaTime);
    }

    IEnumerator CutScene()
    {
        _pm.DialogStart();
        canvasAnimator.SetBool("NoInterface", true);
        moveCang = true;
        cangAnimator.SetBool("Move", true);
        yield return new WaitForSeconds(waitBeforeStop);
        canvasAnimator.SetBool("NoInterface", false);
        Destroy(cangInstance);
        _pm.DialogOver();
        Destroy(gameObject);
    }
}
