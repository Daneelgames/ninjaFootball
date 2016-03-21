using UnityEngine;
using System.Collections;

public class WallArrowTrapController : MonoBehaviour {
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private bool isVisible = false;

    private GameObject shotHolder;

    void Start()
    {
        shotHolder = transform.Find("WallArrowTrapShotHolder").gameObject;
        PickSide();
    }
    
    void PickSide()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5, 1 << 8);
        if (hitRight)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Attack()
    {
        if (isVisible)
        {
            Instantiate(projectile, shotHolder.transform.position, transform.rotation);
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
