using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class GenerateBackgroundScript : MonoBehaviour {
    public GameObject m_bgTile;
    public SpriteRenderer m_Tile;
    public Transform m_bgStart;
    public int m_row = 10;
    public int m_column = 10;

    private float m_spriteWidth;
    private float m_spriteHeight;

	// Use this for initialization
	void Start () {
        m_spriteWidth = m_Tile.sprite.bounds.size.x * 8;
        m_spriteHeight = m_Tile.sprite.bounds.size.y * 4;

        for (int r = 0; r < m_row; ++r)
        {
            for (int c = 0; c < m_column; ++c)
            {
                Vector3 newPosition = new Vector3(m_bgStart.position.x + r * m_spriteWidth, m_bgStart.position.y - c * m_spriteHeight, m_bgTile.gameObject.transform.position.z);
                var newBgTile = Instantiate(m_bgTile, newPosition, m_bgTile.transform.rotation);
                newBgTile.gameObject.SetActive(true);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
