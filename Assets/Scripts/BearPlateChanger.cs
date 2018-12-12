using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPlateChanger : MonoBehaviour
{
    public GameObject countdownObject;
    private HotDogCountdown countdown;
    private SpriteRenderer sr;
    public Sprite fullPlate;
    public Sprite fiveSixthsPlate;
    public Sprite twoThirdsPlate;
    public Sprite halfPlate;
    public Sprite oneThirdPlate;
    public Sprite oneSixthPlate;
    public Sprite emptyPlate;

    // Use this for initialization
    void Start()
    {
        countdown = countdownObject.GetComponent<HotDogCountdown>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int remainingDogs = countdown.GetBearHotDogsRemaining();
        int startingHotDogs = countdown.startingHotDogs;

        if (remainingDogs <= 0)
            sr.sprite = emptyPlate;
        else if (remainingDogs < startingHotDogs / 6)
            sr.sprite = oneSixthPlate;
        else if (remainingDogs < startingHotDogs / 3)
            sr.sprite = oneThirdPlate;
        else if (remainingDogs < startingHotDogs / 2)
            sr.sprite = halfPlate;
        else if (remainingDogs < 2 * startingHotDogs / 3)
            sr.sprite = twoThirdsPlate;
        else if (remainingDogs < 5 * startingHotDogs / 6)
            sr.sprite = fiveSixthsPlate;
        else
            sr.sprite = fullPlate;
    }
}
