using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour {

    public Transform m_startPoint;
    public Transform m_endPoint;
    public float m_rotSpeed = 10.0f;
    public float m_movSpeed = 0.01f;
    Rigidbody2D m_body;
    BoxCollider2D m_colider;
    Vector3 from;
    Vector3 to;
    float steps;
    float moveDir;
    bool vertical;

    // Use this for initialization
    void Start () {
        m_body = this.GetComponent<Rigidbody2D>();
        m_colider = this.GetComponent<BoxCollider2D>();
        from = m_startPoint.position;
        to = m_endPoint.position;
        transform.position = from;
        vertical = from.x == to.x;
        //moveDir = (vertical) ? (to.y > from.y) ? 1.0f : -1.0f : (to.x > from.x) ? 1.0f : -1.0f;
        if (vertical)
        {
            moveDir = (to.y > from.y) ? 1.0f : -1.0f;
        }
        else
        {
            moveDir = (to.x > from.x) ? 1.0f : -1.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (vertical)
        {
            VerticalMovement();
        }
        else
        {
            HorizontalMovement();
        }
        transform.Rotate(0.0f, 0.0f, m_rotSpeed);
	}

    void VerticalMovement()
    {
        if (transform.position.y * moveDir <= to.y * moveDir)
        {
            Vector3 dist = to - from;
            transform.position = transform.position + dist * m_movSpeed;
        }
        else
        {
            SwapValues();
        }
    }

    void HorizontalMovement()
    {
        if (transform.position.x * moveDir <= to.x * moveDir)
        {
            Vector3 dist = to - from;
            transform.position = transform.position + dist * m_movSpeed;
        }
        else
        {
            SwapValues();
        }
    }

    void SwapValues()
    {
        moveDir *= -1.0f;
        Vector3 tmp = from;
        from = to;
        to = tmp;
    }
}
