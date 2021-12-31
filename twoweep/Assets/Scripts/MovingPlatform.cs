using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{ 
    public bool isH = true;

    public float stepsX1, stepsX2, stepsY1, stepsY2;
    private float moveSpeed = 2f;
    private bool isRight = true;
    private bool isUp = true;
    
    void Update()
    {
        if (isH)
            MoveHorizontal();
        else
            MoveVertical();
    }
    private void MoveHorizontal()
    {
        if (transform.localPosition.x > stepsX1) //8
            isRight = false;
        if (transform.localPosition.x < stepsX2) //4
            isRight = true;
        if (isRight)
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y,0);
        else
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y,0);
    }

    private void MoveVertical()
    {
        if (transform.localPosition.y> stepsY1) //5
            isUp = false;
        if (transform.localPosition.y < stepsY2) //1
            isUp = true;

        if (isUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime,0);
        }        
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime,0);
    } 
}
