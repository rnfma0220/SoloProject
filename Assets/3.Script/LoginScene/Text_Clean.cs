using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Clean : MonoBehaviour
{
    private TMP_Text TMP_Text;

    private void OnEnable()
    {
        TryGetComponent(out TMP_Text);

        TMP_Text.text = string.Empty;
    }
}
