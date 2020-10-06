using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    private float runningSpeed = 14f;
    private float jumpForce = 20f;
    public LayerMask groundLayer;
    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Awake()
    {
        sharedInstance = this;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("isGrounded", true);
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
            _animator.SetBool("isGrounded", IsTouchingGround());
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (_rb.velocity.x < runningSpeed)
            {
                _rb.velocity = new Vector2(runningSpeed, _rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if (IsTouchingGround())
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private bool IsTouchingGround()
    {
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 2.2f, groundLayer);
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 2.3f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this._animator.SetBool("Idle", true);
    }
}
