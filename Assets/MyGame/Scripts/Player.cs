using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const string AXISHORIZONTAL = "Horizontal";
    private const string AXISVERTICAL   = "Vertical";
    public float moveSpeed = 10f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        var deltaX = Input.GetAxis(AXISHORIZONTAL) * Time.deltaTime * moveSpeed; //frame rate independent
        var newXPos = transform.position.x + deltaX;

        var deltaY = Input.GetAxis(AXISVERTICAL) * Time.deltaTime * moveSpeed; //frame rate independent
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
       
    }
}
