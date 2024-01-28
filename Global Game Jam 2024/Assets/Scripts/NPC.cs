using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IPunchable
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _changeDirectionInterval = 3f;
    [SerializeField] private float _timer;

    private float _maxUpwardsDirection;
    private float _maxDownwardsDirection;
    private States _currentState;
    Vector2 _randomDirection;
    private States CurrentState { get => _currentState; set => _currentState = value; }

    private enum States
    {
        Walking,
        Punched,
        Laughing,
    }

    private void Start()
    {
        ChangeDirection();
        _currentState = States.Walking;
        _timer = _changeDirectionInterval;
        _maxUpwardsDirection = GameDirector.Instance.UpMapBounds;
        _maxDownwardsDirection = GameDirector.Instance.DownMapBounds;
        GameDirector.Instance.CurrentNPCPopulation = GameDirector.Instance.CurrentNPCPopulation + 1;
    }

    private void Update()
    {
        var position = this.gameObject.transform.position;
        var epsilon = 0.01f;
        if (position.y > _maxUpwardsDirection)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _maxUpwardsDirection - epsilon);
        }
        else if (position.y < _maxDownwardsDirection)
        {
            _randomDirection *= -1;
            this.gameObject.transform.position = new Vector2(position.x, _maxDownwardsDirection + epsilon);
        }

        switch (_currentState)
        {
            case States.Walking:
                Walking();
                break;
            case States.Punched:

                break;
            case States.Laughing:

                break;
        }
    }

    private void Walking()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            ChangeDirection();
            _timer = _changeDirectionInterval;
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
