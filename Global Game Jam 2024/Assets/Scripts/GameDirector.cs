using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    [SerializeField] private GameObject _NPC;

    [SerializeField] private int _currentNPCPopulation;
    [SerializeField] private int _maxNPCPopulation;


    [SerializeField] private float _upMapBounds;
    [SerializeField] private float _downMapBounds;
    [SerializeField] private float _leftMapBounds;
    [SerializeField] private float _rightMapBounds;

    public float UpMapBounds { get => _upMapBounds;}
    public float DownMapBounds { get => _downMapBounds;}
    public float LeftMapBounds { get => _leftMapBounds;}
    public float RightMapBounds { get => _rightMapBounds;}
    public int CurrentNPCPopulation { get => _currentNPCPopulation; set => _currentNPCPopulation = value; }

    private void Start()
    {
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
        var randomPosition = new Vector3(Random.Range(_leftMapBounds + 5f, _rightMapBounds - 5f), Random.Range(_downMapBounds + 5f, _upMapBounds - 5f));
        Instantiate(_NPC, randomPosition, Quaternion.identity, this.gameObject.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(_leftMapBounds, _upMapBounds), new Vector3(_leftMapBounds, _downMapBounds));
    }
}
