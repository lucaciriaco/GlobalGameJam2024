using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    [SerializeField] private GameObject _NPC;

    [SerializeField] private float totalTime = 30f;
    [SerializeField] private int _maxNPCPopulation;
    [SerializeField] private float _upMapBounds = -2.31f;
    [SerializeField] private float _downMapBounds = -7.48f;
    [SerializeField] private float _leftMapBounds = -27.91f;
    [SerializeField] private float _rightMapBounds = 37.14f;

    [SerializeField] private Vector2 spawnBoxSize = new Vector2(5f, 5f);

    private int _currentNPCPopulation;
    private float currentTime;
    private int score = 0;

    public float UpMapBounds { get => _upMapBounds;}
    public float DownMapBounds { get => _downMapBounds;}
    public float LeftMapBounds { get => _leftMapBounds;}
    public float RightMapBounds { get => _rightMapBounds;}
    public int CurrentNPCPopulation { get => _currentNPCPopulation; set => _currentNPCPopulation = value; }
    public int Score { get => Score; set => Score = value; }

    private void Start()
    {
        currentTime = totalTime;
        SoundManager.Instance.AudioData.PlayOneShot(SoundManager.Instance.StageMusic);
        for(int i=0; i <= _maxNPCPopulation; i++)
        {
            SpawnNPC();
        }
    }

    private void Update()
    {
        if (_currentNPCPopulation < _maxNPCPopulation)
            SpawnNPC();
    }

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

    private void SpawnNPC()
    {
        float randomX = Random.Range(-spawnBoxSize.x / 2f, spawnBoxSize.x / 2f);
        float randomY = Random.Range(-spawnBoxSize.y / 2f, spawnBoxSize.y / 2f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(_NPC, spawnPosition, Quaternion.identity, this.gameObject.transform);
    }

}
