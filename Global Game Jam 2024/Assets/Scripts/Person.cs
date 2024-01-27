using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour, IPunchable
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _changeDirectionInterval = 3f;
    [SerializeField] private float _timer;

    private States _currentState;
    Vector2 _randomDirection;
    private States CurrentState { get => _currentState; set => _currentState = value; }

    private enum States
    {
        Walking,
        Punched,
    }

    private void Start()
    {
        ChangeDirection();
        _currentState = States.Walking;
        _timer = _changeDirectionInterval;
    }

    private void Update()
    {
        StateMachine();
    }

    private void StateMachine()
    {
        switch (_currentState)
        {
            case States.Walking:
                Walking();
                break;
            case States.Punched:

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

    private Vector2[] _availableDirections = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    private Vector2 _previousDirection;

    private Vector2 RandomDirection()
    {
        var dir = _availableDirections[Random.Range(0, 4)];

        while (dir != _previousDirection)
        {
            dir = _availableDirections[Random.Range(0, 4)];
        }

        return dir;
    }

    public void PersonPunched()
    {
        CurrentState = States.Punched;
    }
}
