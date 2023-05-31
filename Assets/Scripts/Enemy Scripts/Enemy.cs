using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 tempScale;

    private Rigidbody2D myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleFacingDirection();
    }
    void FixedUpdate()
    {       
        myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y) ;     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;

        if (otherGO.tag == "Cactus")
        {
            moveSpeed *= -1;
        }
    }
    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (moveSpeed < 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (moveSpeed > 0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }

   
}
