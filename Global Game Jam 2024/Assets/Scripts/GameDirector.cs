using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    [SerializeField] private GameObject _NPC;

    [SerializeField] private float totalTime = 30f;

    [SerializeField] private int _currentNPCPopulation;
    [SerializeField] private int _maxNPCPopulation;

    [SerializeField] private PolygonCollider2D _polygonCollider;

    [SerializeField] private float _upMapBounds;
    [SerializeField] private float _downMapBounds;
    [SerializeField] private float _leftMapBounds;
    [SerializeField] private float _rightMapBounds;

    [SerializeField] private Vector2 spawnBoxSize = new Vector2(5f, 5f);

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

    private void OnDrawGizmos()
    {
        if (_polygonCollider == null)
        {
            _polygonCollider = GetComponent<PolygonCollider2D>();
        }

        if (_polygonCollider != null && _polygonCollider.points.Length > 0)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < _polygonCollider.points.Length; i++)
            {
                Vector3 currentPoint = transform.TransformPoint(_polygonCollider.points[i]);
                Vector3 nextPoint = transform.TransformPoint(_polygonCollider.points[(i + 1) % _polygonCollider.points.Length]);

                Gizmos.DrawLine(currentPoint, nextPoint);
            }
        }
    }

}
