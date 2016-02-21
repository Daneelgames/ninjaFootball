using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

    //[ReadOnly]
    //public AudioSource source;
    public AudioClip[] audioClip;
    
    private float pitch;

    public void PlaySound(int clip)
    {
        //source.clip = audioClip[clip];
        pitch = Random.Range(.75f, 1.25f);
        PlayClipAtPoint(audioClip[clip], new Vector3(transform.position.x, transform.position.y, 0), 1f, pitch);
    }

    GameObject PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitch)
    {
        GameObject obj = new GameObject();
        obj.transform.position = position;
        obj.AddComponent<AudioSource>();
        obj.GetComponent<AudioSource>().pitch = pitch;
        obj.GetComponent<AudioSource>().PlayOneShot(clip, volume);
        Destroy(obj, clip.length / pitch);
        return obj;
    }
}
