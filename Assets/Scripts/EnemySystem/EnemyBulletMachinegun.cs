using UnityEngine;
using System.Collections;

public class EnemyBulletMachinegun : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private AudioSource _audio;
    private float pitch;

    private float direction = 1f;
    

    void Start ()
    {
        pitch = Random.Range(0.75f, 1.25f);

        GetDirection();
        StartCoroutine("Shoot", 0.05f);
    }
    
    void GetDirection()
    {
        if (GameObject.Find("Player").transform.position.x > transform.position.x)
            direction = 1f;
        else
            direction = -1f;
    }

    IEnumerator Shoot(float waitTime)
    {
        for (int i = 0; i < 6; i++)
        {
            var lastBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;
            lastBullet.GetComponent<EnemyBullet>().direction = new Vector2(direction, Random.Range(-1f, 1f));
            var lastBullet2 = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), gameObject.transform.rotation) as GameObject;
            lastBullet2.GetComponent<EnemyBullet>().direction = new Vector2(direction, Random.Range(-1f, 1f));
            PlayClipAtPoint(_audio.clip, new Vector3(transform.position.x, transform.position.y, 0), .75f, pitch);
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
