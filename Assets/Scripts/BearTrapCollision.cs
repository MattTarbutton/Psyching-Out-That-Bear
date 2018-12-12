using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BearTrapCollision : MonoBehaviour
{
    private Animator animator;
    private bool close;

	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
        close = false;
	}

    private void OnEnable()
    {
        if (animator != null)
        {
            //animator.SetTrigger("BearTrapOpen");
            close = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bear" && animator.GetCurrentAnimatorStateInfo(0).IsName("BearTrapOpen") && !close)
        {
            animator.SetTrigger("BearTrapClose");
            close = true;
        }
    }
}
