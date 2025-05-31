using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class RatingPop : MonoBehaviour
{
    public Sprite[] sprites;
    public AudioClip[] audios;
    private Image renderer;
    private AudioSource source;
    public Transform right_trans;
    public Transform  left_trans;
    public int i = 0;

    public float offset = 15;

    private SwordSfx sfxSource;

    public int total_points;


    void Awake()
    {
        renderer = GetComponent<Image>();
        sfxSource = GetComponent<SwordSfx>();
        source = GetComponent<AudioSource>();

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
        total_points += (streak % sprites.Length);
        print("total points " + total_points);
        renderer.color = new Color(1,1,1,1);
        sfxSource.PlaySwordSFX();
        source?.PlayOneShot(audios[streak % audios.Length]);
        if (dir == AtkDirection.Left)
        {
            transform.position = left_trans.position + ((Vector3)  Random.insideUnitCircle * offset) ;       
        }
        else
        {
            transform.position = right_trans.position + ((Vector3) Random.insideUnitCircle * offset);
        }
        renderer.sprite = sprites[streak % sprites.Length];
        transform.DOScale(1.8f, 0.4f).OnComplete( () => transform.DOScale(1f, 0.2f).OnComplete(() => renderer.color = new Color(1, 1, 1, 0)) ) ;
        
    }

    void CalculateDrops(int total_enemt_hp )
    {

        float multiplier = total_points / (total_enemt_hp * 5);

    }

}
