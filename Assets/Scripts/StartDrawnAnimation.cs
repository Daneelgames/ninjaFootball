using UnityEngine;
using System.Collections;

public class StartDrawnAnimation : MonoBehaviour {

    [SerializeField]
    private GameObject cutScene;
    [SerializeField]
    private float csDuration;
    [SerializeField]
    private GameObject spawnInstance;

    private GameObject cam;
    private PlayerMovement pm;

    void Start()
    {
        cam = GameObject.Find("_Camera");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            pm = coll.GetComponent<PlayerMovement>();
            pm.DialogStart();
            StartCoroutine(CutScene(csDuration));
        }
    }

    IEnumerator CutScene (float duration)
    {
        GameObject cs = Instantiate(cutScene, cam.transform.position, cam.transform.rotation) as GameObject;
        yield return new WaitForSeconds(duration);
        pm.DialogOver();
        Destroy(cs);
        if (spawnInstance != null)
            Instantiate(spawnInstance, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
