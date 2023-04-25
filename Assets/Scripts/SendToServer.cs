using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToServer : MonoBehaviour
{
    [System.Serializable]
    public class LastLevels{
        public string message;
        public int last_level;
        public int last_diff;
    }
    // Start is called before the first frame update
    void Start()
    {
        Completed_Levels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public const string baseURL = "http://20.83.162.38:8000/";
    private const string LOGOUT_URL = baseURL + "api/dologout/"; // Replace with your actual login URL
    private const string LVLCOMP_URL = baseURL + "api/lvlcomplete/"; // Replace with your actual login URL
    private const string COMPLTD_LVLS_URL = baseURL + "api/completed_levels/"; // Replace with your actual login URL

    public void SendToServerF()
    {
        GameObject px = GameObject.Find("PlayerX");
        PlayerData pd = px.GetComponent<PlayerData>();
        string message = JsonUtility.ToJson(pd.player);
        Debug.Log(message);
        LevelComplete();
        StartCoroutine(SendLogoutData(message));

    }
    
    public void LevelComplete()
    {
        GameObject px = GameObject.Find("PlayerX");
        PlayerData pd = px.GetComponent<PlayerData>();
        string message = JsonUtility.ToJson(pd.player);
        Debug.Log(message);
        Debug.Log("iniciando el server");
        StartCoroutine(SendLevelComplete(message));
        Debug.Log("Termino el server");
    }
    public void Completed_Levels()
    {
        GameObject px = GameObject.Find("PlayerX");
        PlayerData pd = px.GetComponent<PlayerData>();
        string message = JsonUtility.ToJson(pd.player);
        Debug.Log(message);
        StartCoroutine(GetCompletedLvls(message));
    }
    
    IEnumerator SendLogoutData(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);

        using (UnityWebRequest www = UnityWebRequest.Post(LOGOUT_URL, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
                Application.Quit();
            }
        }
    }

    IEnumerator SendLevelComplete(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);

        using (UnityWebRequest www = UnityWebRequest.Post(LVLCOMP_URL, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
                Application.Quit();
            }
        }
    }

    IEnumerator GetCompletedLvls(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);

        using (UnityWebRequest www = UnityWebRequest.Post(COMPLTD_LVLS_URL, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                // Set default values of 1 (Easy, first level) if anything goes wrong.
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);

                LastLevels ms = JsonUtility.FromJson<LastLevels>(txt);
                Debug.Log("Message: " + ms.message);
                Debug.Log("Last completed level = " + ms.last_level);
                Debug.Log("Last completed difficulty " + ms.last_diff);

                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.dificultad = ms.last_diff;
                pd.player.nivel = ms.last_level;

                // SceneManager.LoadScene("Assets/Scenes/LevelSelector.unity");
            }
        }
    }
}
