using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HeavyGun : MonoBehaviour
{
    private Vector3 directionVector;
    Vector3 worldPosition;
    Vector3 aimDirection;
    public Transform[] bulletSpawns;
    public GameObject bullet;
    float gunCountDown;
    public static float fireRate = 5;
    public Image turretUI;
    Color c = new Color(78, 180, 141);
	public AudioClip shootSound;
	public AudioSource audioSource;

    void Start()
    {
        gunCountDown = 0;
    }

    
    void Update()
    {
        //if (PowerUp.powerUpCheck == true) //optimization!!
        //{
            //fireRate = 0.01f;  caused and error so removed
        //}

        gunCountDown -= Time.deltaTime;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        directionVector = (worldPosition - transform.parent.position).normalized;
        transform.parent.up = directionVector;

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (gunCountDown <= 0)
            {
				if (Gun.isPaused == false) 
				{
					foreach (Transform bulletSpawn in bulletSpawns) 
					{
						audioSource.PlayOneShot (shootSound);
						Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
						gunCountDown = fireRate;
						turretUI.color = Color.grey;
						//weaponSwitcherScript.WeaponReset();
					}
				}
            }
        }

        if (gunCountDown <= 0)
        {
			turretUI.color = Color.green;
        }
    }

    public static void ResetRate ()
    {
        fireRate = 0.2f;
    }

}


