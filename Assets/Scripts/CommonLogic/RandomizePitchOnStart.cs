using UnityEngine;
using System.Collections;

public class RandomizePitchOnStart : MonoBehaviour {

    public float minPitch = 0.75f;
    public float maxPitch = 1.25f;

    private AudioSource _audio;

	void Start () {
        _audio = GetComponent<AudioSource>() as AudioSource;
        _audio.pitch = Random.Range(minPitch, maxPitch);
	}
}
