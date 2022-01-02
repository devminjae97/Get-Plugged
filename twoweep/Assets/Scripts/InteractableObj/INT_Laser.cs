using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INT_Laser : MonoBehaviour
{
    public enum LaserDirection
    {
        Top, Right, Bottom, Left
    }
    public LaserDirection laserDir;
    private LineRenderer lineRenderer;

    public Transform blockingLaserTileParent;
    List<Transform> blockingLaserTile = new List<Transform>();
    private BoxCollider2D bc;
    private Transform blockingLaserTileChildTransform;

    float startPosX, startPosY, endPosX, endPosY;
    private bool isInit;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        if (blockingLaserTileParent.childCount > 0)
        {
            for (int i = 0; i < blockingLaserTileParent.childCount; i++)
            {
                blockingLaserTile.Add(blockingLaserTileParent.GetChild(i).transform);              
            }
        }
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.SetPosition(0, transform.position);

        switch (laserDir)
        {
            case LaserDirection.Top:
                lineRenderer.SetPosition(1, blockingLaserTileParent.GetChild(0).transform.position);
                blockingLaserTileChildTransform = blockingLaserTileParent.GetChild(0).transform;
                break;
            case LaserDirection.Right:
                lineRenderer.SetPosition(1, blockingLaserTileParent.GetChild(1).transform.position);
                blockingLaserTileChildTransform = blockingLaserTileParent.GetChild(1).transform;
                break;
            case LaserDirection.Bottom:
                lineRenderer.SetPosition(1, blockingLaserTileParent.GetChild(2).transform.position);
                blockingLaserTileChildTransform = blockingLaserTileParent.GetChild(2).transform;
                break;
            case LaserDirection.Left:
                lineRenderer.SetPosition(1, blockingLaserTileParent.GetChild(3).transform.position);
                blockingLaserTileChildTransform = blockingLaserTileParent.GetChild(3).transform;
                break;
            default:
                break;
        }

        SetBoxCollider2D(gameObject.transform, blockingLaserTileChildTransform, bc);
    }

    private void SetBoxCollider2D(Transform startObject, Transform endObject, BoxCollider2D boxCollider)
    {
        startPosX = startObject.position.x;
        startPosY = startObject.position.y;
        endPosX = endObject.position.x;
        endPosY = endObject.position.y;
        Vector2 tmpPos = new Vector2(Mathf.Abs(startPosX - endPosX), Mathf.Abs(startPosY - endPosY));
        Vector2 tmpPosNotAbs = new Vector2(startPosX - endPosX, startPosY - endPosY);

        lineRenderer.SetPosition(1, blockingLaserTileChildTransform.position);

        if (tmpPos.x == 0 || laserDir == LaserDirection.Top || laserDir == LaserDirection.Bottom)
        {
            tmpPos.x = 0.1f;
            boxCollider.offset = new Vector2(0.0f, -tmpPosNotAbs.y / 2);
        }
        else if (tmpPos.y == 0 || laserDir == LaserDirection.Right || laserDir == LaserDirection.Left)
        {
            tmpPos.y = 0.1f;
            boxCollider.offset = new Vector2(-tmpPosNotAbs.x / 2, 0.0f);
        }
        boxCollider.size = tmpPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.transform.parent != this)
        {
            SetBoxCollider2D(gameObject.transform, collision.gameObject.transform, bc);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.transform.parent != this)
        {
            SetBoxCollider2D(gameObject.transform, blockingLaserTileChildTransform.gameObject.transform, bc);
        }
    }
}
