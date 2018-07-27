using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyFirstScript : MonoBehaviour
{
    private float positionX;
    private bool isGoingRight;
    private Transform myTransform;

   
	void Start ()
	{
	    myTransform = GetComponent<Transform>();
	}
	
	
	void Update ()
	{
	    if (isGoingRight)
	    {
	        positionX += 0.1f;
	    }
	    else
	    {
	        positionX -= 0.1f;
	    }

	    if (myTransform.position.x < -10)
	    {
	        isGoingRight = true;
	    }

        if (myTransform.position.x > 10)
        {
            isGoingRight = false;
        }

        myTransform.position = Vector3.right * positionX;
    }

   
}
