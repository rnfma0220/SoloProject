using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputField_Clean : MonoBehaviour
{
    private TMP_InputField inputField;

    private void OnEnable()
    {
        TryGetComponent(out inputField);

        inputField.text = string.Empty;
    }
}
