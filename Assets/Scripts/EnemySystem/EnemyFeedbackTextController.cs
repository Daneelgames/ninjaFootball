using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyFeedbackTextController : MonoBehaviour {

    private float textTime = 1f;
    [SerializeField]
    private bool showExp = false;

    [SerializeField]
    private Text _text;

    private int curAmountDmg = 0;
    

    public void GetDMG(int dmgAmount)
    {
        curAmountDmg += dmgAmount;
        _text.color = new Color(255, 0, 0);
        showExp = true;
        _text.enabled = true;
        _text.text = "- " + curAmountDmg + " HP";
        textTime = 1;
    }

    void Start()
    {
        _text.enabled = false;
    }

    void Update()
    {
        if (showExp)
        {
            if (textTime > 0)
            {
                textTime -= 1 * Time.deltaTime;
                
            }

            else if (textTime <= 0)
            {
                showExp = false;
                _text.enabled = false;
                textTime = 1f;
                curAmountDmg = 0;
            }
        }
    }
}
