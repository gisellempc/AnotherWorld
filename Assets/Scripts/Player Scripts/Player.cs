using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{    
   
    private UIManager _uiManager;

    [SerializeField]
    private float moveSpeed = 10f;
    private float movementX;
    private Vector3 tempScale;
    private Animator anim;
    private Rigidbody2D myBody;
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField] 
    private float jumpForce = 10f;
    private bool isGrounded;
    private int _hp = 5;
    private int _score;
    private bool isFlickerEnabled = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {       
        HandlePlayerMovement();
        HandleFacingDirection();
        HandlePlayerAnimation();
        HandleJumping();


            _uiManager.SetScore(_score);
        
       
    }

    void HandlePlayerMovement()
    {
        movementX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        transform.position += moveSpeed * Time.deltaTime * new Vector3(movementX, 0f, 0f);
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

        // detecting collision with the enemies
        if (collision.gameObject.CompareTag(TagManager.ENEMY_TAG))
        {
            ReceiveDmg();
            StartCoroutine(colorFlickerRoutine()); 
        }
        if (collision.gameObject.CompareTag(TagManager.WATER_TAG))
        {
            _hp = 0;
           ReceiveDmg();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;

        if (otherGO.CompareTag("Coin"))
        {
            //_sfxPlayer.PlayOneShot(_gemSound);
            MoneyGain();
            Destroy(otherGO);
        }

        if (otherGO.CompareTag("Flag"))
        {
            _uiManager.ShowWinScreen(_score);
            Time.timeScale = 0;
        }
    }
    private IEnumerator colorFlickerRoutine()
    {
        while (isFlickerEnabled == true)
        {
            m_SpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            m_SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            isFlickerEnabled = false;
        }
    }
    private void ReceiveDmg()
    {
        isFlickerEnabled = true;
        _hp--;
        _uiManager.SetHp(_hp);
        

        if (_hp <= 0)
        {
            Destroy(gameObject);
            _uiManager.ShowLooseScreen(_score);
            Time.timeScale = 0;
            //_sfxPlayer.PlayOneShot(_gameOverSound);
            
        }       

    }

    private void MoneyGain()
    {
        _score++;
    }
}