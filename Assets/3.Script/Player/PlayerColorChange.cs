using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System.Text;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerColorData
{
    public string PlayerColor;
}

public class PlayerColorChange : MonoBehaviour
{
    [SerializeField] private Actor actor;

    public void ColorCh(string ColorHex)
    {
        actor.bodyType.HexColor = ColorHex;
        PlayerPrefs.SetString("playercolor", ColorHex);
        actor.bodyType.ColorChange();
        StartCoroutine(ColorCoroutine(ColorHex));
    }


    private IEnumerator ColorCoroutine(string color)
    {
        string uri = "https://kiwebmeta7mh.o-r.kr/api/changecolor";

        string token = PlayerPrefs.GetString("playertoken");

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
            Debug.Log(requesttext);
            PlayerPrefs.SetString("playercolor", color);
        }
    }
}
