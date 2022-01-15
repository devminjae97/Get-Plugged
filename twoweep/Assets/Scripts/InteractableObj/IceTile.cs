using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
    [SerializeField] private int iceTileNum;
    public GameObject iceTileObj;

    private void Awake()
    {
        for (int i = 0; i < iceTileNum; i++)
        {
            GameObject go = Instantiate(iceTileObj, new Vector3(i, 0, -1), transform.rotation);
            go.transform.SetParent(gameObject.transform, false);
        }
    }
}
