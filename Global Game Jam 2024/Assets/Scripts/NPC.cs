using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IPunchable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _changeDirectionInterval = 3f;
    [SerializeField] private float _timer;
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] private SpriteRenderer _sprite;

    private float _timeToChangeState;
    private float _upMapBounds;
    private float _downMapBounds;
    private float _leftMapBounds;
    private float _rightMapBounds;

    private States _currentState;
    Vector2 _randomDirection;
    private States CurrentState { get => _currentState; set => _currentState = value; }

    private enum States
    {
        Walking,
        Sad,
        Punched,
        Laughing,
    }

    private void Start()
    {
        ChangeDirection();
        _currentState = States.Walking;
        _timer = _changeDirectionInterval;
        _upMapBounds = GameDirector.Instance.UpMapBounds;
        _downMapBounds = GameDirector.Instance.DownMapBounds;
        _leftMapBounds = GameDirector.Instance.LeftMapBounds;
        _rightMapBounds = GameDirector.Instance.RightMapBounds;
        GameDirector.Instance.CurrentNPCPopulation = GameDirector.Instance.CurrentNPCPopulation + 1;
    }

    private void Update()
    {
        var position = this.gameObject.transform.position;
        var epsilon = 0.01f;
        if (position.y > _upMapBounds)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _upMapBounds - epsilon);
        }
        else if (position.y < _downMapBounds)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _downMapBounds + epsilon);
        }
        if (position.x > _rightMapBounds)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _rightMapBounds - epsilon);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if
            (position.x < _leftMapBounds)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _leftMapBounds - epsilon);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        switch (_currentState)
        {
            case States.Walking:
                Walking();
                break;
            case States.Sad:

                break;
            case States.Punched:

                break;
            case States.Laughing:
                GameDirector.Instance.Score = GameDirector.Instance.Score + 10;
                break;
        }
    }

    private void Walking()
    {
        _timeToChangeState = Random.Range(0, 15);
        _boxCollider.enabled = false;
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            ChangeDirection();
            _timer = _changeDirectionInterval;
        }
        if (_timer == _timeToChangeState) 
        {

        }
        transform.Translate(_randomDirection * _speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        _randomDirection = RandomDirection();
    }

    private Vector2[] _availableDirections = new Vector2[15] { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.right, Vector2.right, Vector2.right, Vector2.left, Vector2.left, Vector2.left, Vector2.up, Vector2.down };

    private Vector2 _previousDirection;

    private Vector2 RandomDirection()
    {
        var dir = _availableDirections[Random.Range(0, _availableDirections.Length)];

        while (dir == _previousDirection)
        {
            dir = _availableDirections[Random.Range(0, _availableDirections.Length)];
        }
        _previousDirection = dir;
        return dir;
    }

    public void PersonPunched()
    {
        CurrentState = States.Punched;
    }

    private void OnDisable()
    {
        GameDirector.Instance.CurrentNPCPopulation = GameDirector.Instance.CurrentNPCPopulation - 1;
    }
}
