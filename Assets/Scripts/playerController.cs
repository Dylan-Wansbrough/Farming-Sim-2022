using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb = null;
    Vector3 moveVals = Vector3.zero;

    float timer;

    [SerializeField] float speed = 1f;

    //interacting with object
    public GameObject interactable;
    public GameObject waterObject;

    public float water = 100;

    public float[] cropsharvested;
    public GameObject[] seeds;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVals = new Vector2(horizontal, vertical).normalized;


        Interactions();
        
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
            }
        }
    }

}
