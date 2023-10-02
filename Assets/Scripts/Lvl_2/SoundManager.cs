using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _laserLoops;
    [SerializeField] private AudioClip _loopSound;

    private double _playTime;
    private double _clipDuration;
    private int _audioToggle;

    private void Awake()
    {
        foreach (AudioSource source in _laserLoops)
            source.clip = _loopSound;
        _clipDuration = (double)_loopSound.samples / _loopSound.frequency;
    }

    private void Start()
    {
        _playTime = AudioSettings.dspTime + .5d;
        _laserLoops[_audioToggle].PlayScheduled(_playTime);
        _playTime += _clipDuration;
    }

    private void Update()
    {
        if (AudioSettings.dspTime > _playTime - 1)
        {
            PlayScheduledClip();
        }
    }

    private void PlayScheduledClip()
    {
        _laserLoops[_audioToggle].PlayScheduled(_playTime);
        _playTime += _clipDuration;
        _audioToggle = 1 - _audioToggle;
    }
}