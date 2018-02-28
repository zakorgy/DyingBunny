using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour {

    Animator m_animator;
    Rigidbody2D m_body;
    PolygonCollider2D m_colider;
    public GameObject blood;
    public GameObject spawnPoint;
    public float m_jumpForce = 5.0f;
    public float m_timeBetweenJumps = 1.0f;
    public float m_movSpeed = 5.0f;

    float m_lastJump;
    bool m_isJumping;
    bool m_isDoubleJumping;
    bool m_facingRight;
    bool m_isAlive;

    // Use this for initialization
    void Start () {
        m_animator = this.GetComponent<Animator>();
        m_body = this.GetComponent<Rigidbody2D>();
        m_colider = this.GetComponent<PolygonCollider2D>();
        m_lastJump = Time.realtimeSinceStartup;
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
        if (Input.GetButtonDown("Jump") && !m_isDoubleJumping)
        {
            m_animator.SetBool("jumping", true);
            m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
            if (m_isJumping)
            {
                m_isDoubleJumping = true;
            } else
            {
                m_isJumping = true;
            }
            m_lastJump = Time.realtimeSinceStartup;
        }

        if (m_isDoubleJumping && ((Time.realtimeSinceStartup - m_lastJump) > 2 * m_timeBetweenJumps)) {
            m_animator.SetBool("jumping", false);
            m_isDoubleJumping = false;
        }

        if (m_isJumping && ((Time.realtimeSinceStartup - m_lastJump) > m_timeBetweenJumps))
        {
            m_animator.SetBool("jumping", false);
            m_isJumping = false;
        }
        var axisValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal") && !m_isJumping && !m_isDoubleJumping)
        {
            m_body.velocity = new Vector2(axisValue * m_movSpeed, 0.0f);
        }

        if (Input.GetButtonDown("Horizontal") && (m_isJumping || m_isDoubleJumping))
        {
            m_body.AddForce(new Vector2(axisValue * m_movSpeed / 2, 0.0f), ForceMode2D.Impulse);
        }

        if ((axisValue > 0) && !m_facingRight)
        {
            m_facingRight = true;
            Turn();

        }
        if ((axisValue < 0) && m_facingRight)
        {
            m_facingRight = false;
            Turn();
        }
    }

    void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spike")
        {
            m_isAlive = false;
            PlayDeathAnim();
            ResPawn();
        }

        m_animator.SetBool("jumping", false);
        m_isDoubleJumping = false;
        m_isJumping = false;
    }

    void PlayDeathAnim()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    void ResPawn()
    {
        transform.position = spawnPoint.transform.position;
        m_isAlive = true;
        m_body.velocity = new Vector2(0.0f, 0.0f);
    }
}
