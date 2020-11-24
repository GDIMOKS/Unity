using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motions : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 10f;

    private bool _isGrounded;
    private Rigidbody player;
    private Animator player_anim;
    private float time;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        player_anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        Idle();
        CharacterMotions();
        Jump();
    }

    void Idle()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Jump") == 0 && time >= 2f)
        {
            GetComponent<Animator>().SetBool("Stay", true);
            GetComponent<Animator>().SetBool("Move", false);
            GetComponent<Animator>().SetBool("Jump", false);
        }
        else 
        {
            GetComponent<Animator>().SetBool("Stay", false);
            //time = 0f;
        }   
    }


    void CharacterMotions()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        player.AddForce(movement * Speed);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            GetComponent<Animator>().SetBool("Move", true);
            GetComponent<Animator>().SetBool("Stay", false);
            time = 0f;
        }
        else
            GetComponent<Animator>().SetBool("Move", false);
    }    

    void Jump()
    {
        //float moveUp = Input.GetAxis("Jump");
        //if (moveUp != 0)
        //{
            
        //}
        if (Input.GetAxis("Jump") > 0)
        {
            GetComponent<Animator>().SetBool("Jump", true);
            //GetComponent<Animator>().SetBool("Stay", false);
            time = 0f;
            if (_isGrounded)
                player.AddForce(Vector3.up * JumpForce);

        }
        else
            GetComponent<Animator>().SetBool("Jump", false);

    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}

