using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb = null;
    Vector3 moveVals = Vector3.zero;

    float timer;

    [SerializeField] float speed = 1f;

    //interacting with object
    public GameObject interactable;
    public GameObject waterObject;
    public GameObject fertileObject;
    public GameObject depositObject;

    public float water = 100;
    public float money = 0;

    public float[] cropsharvested;
    public float[] cropPrices;
    public float[] seeds;

    public Text waterAmount;
    public Text moneyAmount;
    public Text CornAmount;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waterAmount.text = "Water: 10";
        CornAmount.text = "0";
        moneyAmount.text = "Money: $0";
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVals = new Vector2(horizontal, vertical).normalized;


        Interactions();
        CornAmount.text = cropsharvested[0].ToString();
    }

    private void FixedUpdate()
    {
        if (moveVals != Vector3.zero)
        {
            timer = 1;
            rb.velocity = moveVals * speed;
        }
        else
        {
            timer -= Time.deltaTime;
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, timer);

        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Crop")
        {
            interactable = col.gameObject;
        }else if (col.gameObject.tag == "Water")
        {
            waterObject = col.gameObject;
        }else if (col.gameObject.tag == "Fertile ground")
        {
            fertileObject = col.gameObject;
        }else if (col.gameObject.tag == "Deposit")
        {
            depositObject = col.gameObject;
        }
    }

    // called when the cube hits the floor
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Crop")
        {
            interactable = null;
        }else if (col.gameObject.tag == "Water")
        {
            waterObject = null;
        }
        else if (col.gameObject.tag == "Fertile ground")
        {
            fertileObject = null;
        }else if (col.gameObject.tag == "Deposit")
        {
            depositObject = null;
        }
    }

    void Interactions()
    {   

        //interacting with crops
        if (interactable != null)
        {   
            //if the plant is sick
            if (interactable.GetComponent<cropScript>().isSick)
            {
                if (Input.GetKeyDown("f") && water > 0)
                {
                    water--;
                    waterAmount.text = "Water: " + water;
                    interactable.GetComponent<cropScript>().isSick = false;
                }
            }


            //if the plant cant be harvested
            if (interactable.GetComponent<cropScript>().isGrown)
            {
                if (Input.GetKeyDown("f"))
                {
                    string nameCheck = interactable.GetComponent<cropScript>().cropName;
                    switch (nameCheck)
                    {
                        case "Corn":
                            cropsharvested[0] += Random.Range(interactable.GetComponent<cropScript>().harvestMin, interactable.GetComponent<cropScript>().harvestMax);
                            Destroy(interactable);
                            if(cropsharvested[0] > 75)
                            {
                                cropsharvested[0] = 75;
                            }
                            interactable = null;
                            break;
                        default:
                            Debug.Log("Incorrect crop");
                            break;
                    }
                }
            }
        }

        if(waterObject != null)
        {
            if (Input.GetKeyDown("f") && water < 10)
            {
                water += 10;
                if(water > 10)
                {
                    water = 10;
                }
                waterAmount.text = "Water: " + water;
            }
        }

        if (fertileObject != null)
        {   
            //add check for seeds
            if (Input.GetKeyDown("f"))
            {
                fertileObject.GetComponent<FertileGround>().planetd = true;
            }
        }

        if (depositObject != null)
        {
            //add check for seeds
            if (Input.GetKeyDown("f"))
            {
                int i = 0;
                while(i < cropsharvested.Length)
                {
                    money += (cropsharvested[i] * cropPrices[i]);
                    cropsharvested[i] = 0;
                    i++;
                }

                moneyAmount.text = "Money: $" + money;
            }
        }
    }

}
