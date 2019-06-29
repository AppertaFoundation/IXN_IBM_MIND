using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _movement;

    private int _floorMask;
    private float cameraRayLength = 100f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);
    }

    private void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * speed * Time.deltaTime;

        _rigidbody.MovePosition(transform.position + _movement);
    }

    private void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        bool isHit = Physics.Raycast(cameraRay, out floorHit, cameraRayLength, _floorMask);
        if (isHit)
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            _rigidbody.MoveRotation(rotation);
        }
    }

    private void Animating(float h, float v)
    {
        bool isWalking = (0f != h || 0f != v);

        _animator.SetBool("IsWalking", isWalking);
    }
}
