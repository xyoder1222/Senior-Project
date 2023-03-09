using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokescreen : MonoBehaviour
{
    public ParticleSystem smokescreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void OnAbility1()
    {
        playerLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Quaternion rotation = Quaternion.identity;
        ParticleSystem playerSmokeScreen = Instantiate(smokescreen, playerLocation, rotation);
        playerRenderer.enabled = false;
        Destroy(playerSmokeScreen, 5f);
        StartCoroutine(InvisibilityDuration());
    }

    IEnumerator InvisibilityDuration()
    {
        yield return new WaitForSeconds(invisibleDuration);
        playerRenderer.enabled = true;
    }
    */
}
