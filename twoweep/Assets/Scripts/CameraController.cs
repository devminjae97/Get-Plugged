using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetY; // camera1 : 0, camera2 : =10

    private float posY;
    private float posZ;

    private bool isTracePlayer;

    static private float speed = 2f;

    void Awake()
    {
        posZ = transform.position.z;
        isTracePlayer = false;
    }
    
    void LateUpdate()
    {
        if(isTracePlayer)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, posY + offsetY, posZ), speed * Time.deltaTime);
    }
    
    public void SetCameraPos(float y) 
    {
        posY = y;
        this.transform.position = new Vector3(target.position.x, posY + offsetY, posZ);
    }
    
    public void SetCameraTrace(bool b) {
        isTracePlayer = b;
    }
}
