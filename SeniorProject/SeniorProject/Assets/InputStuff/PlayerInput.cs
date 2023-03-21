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
    public float grenadethrowForce, axethrowForce, throwUpForce;
    [SerializeField] private Rigidbody grenadeRB;
    public Transform throwPosition;
    public Transform grenadeSpawnPos;
    public Camera main;
    public Transform axeSpawnPoint;
    [SerializeField] private Rigidbody axeRB;
    public GameObject axe;
    public GameObject mine, teleporter;
    public Transform mineTelePoss;
    private bool isTeleporterSet, isMinSet = false;
    private GameObject teleporterPlace, minePlace;
    public float radius = 5f;
    public float force = 700f;
    public GameObject explsionFX;

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
        if (isTeleporterSet == false)
        {
            Quaternion rotation = Quaternion.identity;
            teleporterPlace = Instantiate(teleporter, mineTelePoss.position, rotation);
            isTeleporterSet = true;
        }
        else
        {
            transform.position = teleporter.transform.position;
            Destroy(teleporterPlace);
            isTeleporterSet = false;
        }
    }

    public void OnAbility2()
    {
        if(isMinSet == false)
        { 
            Quaternion rotation = Quaternion.identity;
            minePlace = Instantiate(mine, mineTelePoss.position, rotation);
            isMinSet = true;
        }
        else
        {
            GameObject Explosion = Instantiate(explsionFX, minePlace.transform.position, minePlace.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(minePlace.transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, minePlace.transform.position, radius);
                }
            }

            Destroy(minePlace);
            Destroy(Explosion, 1f);
            isMinSet = false;
        }
    }

    public void OnPrimaryFire()
    {

    }

    public void OnSecondaryFire()
    {
        Quaternion rotation = Quaternion.identity;
        GameObject grenadeThrown = Instantiate(grenade, grenadeSpawnPos.position, rotation);
        grenadeRB = grenadeThrown.GetComponent<Rigidbody>();
        grenadeRB.isKinematic = false;

        Vector3 forceDirection = main.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(main.transform.position, main.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - axeSpawnPoint.position).normalized;
        }
        Vector3 throwVector = forceDirection * grenadethrowForce * throwUpForce;
        grenadeRB.AddForce(throwVector, ForceMode.Impulse);
        grenadeRB.AddTorque(transform.right * 99999999f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isMinSet && other.CompareTag("Enemy"))
        {
            GameObject Explosion = Instantiate(explsionFX, minePlace.transform.position, minePlace.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(minePlace.transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, minePlace.transform.position, radius);
                }
            }

            Destroy(minePlace);
            Destroy(Explosion, 1f);
            isMinSet = false;
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

    IEnumerator InvisibilityDuration()
    {
        yield return new WaitForSeconds(invisibleDuration);
        playerRenderer.enabled = true;
    }
}
