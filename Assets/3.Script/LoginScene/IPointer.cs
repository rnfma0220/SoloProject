using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class IPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 Scale;
    private TextMeshProUGUI textMeshPro;

    private void OnEnable()
    {
        Scale = transform.localScale;
        textMeshPro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Scale * 1.3f;
        textMeshPro.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Scale;
        textMeshPro.color = Color.white;
    }
}
