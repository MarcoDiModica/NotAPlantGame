using UnityEngine;
using UnityEngine.XR;

public class LeftRightModeHandler : MonoBehaviour
{

    private XRNode controllerNode = XRNode.RightHand;
    public GameObject leftHandController;
    public GameObject rightHandController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controllerNode = SettingsManager.Instance.hand;
        switch (controllerNode)
        {
            case XRNode.RightHand:
                transform.parent = rightHandController.transform;
                break;
            case XRNode.LeftHand:
                transform.parent = leftHandController.transform;
                break;
        }

        Sword sword = GetComponent<Sword>();
        sword.GrabSword(transform.parent);

    }

    
}
