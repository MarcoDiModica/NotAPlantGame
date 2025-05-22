using System;
using UnityEngine;

[Serializable]
public class AudioObject
{
    [Serializable]
    public class Audio
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume;
        [SerializeField, Range(0, 1)] public float volume_variation;

        public float VolumeVariation()
        {
            return 1f + UnityEngine.Random.Range(-volume_variation, 0);
        }

    }

    [Header("Possible Audios")]
    [SerializeField] Audio[] audios;

    [Header("Config")] /* 2 = twice as fast, an octave higher */
    [SerializeField, Range(1, 2)] private float pitch_variation;


    [Range(0, 1)] public float volume;

    public Audio GetRandomAudio()
    {
        var audio = audios[UnityEngine.Random.Range(0, audios.Length)];
        return audio;
    }

    public void PlayRandomAudioRandomPitchRandomVol(AudioSource source)
    {
        source.pitch = PitchVariation();
        var audio = audios[UnityEngine.Random.Range(0, audios.Length)];
        source.volume = audio.VolumeVariation();
        source.PlayOneShot(audio.clip);
    }

    public float PitchVariation()
    {
        return UnityEngine.Random.Range(1 / pitch_variation, pitch_variation);
    }



}