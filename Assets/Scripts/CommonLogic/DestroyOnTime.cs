using UnityEngine;
using System.Collections;

public class DestroyOnTime : MonoBehaviour {

    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private SpriteRenderer spriteToFade = null;

    private Color spriteAlpha;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, destroyTime);

        if (spriteToFade != null)
            spriteAlpha = spriteToFade.GetComponent<SpriteRenderer>().color;
	}
	
    void Update()
    {
        if (spriteToFade != null)
        {
            spriteAlpha.a -= 1 * Time.deltaTime / destroyTime;
        }

    }

}
