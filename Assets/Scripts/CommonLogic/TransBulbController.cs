using UnityEngine;
using System.Collections;

public class TransBulbController : MonoBehaviour {
    [HideInInspector]
    public bool  bulbFixed = false;

    [SerializeField]
    private GameObject brokenBulb;
    [SerializeField]
    private GameObject fixedBulb;

    void Start()
    {
        InvokeRepeating("CheckIfFixed", 1f, 1f);
        fixedBulb.SetActive(false);
    }

    void CheckIfFixed()
    {
        if (bulbFixed)
        {
            brokenBulb.SetActive(false);
            fixedBulb.SetActive(true);
            CancelInvoke("CheckIfFixed");
        }
    }
}
