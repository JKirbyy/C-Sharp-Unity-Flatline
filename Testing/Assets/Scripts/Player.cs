using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public static float speed = 12; //9.5f
	Vector3 movementVector;
	float xAxis;
	float yAxis;
    public static int health;
    public static bool gameOver;
    public static bool invulnerable;
    public float invulnerableTime;
    public float suicideTime = 15;
    private UIManager managerOfUI;
    Color c;
	public float horizontalClampOffset;
	public float verticalClamp;
	public SpriteRenderer renderer;
	public GameObject[] healthItems;


    void Start () 
	{
		suicideTime = 15;
		Time.timeScale = 1;
		health = 3;
        invulnerable = false;
        managerOfUI = GameObject.Find("Managers").GetComponent<UIManager>();
		renderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

        if (PowerUp.powerUpCheck == true)
        {
            speed = 20;
        }

        if (health <= 0)
        {
            GameOver();
        }
        
        xAxis = Input.GetAxis ("Horizontal"); //the float created by the player pressing A and D.
		yAxis = Input.GetAxis ("Vertical"); // the float created by the player pressing W and S.
		movementVector = new Vector3 (xAxis, yAxis, 0); //a vector that holds the returned values. 0 because there is no z axis in 2D.
		transform.position += (movementVector * speed * Time.deltaTime); //Time.deltaTime makes the process frame rate independent.

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
			if (invulnerable == false) 
			{
				Debug.Log ("suicide called");
				invulnerable = true;
				health = health - 1;
				HealthDrop ();
				StartCoroutine (invulnerableCount (suicideTime));
			}
        }

		float dist = (transform.position - Camera.main.transform.position).z;
		float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0 + horizontalClampOffset, 0, dist)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1 - horizontalClampOffset, 0, dist)).x;

		Vector3 newPos = transform.position;
		newPos.x = Mathf.Clamp(newPos.x, leftBorder, rightBorder);
		newPos.y = Mathf.Clamp(newPos.y, - verticalClamp, verticalClamp);

		transform.position = newPos;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (invulnerable == false)
        {
            if (other.tag == "Mover" || other.tag == "EnemyBullet" || other.tag == "Floater" || other.tag == "Follower" || other.tag == "Circler")
            {
                Destroy(other);
                health = health - 1;
				HealthDrop ();
                invulnerable = true;
				StartCoroutine(invulnerableCount(invulnerableTime));
            }
        }
    }

	IEnumerator invulnerableCount(float waitTime)
    {
		//c = new Color (255, 255, 255, 150);
		//renderer.color = new Color (255, 26, 58);
		renderer.material.SetColor("_Color", Color.red); //the colour of the player
		yield return new WaitForSecondsRealtime (waitTime); //how long the player should be invulnerable for
		renderer.material.SetColor("_Color", Color.white); 
        invulnerable = false;
    }

    IEnumerator suicide()
    {
		Debug.Log ("scall");
		health = health -1;
		yield return new WaitForSeconds(suicideTime);
        invulnerable = false;
    }

    public static void ResetSpeed()
    {
        speed = 9.5f;
    }


    void GameOver()
    {
        Destroy(gameObject);
		Time.timeScale = 0;
		managerOfUI.UnPauseCancel ();
		GameManager.ScoreSave ();
        gameOver = true;
        managerOfUI.ToggleObject(managerOfUI.gameOverScreen);
    }

	void HealthDrop()
	{
		healthItems [health].SetActive (false);
	}

	public void HealthGain()
	{
		health = health + 1;
		//healthItems [health].SetActive (true);
	}
}
