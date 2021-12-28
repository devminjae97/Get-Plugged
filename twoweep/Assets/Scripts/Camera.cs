using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float posY;
    private float posZ;

    static private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;
        posZ = transform.position.z;
    }
    
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, posY, posZ), speed * Time.deltaTime);
    }
}
