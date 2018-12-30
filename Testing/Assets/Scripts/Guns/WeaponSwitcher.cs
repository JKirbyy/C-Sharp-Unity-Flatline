using UnityEngine;
using System.Collections;

public class WeaponSwitcher : MonoBehaviour 
{
	public GameObject[] Guns; //holds the guns.
	private int x; // used as a Guns array index.
    public string currentGun; //returns the name of the current gun.
    public Gun gunScript;
    public Texture2D standardReticle;
    public Texture2D shotgunReticle;
    public CursorMode cursorMode = CursorMode.Auto; //means the cursor can be rendered by the hardware or the software.
    public Vector2 hotSpot = new Vector2(16, 16); //where the reticle is centered. The image is 32 x 32 so this would be the middle.

    void Start () 
	{
        hotSpot = new Vector2(16, 16);
        x = 0;
		Guns [x].SetActive (true);
        Guns[2].SetActive(true);
        gunScript = GameObject.Find("HeavyTurret").GetComponent<Gun>();
	}


	void Update () 
	{
        currentGun = Guns[x].transform.name;
     
        if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (x == 1) 
			{
				Guns [x].SetActive (false);
				Guns [0].SetActive (true);
                x = 0;
			} 

			else if (x == 0)
			{
				Guns [x].SetActive (false);
				Guns [x + 1].SetActive (true);
                x = x + 1;
			}          
		}

        if (x == 0)
        {
            hotSpot = new Vector2(16, 16);
            Cursor.SetCursor(standardReticle, hotSpot, cursorMode);
        }

        if (x == 1)
        {
            hotSpot = new Vector2(16, 16);
            Cursor.SetCursor(shotgunReticle, hotSpot, cursorMode);
        }

    }
    
    public void WeaponReset ()
    {
        Guns[x].SetActive(false);
        Guns[0].SetActive(true);
        x = 0;
    }
}
