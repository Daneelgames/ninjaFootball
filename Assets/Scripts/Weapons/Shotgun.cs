using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private AudioSource _audio;
    private float pitch;

    void Start ()
    {
        pitch = Random.Range(0.75f, 1.25f);
        PlayClipAtPoint(_audio.clip, new Vector3(transform.position.x, transform.position.y, 0), 1f, pitch);

        for (int i = 0; i < 10; i++)
        {
            var lastBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.1f), gameObject.transform.rotation) as GameObject;
            Bullet bulletScript = lastBullet.GetComponent<Bullet>() as Bullet;
            bulletScript.hRandom = 10f;
            bulletScript.damage = 3;
        }
        Destroy(gameObject);
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
