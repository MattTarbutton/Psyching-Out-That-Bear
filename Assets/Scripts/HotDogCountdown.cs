using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotDogCountdown : MonoBehaviour
{
    public Text kobayashiHotDogText;
    public Text bearHotDogText;
    private float kobayashiMaxTimeToNextHotdog = 6;
    private float kobayashiMinTimeToNextHotdog = 4;
    private float bearMaxTimeToNextHotdog = 10;
    private float bearMinTimeToNextHotdog = 5;
    private int kobayashiHotDogsPerTimer = 2;
    private int bearMaxHotDogsPerTimer = 10;
    private int bearMinHotDogsPerTimer = 4;
    private float kobayashiTimer;
    private float bearTimer;
    public int startingHotDogs = 50;
    private int kobayashiHotDogs;
    private int kobayashiHotDogsString;
    private int bearHotDogs;
    private int bearHotDogsString;

    public bool gameGoing;

	// Use this for initialization
	void Start ()
    {
        ResetHotDogCount();
        gameGoing = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameGoing)
        {
            kobayashiTimer -= Time.deltaTime;
            if (kobayashiTimer <= 0)
            {
                kobayashiHotDogs -= kobayashiHotDogsPerTimer;
                if (kobayashiHotDogs < 0)
                    kobayashiHotDogs = 0;
                GetNextKobayashiTimer();
                StartCoroutine(UpdateKobayashiHotDogsText());
            }

            bearTimer -= Time.deltaTime;
            //Debug.Log(bearTimer);
            if (bearTimer <= 0)
            {
                bearHotDogs -= GetBearHotDogs();
                if (bearHotDogs < 0)
                    bearHotDogs = 0;
                GetNextBearTimer();
                StartCoroutine(UpdateBearHotDogsText());
            }
        }
        
        if (bearHotDogs < startingHotDogs / 2)
        {
            if (bearHotDogs < startingHotDogs / 5)
            {
                bearHotDogText.rectTransform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time / 1, .3f)) / 1;
                bearHotDogText.color = new Color(1, Mathf.PingPong(Time.time * 2, 1), Mathf.PingPong(Time.time * 2, 1));
            }
            else if (bearHotDogs < startingHotDogs / 4)
            {
                bearHotDogText.rectTransform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time / 3, .25f)) / 1;
                bearHotDogText.color = new Color(1, Mathf.PingPong(Time.time / 1, 1), Mathf.PingPong(Time.time / 1, 1));
            }
            else if (bearHotDogs < startingHotDogs / 3)
            {
                bearHotDogText.rectTransform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time / 5, .2f)) / 1;
            }
            else if (bearHotDogs < startingHotDogs / 2)
            {
                bearHotDogText.rectTransform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time / 7, .15f)) / 1;
            }
        }
        else
        {
            bearHotDogText.rectTransform.localScale = Vector3.one;
            bearHotDogText.color = Color.white;
        }
    }
    public void ResetHotDogCount()
    {
        kobayashiHotDogs = startingHotDogs;
        kobayashiHotDogsString = startingHotDogs;
        kobayashiHotDogText.text = kobayashiHotDogsString.ToString();
        bearHotDogs = startingHotDogs;
        bearHotDogsString = startingHotDogs;
        bearHotDogText.text = bearHotDogsString.ToString();
        GetNextKobayashiTimer();
        GetNextBearTimer();
    }

    private void GetNextKobayashiTimer()
    {
        kobayashiTimer = UnityEngine.Random.Range(kobayashiMinTimeToNextHotdog, kobayashiMaxTimeToNextHotdog);
    }

    private void GetNextBearTimer()
    {
        bearTimer = UnityEngine.Random.Range(bearMinTimeToNextHotdog, bearMaxTimeToNextHotdog);
    }

    private int GetBearHotDogs()
    {
        return UnityEngine.Random.Range(bearMinHotDogsPerTimer, bearMaxHotDogsPerTimer);
    }

    public int GetKobayashiHotDogsRemaining()
    {
        return kobayashiHotDogs;
    }

    public int GetBearHotDogsRemaining()
    {
        return bearHotDogs;
    }

    public void AddToBearTimer(float timeToAdd)
    {
        bearTimer += timeToAdd;
    }

    IEnumerator UpdateKobayashiHotDogsText()
    {
        while (kobayashiHotDogsString != kobayashiHotDogs)
        {
            kobayashiHotDogsString--;
            kobayashiHotDogText.text = kobayashiHotDogsString.ToString();
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator UpdateBearHotDogsText()
    {
        while (bearHotDogsString != bearHotDogs)
        {
            bearHotDogsString--;
            bearHotDogText.text = bearHotDogsString.ToString();
            yield return new WaitForSeconds(.2f);
        }
    }
}
