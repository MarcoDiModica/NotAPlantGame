using UnityEngine;
using UnityEngine.UI;

public class AtkIndicator : MonoBehaviour
{
    private Image img;

    [Header("Images")]
    public Sprite leftWarning;
    public Sprite rightWarning;
    public Sprite topWarning;
    private Sprite defaultSprite;

    private void Awake()
    {
        img = GetComponent<Image>();
        defaultSprite = img.sprite;
    }

    public void AlertAttack(AtkDirection dir)
    {
        switch (dir)
        {
            case AtkDirection.Left:
                img.sprite = leftWarning;
                break;
            case AtkDirection.Right:
                img.sprite = rightWarning;
                break;
            case AtkDirection.Up: 
                img.sprite = topWarning;
                break;
            case AtkDirection.None: 
                img.sprite=defaultSprite;
                break;

        }

        Invoke("ReturnNormalSprite", 0.9f);
    }

    void ReturnNormalSprite()
    {
        img.sprite = defaultSprite;
    }

}
