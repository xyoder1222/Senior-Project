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
    public int[] playerAbilitySets;
    private int abilitySet;
    public float backwardsforce = -2f;
    public ParticleSystem smokescreen;
    public Vector3 playerLocation;
    public float invisibleDuration = 5f;
    public MeshRenderer playerRenderer;
    public GameObject grenade;
    public float throwForce = 500f, throwUpForce = 100f;
    [SerializeField] private Rigidbody grenadeRB;
    public Transform throwPosition;
    public Transform grenadeSpawnPos;
    public Camera main;

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

    public void OnAbility1()
    {
        playerLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Quaternion rotation = Quaternion.identity;
        ParticleSystem playerSmokeScreen = Instantiate(smokescreen, playerLocation, rotation);
        playerRenderer.enabled = false;
        Destroy(playerSmokeScreen, 5f);
        StartCoroutine(InvisibilityDuration());
    }

    public void OnSecondaryFire()
    {
        Quaternion rotation = Quaternion.identity;
        GameObject grenadeThrown = Instantiate(grenade, grenadeSpawnPos.position, rotation);
        grenadeRB = grenadeThrown.GetComponent<Rigidbody>();
        grenadeRB.isKinematic = false;
        Vector3 throwVector = main.transform.forward * throwForce + player.transform.up * throwUpForce;
        grenadeRB.AddForce(throwVector, ForceMode.Impulse);
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

    IEnumerator InvisibilityDuration()
    {
        yield return new WaitForSeconds(invisibleDuration);
        playerRenderer.enabled = true;
    }
}
