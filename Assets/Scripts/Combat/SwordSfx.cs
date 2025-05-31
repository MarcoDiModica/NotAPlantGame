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
        float pitch_buffer = source.pitch;
        audio.PlayRandomAudioRandomPitchRandomVol(source);
        source.pitch = pitch_buffer;
       // source?.PlayOneShot(audio.GetRandomAudio().clip);
    }

}
