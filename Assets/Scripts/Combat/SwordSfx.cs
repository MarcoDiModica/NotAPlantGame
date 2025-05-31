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
        float vol_buffer = source.volume;
        audio.PlayRandomAudioRandomPitchRandomVol(source);
        source.pitch = pitch_buffer;
        source.volume = vol_buffer;
       // source?.PlayOneShot(audio.GetRandomAudio().clip);
    }

}
