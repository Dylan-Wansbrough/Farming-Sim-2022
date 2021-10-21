﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depositBoxController : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_SpriteRenderer.sprite = sprites[1];
        }
    }

    // called when the cube hits the floor
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_SpriteRenderer.sprite = sprites[0];
        }
    }
}
