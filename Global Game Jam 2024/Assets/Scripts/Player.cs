using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    
    [SerializeField] private float _horizontalMoveSpeed = 3f;
    [SerializeField] private float _verticalMoveSpeed = 1.5f;
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 1f;


    private bool isGrounded;
    private bool _isDashing;
    private bool _canDash = true;
    private float _dashTimer;
    private LayerMask _enemies;
    private LayerMask _groundLayer;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float radius;

    void Start()
    {
        _groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        HandleMovement();
        HandlePunch();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _isDashing = false;
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, new Vector2(horizontalInput * _horizontalMoveSpeed, verticalInput * _verticalMoveSpeed), Time.deltaTime);


        // Flip the character based on the direction
        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Ground check using raycasting
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _groundLayer);

        // Cooldown for dash
        if (!_canDash && _dashTimer >= _dashDuration)
        {
            if (_dashTimer >= _dashCooldown)
            {
                _canDash = true;
            }
        }

    }
    void DetectEnemy()
    {
        var result = Physics.OverlapSphere(transform.position + offset, radius);
        if(result[0] != null)
            result[0].gameObject.GetComponent<IPunchable>().PersonPunched();
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }

    private void HandlePunch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            DetectEnemy();
        }
    }

    void HandleDash()
    {
        if (Input.GetButtonDown("Horizontal") && _canDash)
        {
            _isDashing = true;
            _dashTimer = 0f;
        }

        if (_dashTimer < _dashDuration)
        {
            _canDash = false;
            _dashTimer += Time.deltaTime;
        }
        else if (!_canDash)
        {
            _canDash = true;
        }
    }
}
