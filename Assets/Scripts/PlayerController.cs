using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float moveSpeed = 300; // didn't update in unity? had to manually do it?
    private Vector2 input; // simple structure rep 2d pos (x, y) and vectors

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // For physics
    void FixedUpdate()
    {
        // if (input.x != 0) input.y = 0; only if we wish to prevent diagonals
        rb.velocity = new Vector2(input.x * moveSpeed * Time.deltaTime, input.y * moveSpeed * Time.deltaTime);

        if (rb.velocity != Vector2.zero)
        {
            animator.SetFloat("moveX", rb.velocity.x);
            animator.SetFloat("moveY", rb.velocity.y);
            // with more animations refer to https://youtu.be/12AOiObT_zg?si=rXRWbN9jp4VS_h7b&t=3838
        }
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");   
        input.y = Input.GetAxisRaw("Vertical");   
    }
}
