using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float despawnTime = 5f;
    public int maxAmmo = 30;
    public float accuracy = 0.1f;
    public float shootDelay = 0.2f;
    public float reloadTime = 1f;

    public int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {

    }

    public void Reload()
    {
        isReloading = true;
        StartCoroutine(ReloadTime());
    }

    void OnReload()
    {
        Reload();
    }

    void OnShoot()
    {
       if(currentAmmo > 0 && isReloading == false)
        { 
            // Calculate accuracy
            Vector3 shootDirection = transform.forward + Random.insideUnitSphere * accuracy;

            // Instantiate bullet at muzzle position
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            // Set bullet velocity
            rb.velocity = shootDirection * bulletForce;
            
            Destroy(bullet, despawnTime);
            currentAmmo--;
       }
       else if(currentAmmo != maxAmmo)
       {
            Reload();
       }

        StartCoroutine(ShootDelay());
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
    }
}
