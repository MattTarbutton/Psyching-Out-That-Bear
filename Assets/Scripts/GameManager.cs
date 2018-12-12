using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HotDogCountdown hotDogCountdown;
    public Animator kobayashiAnimator;
    public Animator bearAnimator;
    public Text countdownText;
    public Text gameOverText;
    public Text psychOutText;
    public Text restartText;
    private float countdown;


	// Use this for initialization
	void Start ()
    {
        countdown = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;

            countdownText.rectTransform.localScale = Vector3.one * (1 + (countdown - (int)countdown) * .5f);
            if ((int)countdown == 0)
            {
                countdownText.text = "GO!";
            }
            else
            {
                countdownText.text = ((int)countdown).ToString();
            }

            if (countdown <= 0)
            {
                hotDogCountdown.gameGoing = true;
                kobayashiAnimator.SetBool("Started", true);
                bearAnimator.SetBool("Started", true);
                countdownText.gameObject.SetActive(false);
            }
        }

		if (countdown <= 0 && (hotDogCountdown.GetBearHotDogsRemaining() == 0 || hotDogCountdown.GetKobayashiHotDogsRemaining() == 0))
        {
            hotDogCountdown.gameGoing = false;
            kobayashiAnimator.SetBool("Started", false);
            bearAnimator.SetBool("Started", false);

            gameOverText.gameObject.SetActive(true);
            psychOutText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            if (hotDogCountdown.GetKobayashiHotDogsRemaining() == 0)
            {
                gameOverText.text = "Kobayashi Wins!";
                psychOutText.text = "You psyched out that bear!";
            }
            else
            {
                gameOverText.text = "The Bear Wins!";
                psychOutText.text = "The bear's mental fortitude was too powerful.";
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                countdown = 4;
                hotDogCountdown.ResetHotDogCount();
                countdownText.gameObject.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                psychOutText.gameObject.SetActive(false);
                restartText.gameObject.SetActive(false);
            }
        }
	}
}
