using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RatingPop : MonoBehaviour
{
    public Sprite[] sprites;
    private Image renderer;

    public Transform right_trans;
    public Transform  left_trans;
    public int i = 0;

    public float offset = 15;

    void Awake()
    {
        renderer = GetComponent<Image>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Poppt1(i++, (AtkDirection) Random.Range(0,2) );
        }
    }

    public void Poppt1(int streak, AtkDirection dir)
    {
        renderer.color = new Color(1,1,1,1);
        if (dir == AtkDirection.Left)
        {
            transform.position = left_trans.position + ((Vector3)  Random.insideUnitCircle * offset) ;       
        }
        else
        {
            transform.position = right_trans.position + ((Vector3) Random.insideUnitCircle * offset);
        }
        renderer.sprite = sprites[streak % sprites.Length];
        transform.DOScale(1.8f, 0.6f).OnComplete( () => transform.DOScale(1f, 0.6f).OnComplete(() => renderer.color = new Color(1, 1, 1, 0)) ) ;
        
    }
}
