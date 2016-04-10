using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerFeedbackTextController : MonoBehaviour {

    private float textTime = 1f;
    [SerializeField]
    private bool showExp = false;

    [SerializeField]
    private Text _text;

    private int curAmountExp = 0;
    

    public void GetExp(int expAmount)
    {
        curAmountExp += expAmount;
        _text.color = new Color(255, 174, 0);
        showExp = true;
        _text.enabled = true;
        _text.text = curAmountExp + " EXP";
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
                curAmountExp = 0;
            }
        }
    }
}
