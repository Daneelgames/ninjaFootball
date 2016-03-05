using UnityEngine;
using System.Collections;

public class StartCutSceneInTrigger : MonoBehaviour {

    private TypewriterText npcScript;

    void Start()
    {
        npcScript = GetComponent<TypewriterText>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" && !npcScript.talked)
            npcScript.StartDialog();
    }
}
