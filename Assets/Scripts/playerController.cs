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
    public GameObject vendingObject;

    public GameObject vendingTextbox;

    public float water = 100;
    public float money = 0;

    public float[] cropsharvested;
    public float[] cropPrices;
    public float[] seeds;

    public int CurrentSeed;

    public Text waterAmount;
    public float waterMax;
    public GameObject upgrade;
    public Text moneyAmount;
    public Text CornAmount;
    public Text PotatoeAmount;
    public Text CarrotAmount;

    public Text currentSeedtext;
    public Text currentSeedRemainingtext;


    public Animator controller;
    public Animator duckController;
    float timing;
    public AudioSource farmingSounds;
    public AudioClip[] farmClips;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waterAmount.text = "Water: 10";
        CornAmount.text = "0";
        moneyAmount.text = "Money: $0";
        currentSeedtext.text = "Current Seed: Corn Seed";
        currentSeedRemainingtext.text =  "Corn Seed: " +  seeds[0];
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVals = new Vector2(horizontal, vertical).normalized;


        if(moveVals.x > 0)
        {
            controller.SetInteger("Direction", 4);
        }else if (moveVals.x < 0)
        {
            controller.SetInteger("Direction", 2);
        }
        else if (moveVals.y > 0)
        {
            controller.SetInteger("Direction", 3);
        }
        else if (moveVals.y < 0)
        {
            controller.SetInteger("Direction", 1);
        }
        else
        {
            controller.SetInteger("Direction", 0);
        }

        Interactions();
        CornAmount.text = cropsharvested[0].ToString();
        PotatoeAmount.text = cropsharvested[1].ToString();
        CarrotAmount.text = cropsharvested[2].ToString();

        if (Input.GetKeyDown("q"))
        {
            CurrentSeed++;
            if(CurrentSeed > seeds.Length - 1)
            {
                CurrentSeed = 0;
            }
        }

        seedNames();

        timing += Time.deltaTime;
        if (timing > 40)
        {
            duckController.SetInteger("Time", 21);
        }
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
        }else if (col.gameObject.tag == "Vending Machine")
        {
            vendingObject = col.gameObject;
            vendingTextbox.SetActive(true);
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
        else if (col.gameObject.tag == "Vending Machine")
        {
            vendingObject = null;
            vendingTextbox.SetActive(false);
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
                    farmingSounds.clip = farmClips[1];
                    farmingSounds.Play();
                    water--;
                    waterAmount.text = "Water: " + water;
                    interactable.GetComponent<cropScript>().isSick = false;
                    controller.SetInteger("Direction", 5);
                }
            }


            //if the plant cant be harvested
            if (interactable.GetComponent<cropScript>().isGrown)
            {
                if (Input.GetKeyDown("f"))
                {
                    farmingSounds.clip = farmClips[0];
                    farmingSounds.Play();
                    string nameCheck = interactable.GetComponent<cropScript>().cropName;
                    switch (nameCheck)
                    {
                        case "Corn":
                            controller.SetInteger("Direction", 6);
                            cropsharvested[0] += Random.Range(interactable.GetComponent<cropScript>().harvestMin, interactable.GetComponent<cropScript>().harvestMax);
                            Destroy(interactable);
                            if(cropsharvested[0] > 75)
                            {
                                cropsharvested[0] = 75;
                            }
                            interactable = null;
                            break;
                        case "Potato":
                            controller.SetInteger("Direction", 6);
                            cropsharvested[1] += Random.Range(interactable.GetComponent<cropScript>().harvestMin, interactable.GetComponent<cropScript>().harvestMax);
                            Destroy(interactable);
                            if (cropsharvested[1] > 75)
                            {
                                cropsharvested[1] = 75;
                            }
                            interactable = null;
                            break;
                        case "Carrot":
                            controller.SetInteger("Direction", 6);
                            cropsharvested[2] += Random.Range(interactable.GetComponent<cropScript>().harvestMin, interactable.GetComponent<cropScript>().harvestMax);
                            Destroy(interactable);
                            if (cropsharvested[2] > 75)
                            {
                                cropsharvested[2] = 75;
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
            if (Input.GetKeyDown("f") && water < waterMax)
            {
                farmingSounds.clip = farmClips[1];
                farmingSounds.Play();
                water += 10;
                if(water > waterMax)
                {
                    water = waterMax;
                }
                controller.SetInteger("Direction", 5);
                waterAmount.text = "Water: " + water;
            }
        }

        if (fertileObject != null)
        {   
            //add check for seeds
            if (Input.GetKeyDown("f"))
            {
                if(seeds[CurrentSeed] > 0)
                {
                    farmingSounds.clip = farmClips[2];
                    farmingSounds.Play();
                    fertileObject.GetComponent<FertileGround>().planetd = true;
                    fertileObject.GetComponent<FertileGround>().SeedNum = CurrentSeed;
                    fertileObject = null;
                    seeds[CurrentSeed]--;
                }
                else
                {
                    Debug.Log("No seeds");
                }
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

                moneyAmount.text = "Money: $" + ((Mathf.Round(money * 10)) / 10);
            }
        }

        if (vendingObject != null)
        {
            //add check for seeds
            if (Input.GetKeyDown("1"))
            {
                if(money >= 60)
                {
                    money -= 60;
                    seeds[0] += 10;
                }

                moneyAmount.text = "Money: $" + ((Mathf.Round(money * 10)) / 10);
            }
            else if (Input.GetKeyDown("2"))
            {
                if (money >= 45)
                {
                    money -= 45;
                    seeds[1] += 10;
                }
                
                moneyAmount.text = "Money: $" + ((Mathf.Round(money * 10))/10);
            }else if (Input.GetKeyDown("3"))
            {
                if (money >= 75)
                {
                    money -= 75;
                    seeds[2] += 10;
                }

                moneyAmount.text = "Money: $" + ((Mathf.Round(money * 10)) / 10);
            }
            else if (Input.GetKeyDown("4") && upgrade.activeSelf)
            {
                if (money >= 250)
                {
                    upgrade.SetActive(false);
                    money -= 250;
                    waterMax += 10;
                }

                moneyAmount.text = "Money: $" + ((Mathf.Round(money * 10)) / 10);
            }
        }
    }

    void seedNames()
    {
        switch (CurrentSeed)
        {
            case 0:
                currentSeedtext.text = "Current Seed: Corn Seed";
                currentSeedRemainingtext.text = "Corn Seeds: " + seeds[CurrentSeed];
                break;
            case 1:
                currentSeedtext.text = "Current Seed: Potato Seed";
                currentSeedRemainingtext.text = "Potato Seeds: " + seeds[CurrentSeed];
                break;
            case 2:
                currentSeedtext.text = "Current Seed: Carrot Seed";
                currentSeedRemainingtext.text = "Carrot Seeds: " + seeds[CurrentSeed];
                break;
            default:
                Debug.Log("Incorrect crop");
                break;

        }
    }

}
