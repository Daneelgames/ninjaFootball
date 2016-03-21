using UnityEngine;
using System.Collections;

public class WallArrowTrapController : MonoBehaviour {
    [SerializeField]
    private GameObject projectile;

    private bool isVisible = false;
    private GameObject shotHolder;

    void Start()
    {
        shotHolder = transform.Find("WallArrowTrapShotHolder").gameObject;
    }
    
    public void Attack()
    {
        if (isVisible)
        {
            GameObject newProjectile = Instantiate(projectile, shotHolder.transform.position, transform.rotation) as GameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zone")
            isVisible = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Zone")
            isVisible = false;
    }
}
