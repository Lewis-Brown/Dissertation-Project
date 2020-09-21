using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{

    public float speed = 15f;
    public float jumpVelocity = 10f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public GameObject Hook;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float move = Input.GetAxis("Horizontal");
        //move left and right
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        //pressing space to jump
        if (Input.GetKeyDown("space"))
        {

            rb.velocity = Vector2.up * jumpVelocity;
        }

    }
}
