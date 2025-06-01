using UnityEngine;

using UnityEngine.XR;

public class SwordAtk : MonoBehaviour
{
    private XRNode targetNode = XRNode.RightHand;
    private InputDevice targetDevice;
    private Vector3 velocity;

    private Collider swordCollider;
    [Tooltip("How fas the swing of the sword must be to be recognized")]
    public float swordStrengthRequirement = 0.8f;

    private void Awake()
    {
        swordCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        targetNode = SettingsManager.Instance.hand;
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            targetDevice = InputDevices.GetDeviceAtXRNode(targetNode);
        }

        if (targetDevice.isValid)
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity))
            {
                //Debug.Log($"Right Hand Velocity (XR): {velocity.magnitude}");

                swordCollider.enabled = velocity.magnitude > swordStrengthRequirement ? true : false;

            }
        }
    }
}
