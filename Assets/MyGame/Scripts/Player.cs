using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const string AXISHORIZONTAL = "Horizontal";
    private const string AXISVERTICAL   = "Vertical";
    private const string FIRE1 = "Fire1";

    //config parameters
    public float moveSpeed = 10f;
    public float padding = 1f;
    public GameObject laserPlayer;
    public float projectileSpeed = 10f;

    float xMin,yMin;
    float xMax,yMax;

    // Use this for initialization
    void Start ()
    {
        SetUpMoveBounderies();
        StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Fire()
    {
        if (Input.GetButtonDown(FIRE1))
        {
            GameObject laser = Instantiate(
                laserPlayer, 
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectileSpeed);
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

    IEnumerator PrintAndWait()
    {
        Debug.Log("First message sent, boss");
        yield return new WaitForSeconds(3);
        Debug.Log("The second messages");
    }
}
