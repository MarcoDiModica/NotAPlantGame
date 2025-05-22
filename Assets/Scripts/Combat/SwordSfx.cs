using UnityEngine;

public class SwordSfx : MonoBehaviour
{
    public AudioObject audio;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlaySwordSFX()
    {
        audio.PlayRandomAudioRandomPitchRandomVol(source);
       // source?.PlayOneShot(audio.GetRandomAudio().clip);
    }

}
