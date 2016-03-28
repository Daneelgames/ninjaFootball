using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }

    [SerializeField]
    private RawImage fadeSprite;
    [SerializeField]
    private Color fadeSpriteColor;

    [SerializeField]
    private bool fadeSpriteVisible = false;

    private Canvas canvas;
    private Animator canvasAnimator;

    [SerializeField]
    private Image quitSprite;
    private float quitValue = 0f;
    private bool exitGame = false;
    

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasAnimator = canvas.GetComponent<Animator>();
        canvas.worldCamera = GameObject.Find("RenderCamera").GetComponent<Camera>();
        InvokeRepeating("FindCamera", 1f, 1f);
        UnityEngine.Cursor.visible = false;
	}
	
    void FindCamera()
    {
        if (canvas.worldCamera == null)
            canvas.worldCamera = GameObject.Find("RenderCamera").GetComponent<Camera>();
    }

	void Update () {
        QuitGameManager();

        if (fadeSpriteVisible && fadeSpriteColor.a < 1)
        {
            fadeSpriteColor.a += 5 * Time.deltaTime;
            fadeSprite.color = fadeSpriteColor;
        }
            
        else if (!fadeSpriteVisible && fadeSpriteColor.a > 0)
        {
            fadeSpriteColor.a -= 5 * Time.deltaTime;
            fadeSprite.color = fadeSpriteColor;
        }

    }

    void QuitGameManager()
    {
        if (!exitGame)
        {
            if (Input.GetButton("Cancel") && quitValue < 1)
            {
                quitValue += 1f * Time.deltaTime;
            }

            else if (!Input.GetButton("Cancel") && quitValue > 0)
            {
                quitValue -= 5f * Time.deltaTime;
            }
            else if (quitValue < 0)
                quitValue = 0;

            quitSprite.fillAmount = quitValue;

            if (quitValue >= 1)
            {
                exitGame = true;
                StartCoroutine( QuitGame());
            }
        }
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    public void ChangeScene(int nextLevel)
    {
        StartCoroutine(LoadScene(nextLevel));
    }

    public IEnumerator LoadScene(int nextScene)
    {

        // Fade to black
        fadeSpriteVisible = true;
        yield return new WaitForSeconds(0.5f);

        // Load loading screen
        canvasAnimator.SetBool("Loading", true);
        canvasAnimator.SetBool("NoInterface", false);
        yield return SceneManager.LoadSceneAsync("loadingScene");
        // !!! unload old screen (automatic)

        // Fade to loading screen
        fadeSpriteVisible = false;
        yield return new WaitForSeconds(0.5f);

        float endTime = Time.time + 0.5f;

        // Load level async
        yield return SceneManager.LoadSceneAsync(nextScene);

        if (Time.time < endTime)
            yield return new WaitForSeconds(endTime - Time.time);

        // Fade to black
        fadeSpriteVisible = true;
        yield return new WaitForSeconds(0.5f);

        // !!! unload loading screen
        canvasAnimator.SetBool("Loading", false);
        // Fade to new screen
        fadeSpriteVisible = false;
        yield return new WaitForSeconds(0.5f);
    }
}