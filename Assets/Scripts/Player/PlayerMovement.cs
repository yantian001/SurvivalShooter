using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playRigidbody;
    int floorMask;
    float camRayLength = 100f;

   

    public void Start()
    {

    }

    public void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playRigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        Animating(h, v);

        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay,out floorHit,camRayLength,floorMask))
        {
            Vector3 player2Mouse = floorHit.point - transform.position;
            player2Mouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(player2Mouse);
            playRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h , float v)
    {
        bool walking = h != 0 || v != 0;
        anim.SetBool("IsWalking", walking);
    }
}
