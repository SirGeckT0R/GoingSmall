using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource MusicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }
    public void PlaySound(AudioClip clip, float volume = 1f, float p1 = 0.85f, float p2 = 1f)
    {
        SFXSource.pitch = Random.Range(p1,p2);
        SFXSource.PlayOneShot(clip, volume);
    }
}
