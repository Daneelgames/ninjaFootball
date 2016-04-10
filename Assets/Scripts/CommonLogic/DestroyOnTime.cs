using UnityEngine;
using System.Collections;

public class DestroyOnTime : MonoBehaviour {

    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private SpriteRenderer spriteToFade = null;

    private float spriteAlpha = 1;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, destroyTime);
	}
	
    void Update()
    {
        if (spriteToFade != null)
        {
            spriteAlpha -= 1 * Time.deltaTime / destroyTime;
            spriteToFade.color = new Color(1f, 1f, 1f, spriteAlpha);
        }

    }

}
