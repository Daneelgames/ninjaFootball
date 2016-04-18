using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerFeedbackTextController : MonoBehaviour {

    private float textTime = 1f;
    [SerializeField]
    private bool showExp = false;

    [SerializeField]
    private Text _text;
    [SerializeField]
    private PlayerSounds sounds;

    private bool getWeapon = false;
    private int curAmountExp = 0;
    
    void Start()
    {
        _text.enabled = false;

    }

    public void GetExp(int expAmount)
    {
        if (!getWeapon)
        {
            curAmountExp += expAmount;
            _text.color = new Color(255, 174, 0);
            showExp = true;
            _text.enabled = true;
            _text.text = curAmountExp + " EXP";
            textTime = 1;
        }
    }

    public void GetWeapon(string weaponName)
    {
        getWeapon = true;
        string name = weaponName;
        StartCoroutine("NewWeaponFeedback", name);
        sounds.PlaySound(0);
    }

    IEnumerator NewWeaponFeedback(string weaponName)
    {
        showExp = true;
        _text.enabled = true;
        _text.text = weaponName + " OBTAINED!";
        textTime = 3;

        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(.5F);
        _text.color = new Color(255, 174, 0);

        getWeapon = false;
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
