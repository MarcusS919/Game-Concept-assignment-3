using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public static float tempSpeed;
    public static float pspeed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;


    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        pspeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed = speed / 2;
        }
        else
        {
            tempSpeed = speed;
        }

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * tempSpeed;

    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            //to kill the player
            //   Destroy(gameObject);
            transform.position = new Vector3(-7, -4, 0);
    }

}