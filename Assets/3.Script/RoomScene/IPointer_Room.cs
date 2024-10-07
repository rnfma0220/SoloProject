using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class IPointer_Room : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 Scale;
    private Text text;

    private void OnEnable()
    {
        Scale = transform.localScale;
        TryGetComponent(out text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.GetComponent<Button>().interactable == true)
        {
            transform.localScale = Scale * 1.3f;
            text.color = Color.yellow;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Scale;
        text.color = Color.white;
    }
}
