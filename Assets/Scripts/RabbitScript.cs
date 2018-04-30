using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour {

    Animator m_animator;
    Rigidbody2D m_body;
    CapsuleCollider2D m_colider;
    public GameObject m_blood;
    public GameObject m_burn;
    public GameObject m_spawnPoint;
    public LayerMask m_lm;
    public Transform m_foot;
    public float m_jumpForce = 5.0f;
    public float m_maxSpeed = 1.0f;
    public float m_footRadius = 1.0f;
    public float m_runMultiplier = 2.5f;
    public int m_blodPoolSize = 20;
    public int m_burnPoolSize = 5;

    bool m_facingRight;
    bool m_isAlive;
    private List<GameObject> m_bloodPool = new List<GameObject>();
    private List<GameObject> m_burnPool = new List<GameObject>();
    private Vector3 m_levelStartPos;

    // Use this for initialization
    void Start () {
        m_animator = this.GetComponent<Animator>();
        m_body = this.GetComponent<Rigidbody2D>();
        m_colider = this.GetComponent<CapsuleCollider2D>();
        m_facingRight = true;
        m_isAlive = true;
        m_levelStartPos = transform.position;

        for (int i = 0; i < m_blodPoolSize; ++i)
        {
            GameObject bloodClone = Instantiate(m_blood, transform.position, Quaternion.identity);
            bloodClone.SetActive(false);
            m_bloodPool.Add(bloodClone);
        }

        for (int i = 0; i < m_burnPoolSize; ++i)
        {
            GameObject burnClone = Instantiate(m_burn, transform.position, Quaternion.identity);
            burnClone.SetActive(false);
            m_burnPool.Add(burnClone);
        }
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

    public bool IsAlive()
    {
        return m_isAlive;
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
                PlayJumpSound();
            }
            else if (!isDoubleJumping)
            {
                m_body.velocity = new Vector2(m_body.velocity.x, 0.0f);
                m_body.AddForce(new Vector2(0.0f, m_jumpForce), ForceMode2D.Impulse);
                m_animator.SetBool("DoubleJumping", true);
                PlayJumpSound();
            }
        }

        if (Physics2D.OverlapCircle(m_foot.position, m_footRadius, m_lm) == null)
        {

            m_animator.SetBool("Jumping", true);
        }
        else
        {
            m_animator.SetBool("Jumping", false);
            m_animator.SetBool("DoubleJumping", false);
        }

        if (Input.GetButton("Fire3") && !isJumping)
        {
            m_body.velocity = new Vector2(movSpeed * m_maxSpeed * m_runMultiplier, 0.0f);
            m_animator.SetBool("Running", true);
            PlayRunSound();
        }
        else
        {
            m_animator.SetBool("Running", false);
        }

        if (isJumping)
        {
            HandleSideCollision();
        }
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
            PlayDeathAnimSpike();
            PlayDeathSoundSplat();
            this.gameObject.SetActive(false);
            ResPawn();
        }

        if (col.gameObject.tag == "Lava")
        {
            m_isAlive = false;
            PlayDeathAnimLava();
            PlayDeathSoundBurn();
            this.gameObject.SetActive(false);
            ResPawn();
        }


        if (col.gameObject.tag == "Exit")
        {
            ResPawn();
            this.transform.position = m_levelStartPos;
        }

    }

    void PlayDeathAnimSpike()
    {
        foreach (var blood in m_bloodPool)
        {
            if (!blood.activeSelf)
            {
                blood.SetActive(true);
                blood.transform.position = this.transform.position;
                break;
            }
        }
    }

    void PlayDeathAnimLava()
    {
        foreach (var burn in m_burnPool)
        {
            if (!burn.activeSelf)
            {
                burn.SetActive(true);
                burn.transform.position = this.transform.position;
                break;
            }
        }
    }

    void PlayDeathSoundSplat()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("Splat" + Random.Range(1, 5));
    }

    void PlayDeathSoundBurn()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("Burn");
    }

    void PlayJumpSound()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("Jump");
    }

    void PlayRunSound()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("Run");
    }

    void ResPawn()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnControllerScript>().resetGame();
        m_isAlive = true;
        m_body.velocity = new Vector2(0.0f, m_body.velocity.y);
        m_animator.SetBool("Jumping", false);
    }
}
