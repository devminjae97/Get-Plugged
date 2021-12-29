using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform buttonScale_TR;
    Vector3 defaultScale_V;
    private void Start()
    {
        defaultScale_V = buttonScale_TR.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale_TR.localScale = defaultScale_V * 1.1f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale_TR.localScale = defaultScale_V;
    }
}
