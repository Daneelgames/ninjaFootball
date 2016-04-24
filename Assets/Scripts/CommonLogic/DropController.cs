using UnityEngine;
using System.Collections;

public class DropController : MonoBehaviour {

    public int amount;

    [SerializeField]
    private GameObject[] expDrop;
    
    void Start()
    {
        int curAmount = amount;

        for (int i = curAmount; i > 10; i -= 10)
        {
            Instantiate(expDrop[2], new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            curAmount = i;
        }

        for (int i = curAmount; i > 5; i -= 5)
        {
             Instantiate(expDrop[1], new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            curAmount = i;
        }

        for (int i = curAmount; i > 1; i -= 1)
        {
            Instantiate(expDrop[0], new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            curAmount = i;
        }

        Destroy(gameObject);
    }
}
