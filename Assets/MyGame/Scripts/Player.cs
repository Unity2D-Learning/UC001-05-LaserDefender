using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const string AXISHORIZONTAL = "Horizontal";
    private const string AXISVERTICAL   = "Vertical";
    private const string FIRE1 = "Fire1";

    //config parameters
    [Header("Player")]
    public float moveSpeed = 10f;
    public float padding = 1f;
    public int health = 200;


    [Header("Projectile")]
    public GameObject laserPlayer;
    public float projectileSpeed = 10f;
    public float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin,yMin;
    float xMax,yMax;

    // Use this for initialization
    void Start ()
    {
        SetUpMoveBounderies();
    }

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown(FIRE1))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp(FIRE1))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
            laserPlayer,
            transform.position,
            Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis(AXISHORIZONTAL) * Time.deltaTime * moveSpeed; //frame rate independent
        var deltaY = Input.GetAxis(AXISVERTICAL) * Time.deltaTime * moveSpeed; //frame rate independent

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin,xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin,yMax);

        transform.position = new Vector2(newXPos, newYPos);
       
    }

    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }


}
