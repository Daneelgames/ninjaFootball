using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour {

	public string[] sourceText;
    [ReadOnly]
    public Text textBox;
    [ReadOnly]
    public bool isInDialog = false;
	[ReadOnly]
    public int currentlyDisplayingText = 0;
    public float textSpeed = 0.1f;

    private PlayerMovement playerScript;
    private bool inTrigger = false;
    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>() as AudioSource;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>() as PlayerMovement;
        textBox = GameObject.Find("Text").GetComponent<Text>() as Text;
    }

    void Update () {
		//Start dialog
		if (isInDialog && Input.GetButtonDown("Fire1"))
			SkipToNextText();

        if (inTrigger)
        {
            if (!isInDialog && Input.GetButtonDown("Fire1"))
            {
                SkipToNextText();
                isInDialog = true;
                playerScript.DialogStart();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
            inTrigger = true;
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
            inTrigger = false;
    }

    public void SkipToNextText(){
		StopAllCoroutines();
		//Skip to the next line
		if (currentlyDisplayingText < sourceText.Length) {
			StartCoroutine (AnimateText(currentlyDisplayingText));
            currentlyDisplayingText += 1;
        }
		//Reached the end of the array
		else if (currentlyDisplayingText == sourceText.Length)
        {
            inTrigger = false;  
            StartCoroutine(IgnorePlayer());
            currentlyDisplayingText = 0;
			isInDialog = false;
			textBox.text = "";
            playerScript.DialogOver();
        }
	}

    IEnumerator IgnorePlayer()
    {
        Physics2D.IgnoreLayerCollision(10, 12, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(10, 12, false);
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