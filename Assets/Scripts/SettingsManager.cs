using UnityEngine;
using UnityEngine.XR;

public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Instance;

    public XRNode hand = XRNode.RightHand;

    void Awake()
    {
        if(Instance != null) { 
            Destroy(this.gameObject); 
        }
        else { Instance = this; }

        DontDestroyOnLoad(gameObject);
    }


}
