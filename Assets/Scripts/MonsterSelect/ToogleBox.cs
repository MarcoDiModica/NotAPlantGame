
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ToogleBox : MonoBehaviour
{

    public XRNode handMode;

    public ToogleBox[] others;
    public Image img;
    public Button toggle;

    public bool is_on;
    Color col;

    private void Awake()
    {
        is_on = false;

        toggle = GetComponent<Button>();
        col = img.color;
    }

    public void Checked()
    {
        is_on = !is_on;

        //>img.enabled = toggle.isOn ;
        if (is_on)
        {
            SettingsManager.Instance.hand = handMode;
            Check();
        }
        else
        {
            UnCheck();
        }
        for (int i = 0; i < others.Length; ++i)
        {
            others[i].UnCheck();
            //others[i].img.enabled = false;
        }
    }

    public void Check() {
        img.color = Color.gray;    
    }

    public void UnCheck()
    {
        img.color = col;
        is_on = false;
    }

}
