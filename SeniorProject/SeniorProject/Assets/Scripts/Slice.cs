using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public Rigidbody player;
    public float backwardsforce = -2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    public void OnSecondaryFire()
    {
        player.AddForce(transform.forward * -backwardsforce, ForceMode.Impulse);
    }
}
