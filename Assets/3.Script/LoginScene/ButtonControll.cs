using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonControll : MonoBehaviour
{
    [SerializeField] private Button Login_Button;
    [SerializeField] private GameObject Login_Panel;
    [SerializeField] private GameObject Signup_Panel;
    [SerializeField] private GameObject account_Panel;

    public void Login_Btu()
    {
        Login_Panel.SetActive(true);
    }

    public void Signup_Btu()
    {
        Signup_Panel.SetActive(true);
    }

    public void auaccount_Btu()
    {
        account_Panel.SetActive(true);
    }

    public void Exit_Btu()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
