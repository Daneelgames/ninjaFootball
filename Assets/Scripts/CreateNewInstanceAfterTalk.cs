using UnityEngine;
using System.Collections;

public class CreateNewInstanceAfterTalk : MonoBehaviour {

    [SerializeField]
    private GameObject npcInstance;

    private TypewriterText npcScript;
    private bool canspawn = true;

    void Start ()
    {
        npcScript = GetComponent<TypewriterText>();
    }

    void Update ()
    {
        if(npcScript.talked && canspawn)
        {
            canspawn = false;
            Instantiate(npcInstance, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
