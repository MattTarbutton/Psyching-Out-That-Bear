using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMotion : MonoBehaviour
{
    public float startingXVelocity;
    public float startingYVelocity;
    public float startingAngularVelocity;
    public float lifetime;
    private float currentLifetime;

	// Use this for initialization
	void Start ()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startingXVelocity, startingYVelocity);
        rb.angularVelocity = startingAngularVelocity;
        currentLifetime = lifetime;
	}

    private void OnEnable()
    {
        currentLifetime = lifetime;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startingXVelocity, startingYVelocity);
        rb.angularVelocity = startingAngularVelocity;
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
