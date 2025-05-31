using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    private int total_hp;
    public int hp;

    [Header("Fields for attacking state")]
    public float baseTimer = 2f;
    private float coolDownTimer;
    [Tooltip("X negative offset , Y positive offset")]
    public Vector2 timerVariation = Vector2.zero;

    //public delegate void AtkEvent(AtkDirection direction);
    //public event AtkEvent atkEvent;
    public UnityEvent<AtkDirection , float> atkEvent;

    private MeshRenderer renderer;

    [Header("Fields for opened state")]
    public float chanceForOpening = 0.4f;
    private ParticleSystem hitVFX;

    private SwordSfx sfx;
    private AudioSource source;
    [Header("Fields for OnSlaught state")]
    public Onslaught[] onslaughts;

    void Awake()
    {
        total_hp = hp;
        coolDownTimer = baseTimer;
        hitVFX = GetComponentInChildren<ParticleSystem>();
        renderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
        sfx = GetComponent<SwordSfx>();
    }

    Vector3 pos;
    bool has_spawned = false;

    private void Start()
    {
        pos = transform.position;
       // InvokeRepeating("Spawn", 0.1f, 0.4f);
    }

    private void Update()
    {
       // transform.position = pos;
    }

    void Spawn()
    {
        //if (!has_spawned)
        //{
        //    transform.position = new Vector3(pos.x, Camera.main.transform.position.y +1, pos.z);
        //    pos = transform.position;
        //    has_spawned = true;
        //}
        //else { return; }
    }

    public void BlinkDamage()
    {
        StartCoroutine(BlinkDamageRoutine());
        hitVFX.Play();
        sfx.PlaySwordSFX();
        hp--;

        if(hp <= 0)
        {
            //Dead a
            StopAllCoroutines();

            EnemyDrop enemy_drop = GetComponent<EnemyDrop>();
            enemy_drop?.DropLoot(total_hp);

            Destroy(gameObject);
        }

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
    public void StartOnslaughts()
    {
        StartCoroutine(ProcessOnSlaught());
    }

    private IEnumerator ProcessOnSlaught()
    {
        Onslaught onslaught = onslaughts[Random.Range(0, onslaughts.Length)];

        for (int i = 0; i < onslaught.monsterAtks.Length; ++i)
        {
            atkEvent?.Invoke(onslaught.monsterAtks[i].direction, onslaught.monsterAtks[i].time);

            yield return new WaitForSeconds(onslaught.monsterAtks[i].time);
        }
        GetComponent<MonsterStateMachine>().OnChildTransitionEvent(State.OPENED);

    }
}
