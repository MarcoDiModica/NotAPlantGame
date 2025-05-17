using TMPro;
using UnityEditor;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Transform hand;
    public TextMeshProUGUI debugText;
    public Guards[] guards;

    public float POSITION_WINDOW = 0.6f;
    public float ROTATION_WINDOW = 12f;

    void Awake()
    {
        hand = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            debugText.text = "Pos " + hand.transform.localPosition.ToString() + " Rot " + hand.transform.localEulerAngles;

        }

        PrintGuardDirection(GetCurrentGuard());


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

    bool CheckStance(SwordStance stance)
    {
        Vector3 posDifference = hand.localPosition - stance.position;
        Vector3 rotationDifference =  new Vector3( /* DeltaAngle returns the shortest distance between 2 angles. ex: DeltaAngle(355 , 5) = 10 */
            Mathf.DeltaAngle(stance.rotation.x, hand.localEulerAngles.x),
            Mathf.DeltaAngle(stance.rotation.y, hand.localEulerAngles.y),
            Mathf.DeltaAngle(stance.rotation.z, hand.localEulerAngles.z)
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
               // print("defenseless");
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

}
