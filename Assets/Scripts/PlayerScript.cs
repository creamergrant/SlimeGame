using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    Vector2 move;
    public float movementSpeed = 2;

    Vector2 turn;
    public float horizontalSens = .1f;
    public float verticalSens = .025f;

    public Camera camera;

    public Transform gunTip;
    public GameObject bulletPrefab;
    public GameObject slimePrefab;
    public GameObject healthBar;
    public ParticleSystem muzzleFlash;

    bool fire = false;
    public float fireRate = 0.5f;
    float lastShot = 0f;

    public float spawnRate = 10f;
    float lastSpawn = 0f;

    [System.Obsolete]
    private void Start()
    {
        SpawnSlime();
        Screen.lockCursor = true;
        horizontalSens = SharedFloatsScript.sens;
        verticalSens = SharedFloatsScript.sens;
        camera.fieldOfView = SharedFloatsScript.fov;
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {
        //add the mouse positions to the adding rotating
        turn.x += Mouse.current.delta.x.ReadValue() * horizontalSens;
        turn.y += Mouse.current.delta.y.ReadValue() * verticalSens;
        //rotate the character
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        //rotate the camera
        camera.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        //move the character based in the direction of the move axis
        transform.localPosition += transform.forward * move.y * movementSpeed * Time.fixedDeltaTime;
        transform.localPosition += transform.right * move.x * movementSpeed * Time.fixedDeltaTime;

        if (fire)
        {
            SpawnBullet();
        }

        SpawnSlime();
    }

    void SpawnBullet()
    {
        if(Time.time > fireRate + lastShot)
        {
            Instantiate(bulletPrefab, gunTip.transform.position, Quaternion.identity);
            muzzleFlash.Play();
            lastShot = Time.time;
        }
    }

    void SpawnSlime() 
    {
        if(Time.time > spawnRate + lastSpawn)
        {
            Instantiate(slimePrefab);
            lastSpawn = Time.time;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context) 
    {
        if(context.ReadValue<float>() == 1)
        {
            fire = true;
        }
        else
        {
            fire = false;
        }
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Equals("Slime(Clone)"))
        {
            healthBar.GetComponent<CanvasScript>().OnHit();
            Destroy(collision.gameObject);
        }
    }

}
