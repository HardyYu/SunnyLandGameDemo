using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ealge : Enemy
{

    private Collider2D coll;
    private Rigidbody2D rb;

    public Transform top, bottom;

    public float speed;

    public float topy, bottomy;

    private bool FlyDown = true;


    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        topy = top.position.y;
        bottomy = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(FlyDown){
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if(transform.position.y < bottomy){
                FlyDown = false;
            }  
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topy)
            {
                FlyDown = true;
            }
        }
        
        
    }
}
