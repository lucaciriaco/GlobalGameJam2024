using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    
    [SerializeField] private float _horizontalMoveSpeed = 3f;
    [SerializeField] private float _verticalMoveSpeed = 1.5f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;
    [SerializeField] private Animator _animator;

    private bool _canPunch;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        var gd = GameDirector.Instance;
        var min = new Vector2(gd.LeftMapBounds, gd.UpMapBounds);
        var max = new Vector2(gd.RightMapBounds, gd.DownMapBounds);

        var delta = new Vector3(horizontalInput, verticalInput);
        var pos = this.gameObject.transform.position + delta;

        pos = Vector2.Min(max, Vector2.Max(min, pos));

        this.gameObject.transform.position = pos;

        _animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate()
    {
        HandlePunch();
    }


    void DetectEnemy()
    {
        var scaleX = transform.localScale.x;
        var result = Physics.OverlapSphere(transform.position + _offset * scaleX, _radius);
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
            if(_canPunch)
                DetectEnemy();
        }
    }

    private void CanPlayerPunch()
    {
       
    }
}
