using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    public float playerSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength = 2; 
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnPoint;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(rb.velocity.x != 0)
        {
            anim.SetBool(name: "isMoving", true);           
        }
        else
        {
            anim.SetBool(name: "isMoving", false);
        }
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal>0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.Raycast(origin: (Vector2)transform.position, direction: Vector2.down, raycastLength, (int)groundLayerMask);
        Debug.DrawRay(start: transform.position, Vector3.down * raycastLength, Color.blue);

        anim.SetBool(name: "isGrounded", isGrounded);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Respawn")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}