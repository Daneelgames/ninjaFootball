using UnityEngine;
using System.Collections;

public class PlaySoundInTrigger : MonoBehaviour {
    private AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            source.Play();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            source.Stop();
        }
    }

}
