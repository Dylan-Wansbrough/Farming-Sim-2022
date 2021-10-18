using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cropScript : MonoBehaviour
{   

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stageChanges();
        Debug.Log("Sick: " + isSick);
    }


    void stageChanges()
    {
        currentStageTime += Time.deltaTime;

        if (currentStageTime < stageTime)
        {
            //stage 1 seed
            Debug.Log("Stage 1 Seed");
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
            isGrown = true;
            //stage 5 seed
            Debug.Log("Stage 5 Seed");
        }
    }
}
