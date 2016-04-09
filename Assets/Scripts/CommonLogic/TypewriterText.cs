using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour {

    public bool talked = false;

	public string[] sourceText;
    [SerializeField]
    private Sprite[] dialogPicture;
    [ReadOnly]
    public Animator canvasAnimator;
    [ReadOnly]
    public bool isInDialog = false;
	[ReadOnly]
    public int currentlyDisplayingText = 0;
    public float textSpeed = 0.1f;

    private PlayerMovement playerScript;
    private bool inTrigger = false;
    private AudioSource _audio;

    private Text textBox;
    private Image picBox;

    private bool dialogToOver = false;

    void Start()
    {
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        _audio = GetComponent<AudioSource>() as AudioSource;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>() as PlayerMovement;
        textBox = GameObject.Find("Text").GetComponent<Text>() as Text;
        picBox = GameObject.Find("DialogPicture").GetComponent<Image>() as Image;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }

    void Update () {
		if (isInDialog && Input.anyKeyDown && !dialogToOver)
        {
            if (textBox.text == sourceText[currentlyDisplayingText - 1])
                SkipToNextText();
            else
                SkipAnimation();
        }

        if (inTrigger)
        {
            //Start dialog
            if (!isInDialog && !playerScript.dialog && Input.GetAxisRaw("Vertical") > 0 && playerScript.isOnGround)
                StartDialog();
        }

    }

    public void StartDialog()
    {
        SkipToNextText();
        isInDialog = true;
        dialogToOver = false;
        canvasAnimator.SetBool("Dialog", true);
        playerScript.DialogStart();

    }

    public void SkipToNextText(){
		StopAllCoroutines();
		//Skip to the next line
		if (currentlyDisplayingText < sourceText.Length)
        {
            if (dialogPicture[currentlyDisplayingText] != null)
                {   
                    if (picBox.enabled != true)
                        picBox.enabled = true;

                    picBox.sprite = dialogPicture[currentlyDisplayingText];
                }
            else
                picBox.enabled = false;

            StartCoroutine (AnimateText(currentlyDisplayingText));
            currentlyDisplayingText += 1;
        }
		//Reached the end of the array
		else if (currentlyDisplayingText == sourceText.Length)
        {
            StartCoroutine(DialogOver());
            dialogToOver = true;
        }
    }

    IEnumerator DialogOver()
    {
        yield return new WaitForSeconds(0.2F);
        inTrigger = false;
        StartCoroutine(IgnorePlayer());
        currentlyDisplayingText = 0;
        isInDialog = false;
        canvasAnimator.SetBool("Dialog", false);
        textBox.text = "";
        playerScript.DialogOver();
    }

    public void SkipAnimation()
    {
        StopAllCoroutines();
        textBox.text = sourceText[currentlyDisplayingText - 1];
    }

        IEnumerator IgnorePlayer()
    {
        Physics2D.IgnoreLayerCollision(10, 12, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(10, 12, false);
        talked = true;
    }

    IEnumerator AnimateText(int curStr)
    {
		
		for (int i = 0; i < (sourceText[curStr].Length+1); i++)
		{
			textBox.text = sourceText[curStr].Substring(0, i);
            _audio.Play();
            _audio.pitch = Random.Range(.7f, 1.3f);
			yield return new WaitForSeconds(textSpeed);
		}
	}
}