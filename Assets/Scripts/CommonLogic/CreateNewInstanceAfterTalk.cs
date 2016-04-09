using UnityEngine;
using System.Collections;

public class CreateNewInstanceAfterTalk : MonoBehaviour {

    [SerializeField]
    private GameObject nextNpcInstance;
    [SerializeField]
    private bool notDestroy = false;

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
            if (nextNpcInstance != null)
                Instantiate(nextNpcInstance, transform.position, transform.rotation);

            if (!notDestroy)
                Destroy(gameObject);
        }

    }
}
