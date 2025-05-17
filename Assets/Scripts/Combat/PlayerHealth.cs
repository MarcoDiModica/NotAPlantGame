using Ilumisoft.HealthSystem;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;

    public Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.maxHealth = maxHealth;
    }

    public void ReceiveDamage(int dmg)
    {
        health.ApplyDamage(dmg);
        SwordVibration.SendHapticImpulse(UnityEngine.XR.XRNode.RightHand, 1f, 0.3f);
        SwordVibration.SendHapticImpulse(UnityEngine.XR.XRNode.LeftHand, 1f, 0.3f);
    }

}
