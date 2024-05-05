using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [Range(1,100)]
    public float jumpVelocity;


    private Rigidbody2D rb2D;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    // Variabler som hanterar hastighet, hur h�gt man kan hoppa, och om man hoppar eller inte:3
    private float moveHorizontal;
    private float moveVertical;
    // kollar spelarens input:3
    public float falljumpMultiplier = 10f;

    GrapplingGun gg;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        gg = GetComponentInChildren<GrapplingGun>();
        

        //h�mtar rigidbodyn fr�n spelarobjektet:3
        moveSpeed = 2f;
        jumpForce = 80f;
        isJumping = false;
        //s�tter ett v�rde p� variablerna:3
        

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        //s�tter variablerna lika med en input som �r antingen h�ger/v�nster, upp/ner:3

        if (rb2D.velocity.y < 0&& gg.isGrapple==false)
        {
            rb2D.velocity += Vector2.up * (falljumpMultiplier - 1) * Time.deltaTime;
        }
        if (gg.isGrapple == true)
        {
            rb2D.drag = 0.1f;
        }
        if (gg.isGrapple == false)
        {
            rb2D.drag = 5;
        }
        if (gg.isGrapple == true)
        {
            moveSpeed = 0;
        }
        if (gg.isGrapple == false)
        {
            moveSpeed = 2;
        }
    }
    private void FixedUpdate()
    {
        


        if (moveHorizontal> 0.1f|| moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal*moveSpeed, 0f),ForceMode2D.Impulse);
        }
        //kollar om inputen �r h�ger eller v�nster och applicerar sen kraft i den riktingingen:3
        //moveHorizontal*moveSpeed tar v�rdet 1 eller -1 (h�ger/v�nster) och g�ngrar det med v�rdet jag satte p� "moveSpeed":3
        if (!isJumping && Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
        // if (!isJumping && moveVertical > 0.1f )
        {
          //  rb2D.AddForce(new Vector2(0f,moveVertical * jumpForce), ForceMode2D.Impulse);
            
            
        }
        
        
        //samma fast med jump och kollar om spelaren hoppar eller inte "isJumping":3
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            //tv� = kollar om det �r lika med ett = s�tter v�rdet lika med
        {
            isJumping = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
        //genom en extra collider s�tt till trigger kollar skriptet om spelaren nuddar marken och s�tter "isJumping" till true i s� fall:3
    }


}
