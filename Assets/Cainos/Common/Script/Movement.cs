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

    public float moveSpeed = 5f; // ��������ƶ��ٶ�
    public float jumpForce = 10f; // ��Ծ����
    public LayerMask groundLayer; // �������ͼ��
    public Transform groundCheck; // �������λ��
    public float groundCheckRadius = 0.2f; // ������ķ�Χ

    private Rigidbody2D rb;
    private float dirx;
    private bool isGrounded;

    void Start()
    {
        // ��ȡ�������
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   

    }

    void Update()
    {
        UpdateAnimationState();
        // ��ȡ��ҵ�����
        float moveInput = Input.GetAxis("Horizontal");
        dirx = Input.GetAxis("Horizontal");

        // �ƶ����
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ����Ƿ��ڵ�����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ��Ծ����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //Ӯ����Ϸ
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
        // ���Ƶ�����ķ�Χ���������
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
