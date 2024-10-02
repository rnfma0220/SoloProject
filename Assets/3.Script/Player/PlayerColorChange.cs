using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Character;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerColorData
{
    public string PlayerColor;
}

public class PlayerColorChange : MonoBehaviour
{
    [SerializeField] private ViewPlayer actor;
    [SerializeField] private TMP_Text Nickname;
    [SerializeField] private Slider slider_R, slider_G, slider_B;

    private void OnEnable()
    {
        string hex = UserManager.Instance.user.User_Color;
        Nickname.text = PhotonNetwork.NickName;

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        slider_R.value = r;
        slider_G.value = g;
        slider_B.value = b;
    }

    public void EditRGB()
    {
        if (actor.bodyType == null) return;
        
        Color color = new Color32((byte)slider_R.value, (byte)slider_G.value, (byte)slider_B.value, 255);
        string hexColor = ColorToHex(color);
        actor.bodyType.HexColor = hexColor;
        actor.bodyType.ColorChange();
    }

    private string ColorToHex(Color color)
    {
        return string.Format("{0:X2}{1:X2}{2:X2}",
                             (int)(color.r * 255),
                             (int)(color.g * 255),
                             (int)(color.b * 255));
    }

    public void SetRGB()
    {
        string ColorHex = actor.bodyType.HexColor;
        StartCoroutine(ColorCoroutine(ColorHex));
    }

    private IEnumerator ColorCoroutine(string color)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/changecolor";

        string token = UserManager.Instance.user.Token;

        PlayerColorData data = new PlayerColorData()
        {
            PlayerColor = color
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer "+ token);

            yield return request.SendWebRequest();

            string requesttext = request.downloadHandler.text;
            //Debug.Log(requesttext);

            UserManager.Instance.user = new User(color, UserManager.Instance.user.Token, UserManager.Instance.user.Nickname);
        }
    }
}
