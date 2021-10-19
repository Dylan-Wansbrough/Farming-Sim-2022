using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileGround : MonoBehaviour
{

    public GameObject[] crops;

    public bool planetd;
    public bool SpawnOnce;

    public GameObject currentlyPlanted;

    public BoxCollider2D collidy;

    // Start is called before the first frame update
    void Start()
    {
        float m_ScaleX = 0.14f;
        float m_ScaleY = 0.14f;
        collidy.size = new Vector2(m_ScaleX, m_ScaleY);
    }

    // Update is called once per frame
    void Update()
    {
        if (planetd)
        {
            if (!SpawnOnce)
            {
                currentlyPlanted = Instantiate(crops[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), Quaternion.identity);
                float m_ScaleX = 0.0f;
                float m_ScaleY = 0.0f;
                collidy.size = new Vector2(m_ScaleX, m_ScaleY);
                SpawnOnce = true;
            }
            
        }

        if(currentlyPlanted == null)
        {
            float m_ScaleX = 0.14f;
            float m_ScaleY = 0.14f;
            collidy.size = new Vector2(m_ScaleX, m_ScaleY);
            planetd = false;
            SpawnOnce = false;
        }
    }
}
