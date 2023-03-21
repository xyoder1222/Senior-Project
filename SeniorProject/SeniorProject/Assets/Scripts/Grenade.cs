using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    /*
     * Grenade code that needs to be used in PlayerInput script
     * 
     * Quaternion rotation = Quaternion.identity;
        GameObject grenadeThrown = Instantiate(grenade, grenadeSpawnPos.position, rotation);
        grenadeRB = grenadeThrown.GetComponent<Rigidbody>();
        grenadeRB.isKinematic = false;

        Vector3 forceDirection = main.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(main.transform.position, main.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - axeSpawnPoint.position).normalized;
        }
            Vector3 throwVector = forceDirection * grenadethrowForce * throwUpForce;
            grenadeRB.AddForce(throwVector, ForceMode.Impulse);
            grenadeRB.AddTorque(transform.right * 99999999f, ForceMode.Impulse);
     * 
     */

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explsionFX;

    float countdown;
    bool hasExploded = false;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        GameObject Explosion = Instantiate(explsionFX, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
        Destroy(Explosion, 1f);
    }
}

