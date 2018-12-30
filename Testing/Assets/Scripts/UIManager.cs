using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text health;
	public Text gameOverScore;
	public Text highScore;
    public Slider powerUpBar;
    public Slider powerDownBar;
	public Slider effectsVolumeSlider;
	public float effectsVolume;
	public Slider musicVolume;
	public AudioSource music;
	public AudioSource standardTurretSound;
	public AudioSource shotgunTurretSound;
	public AudioSource heavyTurretSound;
	public AudioSource floaterSound;
	public AudioSource moverSound;
	public AudioSource travellerSound;
	private float standardTurretVolume = 1;
	private float shotgunTurretVolume = 1;
	private float heavyTurretVolume = 1;
	private float floaterVolume = 0.4f;
	private float moverVolume = 0.4f;
	private float travellerVolume = 0.4f;
    public Texture2D standardReticle;
    public Texture2D shotgunReticle;
    public Texture2D heavyReticle;
    public GameObject gameOverScreen;
	public GameObject pauseMenu;
	public bool canUnPause;
	private float time;

    public Image fill;
    private bool canCall = true;

	void Start()
	{
		canUnPause = true;
		effectsVolume = PlayerPrefs.GetFloat("FX VOL", 0.8f);
		effectsVolumeSlider.value = effectsVolume;
		musicVolume.value = PlayerPrefs.GetFloat("MS Vol",0.9f);
	}

    void Update()
    {
		try
		{
			scoreText.text = GameManager.Score.ToString ();
			powerUpBar.value = PowerUp.count;
			powerDownBar.value = PowerUp.powerDownCount;
			highScore.text = GameManager.highScore.ToString();
			gameOverScore.text = GameManager.Score.ToString ();


			effectsVolume = effectsVolumeSlider.value;
			standardTurretSound.volume = standardTurretVolume * effectsVolume;
			shotgunTurretSound.volume = shotgunTurretVolume * effectsVolume;
			heavyTurretSound.volume = heavyTurretVolume * effectsVolume;
			floaterSound.volume = floaterVolume * effectsVolume;
			moverSound.volume = moverVolume * effectsVolume;
			travellerSound.volume = travellerVolume * effectsVolume;

			music.volume = musicVolume.value;


	        if (PowerUp.powerUpReady == true)
	        {
	            if (canCall == true)
	            {
	                canCall = false;
	                StartCoroutine(FlashBar());
	            }
	        }

			if (Input.GetKeyDown(KeyCode.Escape) && canUnPause == true)
			{
				ToggleObject(pauseMenu);
				Gun.isPaused = !Gun.isPaused;


				if (Time.timeScale > 0)
				{
					time = Time.timeScale; //stores it because if the power up is activated then you don't want to reset it back to 1 when unpaused.
					Time.timeScale = 0;
				}

				else
				{
					Time.timeScale = time;
				}
			}
		}

		catch 
		{
			
		}
    }

    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
		PlayerPrefs.SetFloat ("MS Vol", musicVolume.value);
		PlayerPrefs.SetFloat ("FX Vol", effectsVolume);
    }

    public void ToggleObject(GameObject target)
    {
		Debug.Log ("Toggled");
		target.SetActive(!target.activeSelf);
    }

	public void UnPauseCancel()
	{
		Debug.Log ("unpausecancel");
		canUnPause = !canUnPause;
	}



    public void ExitGame()
    {
		PlayerPrefs.SetFloat ("MS Vol", musicVolume.value);
		PlayerPrefs.SetFloat ("FX Vol", effectsVolume);
		Application.Quit();
    }
		

    public IEnumerator FlashBar ()
    {
        while (PowerUp.powerUpReady == true)
        {
            fill.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.5f);
            fill.color = new Color32(78, 180, 141, 255);
            yield return new WaitForSeconds(0.5f);
        }

        fill.color = new Color32(78, 180, 141, 255);
        canCall = true;
    }
}
