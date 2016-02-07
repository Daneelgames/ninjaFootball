using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

    [ReadOnly]
    public AudioSource source;
    public AudioClip[] audioClip;
    
    private float pitch;

    public void PlaySound(int clip)
    {
        source.clip = audioClip[clip];
        pitch = Random.Range(.75f, 1.25f);
        source.pitch = pitch;
        source.Play();
    }

}
