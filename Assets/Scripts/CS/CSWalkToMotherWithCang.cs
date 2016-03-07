using UnityEngine;
using System.Collections;

public class CSWalkToMotherWithCang : MonoBehaviour {

    [SerializeField]
    private float waitBeforeStop;
    [SerializeField]
    private Vector3 playerNewPosition;
    [SerializeField]
    private GameObject cang;
    [SerializeField]
    private float cangSpeed = 1F;
    [SerializeField]
    private Vector3 cangNewPosition;
    [SerializeField]
    private Vector3 cameraNewPosition;
    [SerializeField]
    private Collider2D ownCollider;

    private GameObject cam;
    private Animator canvasAnimator;
    private GameObject player;
    private PlayerMovement _pm;
    private GameObject cangInstance;
    private bool moveCang;
    private Animator cangAnimator;
    
    void Start()
    {
        transform.position = new Vector3(14, -2, 0);
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
        cam = GameObject.Find("_Camera");
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        cangInstance = Instantiate(cang, cangNewPosition, transform.rotation) as GameObject;
        cangAnimator = cangInstance.GetComponentInChildren<Animator>();
        StartCoroutine(CutScene());
	}

    void Update()
    {
        if (moveCang)
            cangInstance.transform.Translate(Vector3.right * cangSpeed * Time.deltaTime);
    }

    IEnumerator CutScene()
    {
        _pm.DialogStart();
        canvasAnimator.SetTrigger("Dead");
        yield return new WaitForSeconds(.25F);
        canvasAnimator.SetBool("NoInterface", true);
        player.transform.position = playerNewPosition;
        cangInstance.transform.position = cangNewPosition;
        cam.transform.position = cameraNewPosition;
        moveCang = true;
        cangAnimator.SetBool("Move", true);
        _pm.hAxis = 1;
        yield return new WaitForSeconds(waitBeforeStop);
        canvasAnimator.SetBool("NoInterface", false);
        moveCang = false;
        cangAnimator.SetBool("Move", false);
        _pm.hAxis = 0;
        _pm.DialogOver();
        ownCollider.enabled = true;
    }
}
