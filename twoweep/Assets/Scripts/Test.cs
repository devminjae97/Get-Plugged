using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour 
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetY; // 

    private float posY;
    private float posZ;

    private bool isTracePlayer;

    static private float speed = 2f;

    void Awake() 
    {
        posZ = transform.position.z;
        isTracePlayer = false;
    }

    float tx;
    float ty;

    void LateUpdate() {
        if (isTracePlayer) 
        {
            tx = Mathf.Lerp(transform.position.x, target.position.x, speed * Time.deltaTime);
            ty = Mathf.Lerp(transform.position.y, target.position.y + offsetY, speed * Mathf.Pow(Mathf.Abs(transform.position.y - target.position.y), 2) * Time.deltaTime);

            transform.position = new Vector3(tx, ty, posZ);

            //transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, posY + offsetY, posZ), speed * Time.deltaTime);
        }
    }

    public void SetCameraPos(float y) 
    {
        //posY = y;
        this.transform.position = new Vector3(target.position.x, target.position.y + offsetY, posZ);
    }

    public void SetCameraTrace(bool b) 
    {
        isTracePlayer = b;
    }
}
