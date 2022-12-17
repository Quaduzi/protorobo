using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> audioToPlay;
    [Range(0, 1)]
    public float volume = 1;
    
    private AudioSource _audioSource;
    private Random _rnd;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rnd = new Random();
    }

    public void PlayRandom()
    {
        if (audioToPlay.Count == 0) return;
        _audioSource.clip = audioToPlay[_rnd.Next(audioToPlay.Count)];
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    public bool IsPlaying()
    {
        return _audioSource.isPlaying;
    }
}
