using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputUser : MonoBehaviour
{
    //public string numberList, group;

    public TMP_InputField grupo, numLista;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitUser()
    {
        string g = grupo.text;
        string n = numLista.text;
        StartCoroutine(SendLoginData(g, n));
        Debug.Log("Enter Pressed");
    }

    IEnumerator SendLoginData(string gr, string nl)
    {
        WWWForm form = new WWWForm();
        form.AddField("numList", nl);
        form.AddField("grupo", gr);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8000/api/dologin", form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
                txt = txt.Substring(1);
                txt = txt.Substring(0, txt.Length - 2);
                Player ms = JsonUtility.FromJson<Player>(txt);
                Debug.Log("group: " + ms.grupo);
                Debug.Log("list number: " + ms.numLista);
                
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.grupo = ms.grupo;
                pd.player.numLista = ms.numLista;
                SceneManager.LoadScene("Assets/Scenes/LevelSelector.unity");
            }
        }
    }
}
