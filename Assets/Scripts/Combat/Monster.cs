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

    [Header("Fields for opened state")]
    public float chanceForOpening = 0.4f;
    private ParticleSystem hitVFX;

    private SwordSfx sfx;
    private AudioSource source;

    void Awake()
    {
        coolDownTimer = baseTimer;
        hitVFX = GetComponentInChildren<ParticleSystem>();
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
        sfx = GetComponent<SwordSfx>();
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0.1f, 0.4f);
    }

    void Spawn()
    {
        transform.position = Camera.main.transform.position + (transform.forward * 2);
    }

    public void BlinkDamage()
    {
        StartCoroutine(BlinkDamageRoutine());
        hitVFX.Play();
        sfx.PlaySwordSFX();
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

    public void Opened()
    {
        renderer.material.color = new Color(1f, 0.4f, 0.7f);
    }

    public void Attacking()
    {
        renderer.material.color = new Color(0.2f, 0.2f, 0.2f);
    }

}
