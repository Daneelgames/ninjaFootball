using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponLevelUIController : MonoBehaviour
{

    [SerializeField]
    private Image content;

    [HideInInspector]
    public int curExpForLvl = 0;

    public int maxExpForLvl = 50;
    public int minExpForLvl = 0;

    private float minFill = 0f;
    private float maxFill = 1f;

    void Start()
    {
        curExpForLvl = minExpForLvl;
    }

    // Use this for initialization
    void Update()
    {
            HandleBar();
    }

    private void HandleBar()
    {
        content.fillAmount = Map(curExpForLvl, minExpForLvl, maxExpForLvl, minFill, maxFill);
    }

    private float Map(int curHealth, int inMin, int inMax, float outMin, float outMax)
    {
        return (curHealth * 1.0f - inMin) * (outMax - outMin) / (inMax * 1.0f - inMin * 1.0f) + outMin;
    }
}