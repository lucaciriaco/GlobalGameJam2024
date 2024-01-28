using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    
    [SerializeField] private float _horizontalMoveSpeed = 3f;
    [SerializeField] private float _verticalMoveSpeed = 1.5f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;

    void Update()
    {
        HandleMovement();
        HandlePunch();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, new Vector2(horizontalInput * _horizontalMoveSpeed, verticalInput * _verticalMoveSpeed), Time.deltaTime);

        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void DetectEnemy()
    {
        var result = Physics.OverlapSphere(transform.position + _offset, _radius);
        if(result != null)
        {
            for (int i = 1; i < result.Length; i++)
            {
                if(result[i].gameObject.GetComponent<IPunchable>() != null)
                    result[i].gameObject.GetComponent<IPunchable>().PersonPunched();
            }
        }
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }

    private void HandlePunch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            DetectEnemy();
        }
    }
}
