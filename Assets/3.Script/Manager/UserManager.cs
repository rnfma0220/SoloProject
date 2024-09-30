using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class User
{
    public string User_Color { get; set; }
    public string Token { get; set; }
    public string Nickname { get; set; }

    public User (string color, string token, string nickname)
    {
        User_Color = color;
        Token = token;
        Nickname = nickname;
    }
}

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }

    public User user;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

