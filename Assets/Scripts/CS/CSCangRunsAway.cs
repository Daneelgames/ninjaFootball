using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CSCangRunsAway : MonoBehaviour {

    [SerializeField]
    private float waitBeforeStop;
    [SerializeField]
    private float cangSpeed = 1F;

    [SerializeField]
    private GameObject prof_2;
    [SerializeField]
    private GameObject mother_2;

    [SerializeField]
    private Sprite setSlide;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip oneShot;

    private Image canvasSprite;
    private Animator canvasAnimator;
    private GameObject player;
    private PlayerMovement _pm;
    private GameObject cangInstance;
    private bool moveCang;
    private Animator cangAnimator;

    private bool scaleSlide = false;
    private Vector3 slideScale = new Vector3(1, 1, 1);
    
    void Start()
    {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        canvasAnimator.SetBool("NoInterface", false);
        cangInstance = GameObject.Find("DarkCang_3(Clone)");
        cangAnimator = cangInstance.GetComponentInChildren<Animator>();
        canvasSprite = GameObject.Find("Slide").GetComponent<Image>();
        StartCoroutine(CutScene());
	}

    void Update()
    {
        if (moveCang)
            cangInstance.transform.Translate(Vector3.left * cangSpeed * Time.deltaTime);

        if (scaleSlide)
        {
            slideScale.x += 1 * Time.deltaTime / 10;
            slideScale.y += 1 * Time.deltaTime / 10;
            canvasSprite.rectTransform.localScale = slideScale;
        }
    }

    IEnumerator CutScene()
    {
        //cang goes away
        _pm.DialogStart();
        canvasAnimator.SetBool("NoInterface", true);
        moveCang = true;
        cangAnimator.SetBool("Move", true);
        yield return new WaitForSeconds(waitBeforeStop);

        // 13 years later
        canvasSprite.enabled = true;
        canvasSprite.sprite = setSlide;
        scaleSlide = true;
        canvasSprite.rectTransform.sizeDelta = new Vector2(1284, 1036);
        _audio.Play();
        _audio.PlayOneShot(oneShot);
        GameObject mainMusic = GameObject.Find("MainMusicTrigger(Clone)") as GameObject;
        Destroy(mainMusic);

        yield return new WaitForSeconds(12F);
        
        //transition
        canvasAnimator.SetBool("NoInterface", false);
        canvasAnimator.SetTrigger("Dead");

        yield return new WaitForSeconds(.3F);

        //move player, over
        scaleSlide = false;
        canvasSprite.enabled = false;
        canvasSprite.rectTransform.localScale = new Vector3(1, 1, 1);

        GameObject prof_1 = GameObject.Find("Professor_1_2(Clone)");
        Destroy(prof_1);
        Instantiate(prof_2, new Vector3(115, 33.634f, 0), transform.rotation);

        GameObject mother_1 = GameObject.Find("Mother_4(Clone)");
        Destroy(mother_1);
        Instantiate(mother_2, new Vector3(105, 33.26f, 0), transform.rotation);

        TransBulbController transBulb = GameObject.Find("TransBulb").GetComponent<TransBulbController>() as TransBulbController;
        transBulb.bulbFixed = true;
        GameObject cam = GameObject.Find("_Camera");
        cam.transform.position = new Vector3(113, 36.3f, 0f);
        _pm.DialogOver();
        player.transform.position = new Vector3(110f, 33.5f, 0f);

        Destroy(cangInstance);
        Destroy(gameObject);
    }
}
