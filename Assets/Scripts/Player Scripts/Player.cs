using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private float movementX;
    private Vector3 tempScale;
    private Animator anim;
    private Rigidbody2D myBody;
    [SerializeField] 
    private float jumpForce = 10f;
    private bool isGrounded;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandlePlayerMovement();
        HandleFacingDirection();
        HandlePlayerAnimation();
        HandleJumping();
    }

    void HandlePlayerMovement()
    {
        movementX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        transform.position += new Vector3(movementX, 0f, 0f) * moveSpeed * Time.deltaTime;
    }

    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (movementX > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (movementX < 0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }
    void HandlePlayerAnimation()
    {
        if (movementX != 0)
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, true);
        else
            anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, false);
    }

    void HandleJumping()
    {
        if (Input.GetButtonDown(TagManager.JUMP_BUTTON_ID) && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(TagManager.GROUND_TAG))
            isGrounded = true;

    }
}
