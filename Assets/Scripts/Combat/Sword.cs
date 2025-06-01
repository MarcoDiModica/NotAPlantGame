using System.Collections;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.XR;
using System;

public class Sword : MonoBehaviour
{
    private Transform hand;
    public TextMeshProUGUI debugText;
    public Guards[] guards;

    public float POSITION_WINDOW = 0.6f;
    public float ROTATION_WINDOW = 12f;

    private Camera camera;
    private ParticleSystem parryVFX; 
    private SwordSfx swordSFX;

    private XRNode controllerNode = XRNode.RightHand;

    public UnityEvent playerHitEvent;
    public UnityEvent<int , AtkDirection> playerParryEvent;

    public AudioClip too_high;
    public AudioClip too_low;
    public AudioClip too_right;
    public AudioClip too_left;
    public AudioSource source;

    void Awake()
    {

    }

    private void Start()
    {
        controllerNode = SettingsManager.Instance.hand;
        camera = Camera.main;
       
        parryVFX = GetComponentInChildren<ParticleSystem>();
        swordSFX = GetComponent<SwordSfx>();
        source = GetComponent<AudioSource>();
    }

    public void GrabSword(Transform hand)
    {
        this.hand = hand;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print ( "Pos " + relativePosition() + " Rot " + relativeRotation() );

        }

        PrintGuardDirection(GetCurrentGuard());
        miss_timer -= Time.deltaTime;

    }

    public void OnMonsterAttack(AtkDirection dir, float time)
    {
        StartCoroutine(CheckParry(dir));
    }

    private int parryStreak = 0;

    private IEnumerator CheckParry(AtkDirection dir)
    {
        for(float i = 0; i < 1f; i += Time.deltaTime)
        {
            if (dir == GetCurrentGuard())
            {
                // Parry
                parryVFX?.Play();
                swordSFX?.PlaySwordSFX();
                SwordVibration.SendHapticImpulse(controllerNode, 1, 0.3f);
                playerParryEvent?.Invoke(parryStreak++, dir);
                yield break ;
            }
            else
            {
                yield return null;
            }
        }

        //NO Parry on time
        parryStreak = 0;

        playerHitEvent?.Invoke();

        Vector3 absolute_wrongness = new Vector3(Mathf.Abs(wrongness.x), Mathf.Abs(wrongness.y), Mathf.Abs(wrongness.z));
        // Detect whihc axis its the wrongest in
        if(absolute_wrongness.x > absolute_wrongness.y && absolute_wrongness.x > absolute_wrongness.z)
        {
            if(wrongness.x > 0)
            {
                print("More to the left");
                PlayMissAudio(too_right);
            }
            else
            {
                print("more to the right");
                PlayMissAudio(too_left);
            }
        }
        else if (absolute_wrongness.y > absolute_wrongness.x && absolute_wrongness.y > absolute_wrongness.z)
        {
            if (wrongness.y > 0)
            {
                print("More down");
                PlayMissAudio(too_high);
            }
            else
            {
                print("more up");
                PlayMissAudio(too_low);
            }
        }
        else if (absolute_wrongness.z > absolute_wrongness.x && absolute_wrongness.z > absolute_wrongness.y)
        {
            if (wrongness.z > 0)
            {
                print("More wide");
            }
            else
            {
                print("more closed");
            }
        }


    }

    float miss_timer = 0f;
    void PlayMissAudio(AudioClip audio)
    {
        if(miss_timer < 0f)
        {
            source?.PlayOneShot(audio);
            miss_timer = 2f;
        }
    }

    Vector3 wrongness;

    AtkDirection GetCurrentGuard()
    {
        for (int i = 0; i < guards.Length; ++i)
        {
            for(int j = 0; j < guards[i].stances.Count; ++j)
            {
                var result = CheckStance(guards[i].stances[j]);
                if (result.Item1)
                {
                    return guards[i].direction;
                }
                else
                {
                    wrongness = result.Item2;   
                }
            }
        }
        return AtkDirection.None;
    }

    public (bool, Vector3) CheckStance( SwordStance stance)
    {
        Vector3 posDifference = relativePosition() - stance.position;
        Vector3 rotationDifference =  new Vector3( /* DeltaAngle returns the shortest distance between 2 angles. ex: DeltaAngle(355 , 5) = 10 */
            Mathf.DeltaAngle(stance.rotation.x, relativeRotation().x),
            Mathf.DeltaAngle(stance.rotation.y, relativeRotation().y),
            Mathf.DeltaAngle(stance.rotation.z, relativeRotation().z)
        );

        bool is_guarding = true;
        if (Mathf.Abs(posDifference.x) > POSITION_WINDOW) { is_guarding = false; }
        if (Mathf.Abs(posDifference.y) > POSITION_WINDOW) { is_guarding = false; }
        if (Mathf.Abs(posDifference.z) > POSITION_WINDOW) { is_guarding = false; }

        if (Mathf.Abs(rotationDifference.x) > ROTATION_WINDOW) { is_guarding = false; }
        if (Mathf.Abs(rotationDifference.y) > ROTATION_WINDOW) { is_guarding = false; }
        if (Mathf.Abs(rotationDifference.z) > ROTATION_WINDOW) { is_guarding = false; }


        // Calculate the wrongness

        Vector3 wrongness = new Vector3(posDifference.x, posDifference.y, posDifference.z);


        return (is_guarding, wrongness );
    }

    void PrintGuardDirection(AtkDirection dir)
    {
        switch (dir)
        {
            case AtkDirection.None:
                //print("defenseless");
                break;
            case AtkDirection.Left:
                print("Left Guard");
                break;
            case AtkDirection.Right:
                print("Right guard");
                break;
            case AtkDirection.Up:
                print("Up guard");
                break;
        }
    }

    Vector3 relativeRotation()
    {
        Quaternion cameraWorldRotationInverse = Quaternion.Inverse(camera.transform.rotation);
        Quaternion objectRotationRelativeCamera = cameraWorldRotationInverse * hand.rotation;

        // You can then access the Euler angles of this relative rotation if needed:
        Vector3 relativeEulerAngles = objectRotationRelativeCamera.eulerAngles;
        return relativeEulerAngles;
    }

    Vector3 relativePosition()
    {
        return camera.transform.InverseTransformPoint(hand.position);
    }



}
