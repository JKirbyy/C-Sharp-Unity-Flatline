using UnityEngine;
using System.Collections;


public class Gun : MonoBehaviour
{
    Vector3 worldPosition; //the mouse position.
    private Vector3 directionVector; //the direction of the gun. 
    public Transform[] bulletSpawns; //array of bullet spawns.
    public GameObject bullet; //the bullet to be fired.
    float gunCountDown; //counts down from fire rate.
    WeaponSwitcher weaponSwitcherScript; //the weapon switcher class.
    public static float fireRate = 0.2f; //the fire rate of the weapon.
	public AudioClip shootSound;
	public AudioClip shotgunSound;
	public AudioSource audioSource;
	public static bool isPaused;
    
	void Start()
    {
		isPaused = false;
		gunCountDown = 0;
        weaponSwitcherScript = transform.parent.GetComponent<WeaponSwitcher>();
    }

    
    void Update()
    {
        if (PowerUp.powerUpCheck == true) //optimization!!
        {
            fireRate = 0.01f;
        }

        gunCountDown -= Time.deltaTime;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        directionVector = (worldPosition - transform.parent.position).normalized;
        transform.parent.up = directionVector;

        //transform.LookAt(worldPosition, transform.parent.forward);
        //transform.parent.LookAt(worldPosition);

        if (weaponSwitcherScript.currentGun == "Gun") //currently equipped
        {
            if (Input.GetButton("Fire1")) //left Mouse click.
            {
                if (gunCountDown <= 0)
                {
					if (isPaused == false) 
					{
						audioSource.PlayOneShot (shootSound);
						foreach (Transform bulletSpawn in bulletSpawns) 
						{
							Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
							gunCountDown = fireRate;
						}
					}
                }
            }
        }

        if (weaponSwitcherScript.currentGun == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (gunCountDown <= 0)
                {
					if (isPaused == false) 
					{
						audioSource.PlayOneShot (shotgunSound);
						foreach (Transform bulletSpawn in bulletSpawns) 
						{
							Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
							gunCountDown = fireRate;
						}
					}
                }
            }
        }

            if (weaponSwitcherScript.currentGun == "HeavyTurret")
            {
                if(gunCountDown <= 0)
                {
                    foreach (Transform bulletSpawn in bulletSpawns)
                    {
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                        gunCountDown = fireRate;
                        //weaponSwitcherScript.WeaponReset();
                    }
                }
            }        
    }

    public static void ResetRate ()
    {
        fireRate = 0.2f;
    }

    public void HeavyTurret ()
    {
        if (gunCountDown <= 0)
        {
            foreach (Transform bulletSpawn in bulletSpawns)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                gunCountDown = fireRate;
                //weaponSwitcherScript.WeaponReset();
            }
        }
    }
}


