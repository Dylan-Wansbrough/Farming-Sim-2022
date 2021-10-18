using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rb = null;
    Vector3 moveVals = Vector3.zero;

    float timer;

    [SerializeField] float speed = 1f;

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
}
