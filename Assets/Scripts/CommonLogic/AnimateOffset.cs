using UnityEngine;
using System.Collections;

public class AnimateOffset : MonoBehaviour {

    [ReadOnly]
    public Material material;
    public float scrollSpeed;

    private float offset;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        offset += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = new Vector2(0, offset);

        if (offset >= 10)
            offset = -10;
	}
}
