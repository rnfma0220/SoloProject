using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControll : MonoBehaviour
{
    [SerializeField] private Button Login_Button;
    [SerializeField] private GameObject Login_Panel;
    [SerializeField] private GameObject Signup_Panel;
    [SerializeField] private GameObject Character_Panel;

    public void Login_Btu()
    {
        Login_Panel.SetActive(true);
    }

    public void Signup_Btu()
    {
        Signup_Panel.SetActive(true);
    }

    public void Exit_Btu()
    {

    }

    public void Close_Btu()
    {
        Login_Panel.SetActive(false);
        Signup_Panel.SetActive(false);
        Character_Panel.SetActive(false);
    }
}
