using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;

    public AudioSource jumpAudio, hurtAudio, cherryAudio;

    public float speed, jumpforce; 

    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    public int Cherry;

    public Text CherryNum;
    private bool isHurt;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if(!isHurt)
        {
            Movement();
        } 

        Jump();
        
        SwitchAnim();
    }

    private void Update()
    {
        //
        if(Input.GetButtonDown("Jump") && jumpCount > 0 )
        {
            jumpPressed = true;
        }
        
        CherryNum.text = Cherry.ToString();
    }

    void Movement()
    {
        //character move
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        
        if(horizontalMove != 0)
        {
            transform.localScale = new Vector3 (horizontalMove, 1, 1);
        }

    }

    void SwitchAnim()
    {
        //run
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if(isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (! isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        } 
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }

        if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x) <  0.1f ){
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                isHurt = false;
            }
        }

    }

    
    
    private void OnTriggerEnter2D(Collider2D collision) {

        //Collection Function
        if(collision.tag == "Collection")
        {
            cherryAudio.Play();
            collision.GetComponent<Animator>().Play("Get Cherry");
            //CherryNum.text = Cherry.ToString();
        }

        //trigger deadline
        if(collision.tag == "Deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    //Destroy Enemy
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if(anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            } else if (transform.position.x < collision.gameObject.transform.position.x)//hurt
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
                hurtAudio.Play();
            } else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
                hurtAudio.Play();
            }
        }
        
    }

    void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {

        if(isGround)
        {
            jumpCount = 2;
            isJump = false;
        }

        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount --;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount --;
            jumpPressed = false;
        }
        
    }

    public void CherryCount()
    {
        Cherry += 1;
    }

}
