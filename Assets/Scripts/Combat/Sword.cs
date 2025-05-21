using System.Collections;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.XR;

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

    public XRNode controllerNode = XRNode.RightHand;

    public UnityEvent playerHitEvent;
    public UnityEvent<int , AtkDirection> playerParryEvent;

    void Awake()
    {
        camera = Camera.main;
        hand = transform.parent;
        parryVFX = GetComponentInChildren<ParticleSystem>();
        swordSFX = GetComponent<SwordSfx>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print ( "Pos " + relativePosition() + " Rot " + relativeRotation() );

        }

        PrintGuardDirection(GetCurrentGuard());


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

    }

    AtkDirection GetCurrentGuard()
    {
        for (int i = 0; i < guards.Length; ++i)
        {
            for(int j = 0; j < guards[i].stances.Count; ++j)
            {
                if (CheckStance(guards[i].stances[j]))
                {
                    return guards[i].direction;
                }
            }
        }
        return AtkDirection.None;
    }

    bool CheckStance( SwordStance stance)
    {
        Vector3 posDifference = relativePosition() - stance.position;
        Vector3 rotationDifference =  new Vector3( /* DeltaAngle returns the shortest distance between 2 angles. ex: DeltaAngle(355 , 5) = 10 */
            Mathf.DeltaAngle(stance.rotation.x, relativeRotation().x),
            Mathf.DeltaAngle(stance.rotation.y, relativeRotation().y),
            Mathf.DeltaAngle(stance.rotation.z, relativeRotation().z)
        );

        if (Mathf.Abs(posDifference.x) > POSITION_WINDOW) { return false; }
        if (Mathf.Abs(posDifference.y) > POSITION_WINDOW) { return false; }
        if (Mathf.Abs(posDifference.z) > POSITION_WINDOW) { return false; }

        if (Mathf.Abs(rotationDifference.x) > ROTATION_WINDOW) { return false; }
        if (Mathf.Abs(rotationDifference.y) > ROTATION_WINDOW) { return false; }
        if (Mathf.Abs(rotationDifference.z) > ROTATION_WINDOW) { return false; }

        return true;
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
