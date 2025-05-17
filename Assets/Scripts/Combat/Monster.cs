using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    public float baseTimer = 2f;
    private float coolDownTimer;
    [Tooltip("X negative offset , Y positive offset")]
    public Vector2 timerVariation = Vector2.zero;

    //public delegate void AtkEvent(AtkDirection direction);
    //public event AtkEvent atkEvent;
    public UnityEvent<AtkDirection> atkEvent;


    void Awake()
    {
        coolDownTimer = baseTimer;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    void HandleAttack()
    {
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0)
        {
            coolDownTimer = baseTimer + Random.Range(-timerVariation.x, +timerVariation.y);
            //PerformAtk
            atkEvent?.Invoke( (AtkDirection) Random.Range(0,3) );
        }
    }
}
