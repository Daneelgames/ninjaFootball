using UnityEngine;
using System.Collections;

public class TalkCursorController : MonoBehaviour {
    
    private bool visible = false;
    private SpriteRenderer _renderer;
    private Collider2D lastCollider;

    void Start()
    {
        _renderer = transform.Find("TalkCursor").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _renderer.enabled = visible;
        if (lastCollider == null || !lastCollider.enabled)
            visible = false;

    }
    
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "NPC" || coll.tag == "Active")
        {
            visible = true;
            lastCollider = coll;
        }
    }

    void OnTriggerExit2D (Collider2D coll)
    {
        if (coll.tag == "NPC" || coll.tag == "Active")
            visible = false;
    }

}
