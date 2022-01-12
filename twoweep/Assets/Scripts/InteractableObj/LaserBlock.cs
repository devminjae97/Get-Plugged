using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlock : Interactor
{
    public Transform initPos;
    public Sprite laserFlipSprite;
    int layerMask;
    Transform m_transform;

    public enum laserDirection
    {
        Top, Right, Bottom, Left
    }

    public laserDirection laserDir;
    SpriteRenderer sr;

    public override void StoreInitValues() {
        // none
    }

    public override void ResetValues() {
        // none
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); 
        layerMask = 1 << LayerMask.NameToLayer("LayerMask");
        layerMask = ~layerMask;
        m_transform = initPos;
    }

    private void Update()
    {
        DrawLaser();
    }

    void DrawLaser()
    {
        RaycastHit2D _hit;
        switch (laserDir)
        {   
            case laserDirection.Top:
                _hit = Physics2D.Raycast(m_transform.position, m_transform.up, 50, layerMask);
                transform.localScale = new Vector3(transform.localScale.x, _hit.distance, transform.localScale.z);
                transform.localPosition = new Vector3(transform.localPosition.x, _hit.distance / 2, transform.localPosition.z);
                sr.sprite = laserFlipSprite;
                break;
            case laserDirection.Right:
                _hit = Physics2D.Raycast(m_transform.position, m_transform.right, 50, layerMask);
                transform.localScale = new Vector3(_hit.distance, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(_hit.distance / 2, transform.localPosition.y, transform.localPosition.z);
                break;
            case laserDirection.Bottom:
                _hit = Physics2D.Raycast(m_transform.position, m_transform.up * -1, 50, layerMask);
                transform.localScale = new Vector3(transform.localScale.x, _hit.distance, transform.localScale.z);
                transform.localPosition = new Vector3(transform.localPosition.x, (-1 * _hit.distance / 2), transform.localPosition.z);
                sr.sprite = laserFlipSprite;
                break;
            case laserDirection.Left:
                _hit = Physics2D.Raycast(m_transform.position, m_transform.right * -1, 50, layerMask);
                transform.localScale = new Vector3(_hit.distance, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3((-1 * _hit.distance) / 2, transform.localPosition.y, transform.localPosition.z);
                break;
            default:
                break;
        }
    }
}
