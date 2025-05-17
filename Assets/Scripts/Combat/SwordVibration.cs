using UnityEngine;
using System.Collections;
using UnityEngine.XR;
public class SwordVibration : MonoBehaviour
{
    public static void SendHapticImpulse(XRNode hand, float amplitude, float duration)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        if (device.isValid && device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
        {
            device.SendHapticImpulse(0, amplitude, duration);
        }
    }
}
