using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour {

    Animator m_animator;
    Rigidbody2D m_body;
    CapsuleCollider2D m_colider;
    public GameObject blood;
    public GameObject spawnPoint;
    public LayerMask lm;
    public Transform foot;
    public float m_jumpForce = 5.0f;
    public float m_maxSpeed = 1.0f;
    public float m_footRadius = 1.0f;
    public float m_runMultiplier = 2.5f;

    bool m_facingRight;
    bool m_isAlive;

    // Use this for initialization
    void Start () {
        m_animator = this.GetComponent<Animator>();
        m_body = this.GetComponent<Rigidbody2D>();
        m_colider = this.GetComponent<CapsuleCollider2D>();
        m_facingRight = true;
        m_isAlive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isAlive)
        {
            UpdatePlayer();
        }
            else
        {
            m_body.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    void UpdatePlayer()
    {
        float movSpeed = Input.GetAxisRaw("Horizontal");
        bool isJumping = m_animator.GetBool("Jumping");
        bool isDoubleJumping = m_animator.GetBool("DoubleJumping");
        m_body.velocity = new Vector2(movSpeed * m_maxSpeed, m_body.velocity.y);
        m_animator.SetFloat("MovementSpeed", Mathf.Abs(m_body.velocity.x));

        if ((movSpeed > 0) && !m_facingRight)
        {
            m_facingRight = true;
            Turn();

        }
        if ((movSpeed < 0) && m_facingRight)
        {
            m_facingRight = false;
            Turn();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                m_body.velocity = new Vector2(m_body.velocity.x, 0.0f);
                m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
                m_animator.SetBool("Jumping", true);
            }
            else if (!isDoubleJumping)
            {
                m_body.velocity = new Vector2(m_body.velocity.x, 0.0f);
                m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
                m_animator.SetBool("DoubleJumping", true);
            }
        }

        if (Physics2D.OverlapCircle(foot.position, m_footRadius, lm) == null)
        {

            m_animator.SetBool("Jumping", true);
        }
        else
        {
            Debug.Log("jump false overlap");
            m_animator.SetBool("Jumping", false);
            m_animator.SetBool("DoubleJumping", false);
        }

        if (Input.GetButton("Fire3") && !isJumping)
        {
            m_body.velocity = new Vector2(movSpeed * m_maxSpeed * m_runMultiplier, 0.0f);
            m_animator.SetBool("Running", true);
        }
        else
        {
            m_animator.SetBool("Running", false);
        }

        if (isJumping)
        {
            HandleSideCollision();
        }

        /*bool isJumping = m_animator.GetBool("Jumping");
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                m_animator.SetBool("Jumping", true);
                m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
                Debug.Log("Jump started");
            }
        }

        if (Input.GetButton("Horizontal") && !isJumping)
        {
            m_body.velocity = new Vector2(movSpeed * m_movSpeed, 0.0f);

        }

        Debug.Log(Physics.OverlapSphere(feet.transform.localPosition, m_radius, LayerMask.GetMask("StopJump")).Length);
        if  (Physics.OverlapSphere(feet.transform.localPosition, m_radius, LayerMask.GetMask("StopJump")).Length > 0)
        {
            Debug.Log("Jump ended");
            m_animator.SetBool("Jumping", false);
        }*/
    }

    void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HandleSideCollision()
    {
        Vector2 boxVectorStartRight = new Vector2(m_body.position.x + m_colider.size.x / 2 + 0.05f, m_body.position.y);
        Vector2 boxVectorStartLeft = new Vector2(m_body.position.x - m_colider.size.x / 2 - 0.05f, m_body.position.y);
        if (!m_facingRight)
        {
            if (Physics2D.Raycast(boxVectorStartLeft, -Vector2.left, 0.0001f))
            {
                m_body.velocity = new Vector2 (0.0f, m_body.velocity.y);
            }
        }
        else
        {
            if (Physics2D.Raycast(boxVectorStartRight, Vector2.left, 0.0001f))
            {
                m_body.velocity = new Vector2(0.0f, m_body.velocity.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spike")
        {
            m_isAlive = false;
            PlayDeathAnim();
            ResPawn();
            m_animator.SetBool("Jumping", false);
        }

    }

    void PlayDeathAnim()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    void ResPawn()
    {
        transform.position = spawnPoint.transform.position;
        m_isAlive = true;
        m_body.velocity = new Vector2(0.0f, m_body.velocity.y);
        m_animator.SetBool("Jumping", false);
    }
}
