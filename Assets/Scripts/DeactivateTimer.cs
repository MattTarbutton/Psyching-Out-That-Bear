using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTimer : MonoBehaviour
{

    public float lifetime;
    private float currentLifetime;

    // Use this for initialization
    void Start ()
    {
        currentLifetime = lifetime;
    }

    private void OnEnable()
    {
        currentLifetime = lifetime;
    }

    // Update is called once per frame
    void Update ()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
