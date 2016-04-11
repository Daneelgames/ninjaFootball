using UnityEngine;
using System.Collections;

public class Machinegun : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private AudioSource _audio;
    private float pitch;

    private GameObject playerShot;

    void Start ()
    {
        playerShot = GameObject.Find("Player/PlayerSprites/Shot");
        pitch = Random.Range(0.75f, 1.25f);
        StartCoroutine("Shoot", 0.05f);
    }

    void Update()
    {
        transform.position = playerShot.transform.position;
    }

    IEnumerator Shoot(float waitTime)
    {
        for (int i = 0; i < 6; i++)
        {
            var lastBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.1f), gameObject.transform.rotation) as GameObject;
            lastBullet.GetComponent<BulletMovementController>().hRandom = 5f;
            var lastBullet2 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.1f), gameObject.transform.rotation) as GameObject;
            lastBullet2.GetComponent<BulletMovementController>().hRandom = 5f;
            PlayClipAtPoint(_audio.clip, new Vector3(transform.position.x, transform.position.y, 0), 1f, pitch);
            yield return new WaitForSeconds(waitTime);
            if (i == 5)
                Destroy(gameObject);
        }
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
