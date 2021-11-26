using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private float speed = 10f;
    private float jump = 10f;
    private float movementX = 1f;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string Run_ANIMATION = "Run";
    private bool isGrounded = true;

    public event EventHandler OnDied;


    // Start is called before the first frame update
    public static Player GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        Dead();
    }
    void PlayerMoveKeyboard() {

        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * speed * Time.deltaTime;

    }

    void AnimatePlayer() {

        // we are going to the right side
        // we are going to the right side
        if (movementX > 0)
        {
            anim.SetBool(Run_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            // we are going to the left side
            anim.SetBool(Run_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(Run_ANIMATION, false);
        }

    }
    void PlayerJump() {

        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
    }
    public void Dead()
    {
        if(transform.position.y <= -10f)
        {
            myBody.bodyType = RigidbodyType2D.Static;
            if (OnDied != null) OnDied(this, EventArgs.Empty);
        }
    }

    
   
}
