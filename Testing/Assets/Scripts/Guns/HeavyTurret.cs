using UnityEngine;
using System.Collections;



public class HeavyTurret : MonoBehaviour
{
    Vector3 worldPosition;
    Vector3 aimDirection;
    public Transform[] bulletSpawns;
    public GameObject bullet;
    public float fireRate;
    float gunCountDown;
    float shotgunCountDown;
    WeaponSwitcher weaponSwitcherScript;

    void Start()
    {
        shotgunCountDown = 0.7f;
        gunCountDown = 0.1f;
        weaponSwitcherScript = transform.parent.GetComponent<WeaponSwitcher>();
    }


    void Update()
    {
        shotgunCountDown -= Time.deltaTime;
        gunCountDown -= Time.deltaTime;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        transform.LookAt(worldPosition);
        transform.parent.LookAt(worldPosition);

        if (weaponSwitcherScript.currentGun == "Gun")
        {
            if (Input.GetButton("Fire1"))
            {
                if (gunCountDown <= 0)
                {
                    foreach (Transform bulletSpawn in bulletSpawns)
                    {
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                        gunCountDown = 0.1f;
                    }
                }
            }
        }

        if (weaponSwitcherScript.currentGun == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (shotgunCountDown <= 0)
                {
                    foreach (Transform bulletSpawn in bulletSpawns)
                    {
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                        shotgunCountDown = 0.7f;
                    }
                }
            }
        }
    }
}


