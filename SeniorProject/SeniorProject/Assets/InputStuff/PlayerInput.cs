using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Vector3 move;
    public Rigidbody player;
    private Vector3 playerVelocity;
    public float jumpheight = 2f;
    private bool jump = false;
    private bool isGrounded = true;
    public GameObject theGround;
    public bool isCrouched = false;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();

        if(jump && isGrounded)
        {
            player.AddForce(0, jumpheight, 0, ForceMode.Impulse);
            jump = false;
            isGrounded = false;
        }
    }

    void Run()
    {
        Vector3 playervelocity = new Vector3(move.x * moveSpeed, player.velocity.y, move.y * moveSpeed);
        player.velocity = transform.TransformDirection(playervelocity);
    }

    public void OnWalk(InputValue input)
    {
        move = input.Get<Vector2>();
    }

    public void OnJump()
    {
        if (isGrounded == true)
        {
            jump = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = true;
        }
    }

    public void OnCrouch()
    {
        if (isCrouched == false)
        {
            playerModel.transform.localScale += new Vector3(0, -.5f, 0);
            isCrouched = true;
        }
        else
        {
            playerModel.transform.localScale += new Vector3(0, .5f, 0);
            isCrouched = false;
        }
    }
}
