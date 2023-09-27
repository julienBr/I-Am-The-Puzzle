using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sourceStart;
    [SerializeField] private AudioSource _sourceLoop;
    [SerializeField] private AudioSource _sourceEnd;

    private double _startTime;
    private double _durationClip0;
    private double _durationClip1;
    
    private void Awake()
    {
        _startTime = AudioSettings.dspTime + 2;
        _durationClip0 = _sourceStart.clip.samples / _sourceStart.clip.frequency;
        _durationClip1 = _sourceLoop.clip.samples / _sourceLoop.clip.frequency;
        
    }

    private void Start()
    {
        _sourceStart.PlayScheduled(_startTime);
        _sourceLoop.PlayScheduled(_startTime + _durationClip0);
        _sourceEnd.PlayScheduled(_startTime + _durationClip1);
    }
}