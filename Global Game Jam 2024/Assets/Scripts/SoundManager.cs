using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _stageMusic;
    [SerializeField] private AudioClip _punchSound;
    [SerializeField] private AudioClip _slamSound;
    [SerializeField] private AudioClip _laughSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
