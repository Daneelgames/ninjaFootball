using UnityEngine;
using System.Collections;

public class TalkCursorController : MonoBehaviour {

    private bool visible = false;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _renderer.enabled = visible;
    }

    public void SwitchActive()
    {
        visible = !visible;
    }

}
