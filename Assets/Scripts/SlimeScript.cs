using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeScript : MonoBehaviour
{

    GameObject player;
    GameObject canvas;
    CanvasScript canvasScript;
    float scale;
    Vector3 spawn;
    Rigidbody rb;

    public GameObject slimeDeath;

    bool grounded = true;

    public float movementForce = 5;
    public float jumpForce = 5;
    public float rotateSpeed = 1;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvasScript = canvas.GetComponent<CanvasScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        scale = Random.Range(.5f, 3f);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        spawn.x = Random.Range(-24, 24);
        spawn.y = 2;
        spawn.z = Random.Range(-24, 24);
        if(Vector3.Distance(spawn, player.transform.position)  > 5)
        {
            spawn.x = Random.Range(-24, 24);
            spawn.z = Random.Range(-24, 24);
        }
        gameObject.transform.localPosition = new Vector3(spawn.x, spawn.y, spawn.z);
        
    }

    void FixedUpdate()
    {
        if(isGrounded())
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            float singleStep = rotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            InvokeRepeating("Jump", 1, 5);
        }
    }

    void Jump()
    {
        if(grounded)
        {
            rb.AddForce(transform.forward * movementForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        if (rb.velocity.y == 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Equals("Bullet(Clone)"))
        {
            Instantiate(slimeDeath, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(gameObject, 0);
            canvasScript.IncreaseScore(100);
        }
    }

}
