﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public float decelerationSpeed = 0.5f;
	
	
	private bool grounded = false;
	//private Animator anim;
	private Rigidbody2D rb2d;
	
	
	// Use this for initialization
	void Awake ()
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		
		if (h * rb2d.velocity.x < maxSpeed) {
				rb2d.AddForce (Vector2.right * h * moveForce);
		}
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (grounded) {
			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();
		}
		
		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		if(h == 0 && grounded){
			rb2d.velocity = rb2d.velocity * decelerationSpeed;
		}
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void Hide()
	{
		Destroy(gameObject);
	}
}