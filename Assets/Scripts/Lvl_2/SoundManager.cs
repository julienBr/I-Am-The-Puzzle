using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _tripodLaserLoop;

    private void OnEnable() { Transmitter.PlaySound += PlayLaserSound; }
    private void OnDisable() { Transmitter.PlaySound -= PlayLaserSound; }

    private void PlayLaserSound(int idTripod, bool play)
    {
        if (idTripod == 0) PlayClip(_tripodLaserLoop[0], play);
        else if (idTripod == 1) PlayClip(_tripodLaserLoop[1], play);
        else if (idTripod == 2) PlayClip(_tripodLaserLoop[2], play);
        else PlayClip(_tripodLaserLoop[3], play);
    }

    private void PlayClip(AudioSource source, bool play)
    {
        StartCoroutine(play ? StartAudioClip(source) : StopAudioClip(source));
    }

    private IEnumerator StartAudioClip(AudioSource source)
    {
        if (!source.isPlaying)
        {
            source.volume = 0f;
            source.Play();
            while (source.volume < .5f)
            {
                source.volume += .01f;
                yield return new WaitForSeconds(.01f);
            }
        }
    }
    
    private IEnumerator StopAudioClip(AudioSource source)
    {
        source.volume = .5f;
        if (source.isPlaying)
        {
            while (source.volume > 0f)
            {
                source.volume -= .01f;
                yield return new WaitForSeconds(.01f);
            }
            source.Stop();
        }
    }
}