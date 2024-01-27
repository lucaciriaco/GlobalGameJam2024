using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource AudioData;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _stageMusic;
    [SerializeField] private AudioClip _punchSound;
    [SerializeField] private AudioClip _slamSound;
    [SerializeField] private AudioClip _laughSound;

    public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
    public AudioClip StageMusic { get => _stageMusic; set => _stageMusic = value; }
    public AudioClip PunchSound { get => _punchSound; set => _punchSound = value; }
    public AudioClip SlamSound { get => _slamSound; set => _slamSound = value; }
    public AudioClip LaughSound { get => _laughSound; set => _laughSound = value; }

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
