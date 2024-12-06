using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // Property usage example
    public SpriteRenderer sprite;
    public Animator anim;
    public Collider2D coll;
    public Collider2D flag;
    public Collider2D bg;

    public float moveSpeed = 5f; // 玩家左右移动速度
    public float jumpForce = 10f; // 跳跃力度
    public LayerMask groundLayer; // 检测地面的图层
    public Transform groundCheck; // 检测地面的位置
    public float groundCheckRadius = 0.2f; // 地面检测的范围

    private Rigidbody2D rb;
    private float dirx;
    private bool isGrounded;

    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   

    }

    void Update()
    {
        UpdateAnimationState();
        // 获取玩家的输入
        float moveInput = Input.GetAxis("Horizontal");
        dirx = Input.GetAxis("Horizontal");

        // 移动玩家
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 检测是否在地面上
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 跳跃输入
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //赢得游戏
        if (coll.IsTouching(flag)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (coll.IsTouching(bg))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        Restart(); 
    }

    void Restart()
    {//use to restart the game and set the player position 
        if (Input.GetKeyDown("r"))
        {
            rb.position = new Vector2(-12.67f, -1.06f);
        }
    }
    void OnDrawGizmosSelected()
    {
        // 绘制地面检测的范围，方便调试
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void UpdateAnimationState()
    {
        if (dirx > 0f)
        {
            anim.SetBool("walking",true);
            sprite.flipX = false;
        }else if (dirx < 0f)
        {
            anim.SetBool("walking", true);
            sprite.flipX = true;
        } else
        {
            anim.SetBool("walking", false);
        }
    }
}
