using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const string AXISHORIZONTAL = "Horizontal";
    private const string AXISVERTICAL   = "Vertical";
    public float moveSpeed = 10f;
    public float padding = 1f;

    float xMin,yMin;
    float xMax,yMax;

    // Use this for initialization
    void Start ()
    {
        SetUpMoveBounderies();
    }

    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    private void Move()
    {
        var deltaX = Input.GetAxis(AXISHORIZONTAL) * Time.deltaTime * moveSpeed; //frame rate independent
        var deltaY = Input.GetAxis(AXISVERTICAL) * Time.deltaTime * moveSpeed; //frame rate independent

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin,xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin,yMax);

        transform.position = new Vector2(newXPos, newYPos);
       
    }
}
