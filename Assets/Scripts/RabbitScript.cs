using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour {

    Rigidbody2D m_body;
    PolygonCollider2D m_colider;
    public float m_jumpForce = 5.0f;
    public float m_timeBetweenJumps = 1.0f;
    public float m_movSpeed = 5.0f;

    float m_lastJump;
    bool m_isJumping;
    bool m_facingRight;

    // Use this for initialization
    void Start () {
        m_body = this.GetComponent<Rigidbody2D>();
        m_colider = this.GetComponent<PolygonCollider2D>();
        m_lastJump = Time.realtimeSinceStartup;
        m_facingRight = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump") && !m_isJumping)
        {
            m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
            m_isJumping = true;
            m_lastJump = Time.realtimeSinceStartup;
        }

        if (m_isJumping && ((Time.realtimeSinceStartup - m_lastJump) > m_timeBetweenJumps))
        {
            m_isJumping = false;
        }
        var axisValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal") && !m_isJumping)
        {
            m_body.velocity = new Vector2(axisValue * m_movSpeed, 0.0f);
        }

        if (Input.GetButtonDown("Horizontal") && m_isJumping)
        {
            m_body.AddForce(new Vector2(axisValue * m_movSpeed, 0.0f), ForceMode2D.Impulse);
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
}
