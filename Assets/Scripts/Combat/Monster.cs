using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [Header("Fields for attacking state")]
    public float baseTimer = 2f;
    private float coolDownTimer;
    [Tooltip("X negative offset , Y positive offset")]
    public Vector2 timerVariation = Vector2.zero;

    //public delegate void AtkEvent(AtkDirection direction);
    //public event AtkEvent atkEvent;
    public UnityEvent<AtkDirection> atkEvent;

    private MeshRenderer renderer;

    void Awake()
    {
        coolDownTimer = baseTimer;
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlinkDamage()
    {
        StartCoroutine(BlinkDamageRoutine());
    }

    private IEnumerator BlinkDamageRoutine()
    {
        for(int i = 0; i < 2; ++i)
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.12f);
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.12f);
        }
    }


}
