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

    public float UpMapBounds { get => _upMapBounds; set => _upMapBounds = value; }
    public float DownMapBounds { get => _downMapBounds; set => _downMapBounds = value; }
    public float LeftMapBounds { get => _leftMapBounds; set => _leftMapBounds = value; }
    public float RightMapBounds { get => _rightMapBounds; set => _rightMapBounds = value; }
    public int CurrentNPCPopulation { get => _currentNPCPopulation; set => _currentNPCPopulation = value; }

    private void Start()
    {
        SoundManager.Instance.AudioData.PlayOneShot(SoundManager.Instance.StageMusic);
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

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(_leftMapBounds, _upMapBounds), new Vector3(_leftMapBounds, _downMapBounds));
    }
}
