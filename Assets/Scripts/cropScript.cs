using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cropScript : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    public string cropName;
    public int harvestMin;
    public int harvestMax;


    //stages
    public int currentStage = 0;
    public float stageTime;

    public bool isSick;
    public bool isDead;
    public bool isGrown;

    public float currentStageTime;

    public int sicknessChance;

    public bool stage2;
    public bool stage3;
    public bool stage4;

    public Sprite[] sprites;
    public Color[] colours;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        stageChanges();
        if (isSick)
        {
            m_SpriteRenderer.color = colours[1];
        }
        else
        {
            m_SpriteRenderer.color = colours[0];
        }
    }


    void stageChanges()
    {
        if (!isGrown)
        {
            currentStageTime += Time.deltaTime;
        }

        if (currentStageTime < stageTime)
        {
            //stage 1 seed
            Debug.Log("Stage 1 Seed");
            m_SpriteRenderer.sprite = sprites[0];
        }
        else if (currentStageTime < (stageTime * 2))
        {

            //stage 2 seed
            Debug.Log("Stage 2 Seed");
            if (!stage2)
            {
                int sick = Random.Range(1, sicknessChance);
                if(sick == 2)
                {
                    isSick = true;
                }
                stage2 = true;
                m_SpriteRenderer.sprite = sprites[1];
            }
            
        }
        else if (currentStageTime < (stageTime * 3))
        {
            if (!stage3)
            {
                if (!isSick)
                {
                    int sick = Random.Range(1, sicknessChance);
                    if (sick == 2)
                    {
                        isSick = true;
                    }
                    stage3 = true;
                    m_SpriteRenderer.sprite = sprites[2];
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
            //stage 3 seed
            Debug.Log("Stage 3 Seed");
        }
        else if (currentStageTime < (stageTime * 4))
        {
            if (!stage4)
            {
                if (!isSick)
                {
                    int sick = Random.Range(1, sicknessChance);
                    if (sick == 2)
                    {
                        isSick = true;
                    }
                    stage4 = true;
                    m_SpriteRenderer.sprite = sprites[3];
                }
                else
                {
                    Destroy(gameObject);
                }

            }
            //stage 4 seed
            Debug.Log("Stage 4 Seed");
        }
        else if (currentStageTime < (stageTime * 5))
        {
            if (isSick)
            {
                Destroy(gameObject);
            }
            m_SpriteRenderer.sprite = sprites[4];
            isGrown = true;
            //stage 5 seed
            Debug.Log("Stage 5 Seed");
        }
    }
}
