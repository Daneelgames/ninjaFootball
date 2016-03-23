using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private RawImage fadeSprite;
    [SerializeField]
    private Color fadeSpriteColor;

    private bool fadeSpriteVisible = false;

    private Canvas canvas;
    private Animator canvasAnimator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasAnimator = canvas.GetComponent<Animator>();
        canvas.worldCamera = GameObject.Find("RenderCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Restart"))
            SceneManager.LoadScene("testScene");
        
        if (fadeSpriteVisible && fadeSpriteColor.a < 1)
            fadeSpriteColor.a += 5 * Time.deltaTime;
        else if (!fadeSpriteVisible && fadeSpriteColor.a > 0)
            fadeSpriteColor.a -= 5 * Time.deltaTime;

    }

    public void ChangeScene(int nextLevel)
    {
        StartCoroutine(LoadScene(nextLevel));
    }

    public IEnumerator LoadScene(int nextScene)
    {

        // Fade to black
        fadeSpriteVisible = true;
        yield return new WaitForSeconds(0.2f);

        // Load loading screen
        canvasAnimator.SetBool("Loading", true);
        yield return SceneManager.LoadSceneAsync("loadingScene");
        // !!! unload old screen (automatic)

        // Fade to loading screen
        fadeSpriteVisible = false;
        yield return new WaitForSeconds(0.2f);

        float endTime = Time.time + 0.5f;

        // Load level async
        yield return SceneManager.LoadSceneAsync(nextScene);

        if (Time.time < endTime)
            yield return new WaitForSeconds(endTime - Time.time);

        // Fade to black
        fadeSpriteVisible = true;
        yield return new WaitForSeconds(0.2f);

        // !!! unload loading screen
        canvasAnimator.SetBool("Loading", false);
        // Fade to new screen
        fadeSpriteVisible = false;
        yield return new WaitForSeconds(0.2f);
    }
    
}
