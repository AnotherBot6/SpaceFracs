using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class APIConnection : MonoBehaviour
{
    public const string baseURL = "http://20.83.162.38:8000/";
    private const string LOGIN_URL = baseURL + "api/dologin/"; // Replace with your actual login URL
    public TMP_InputField group, numberList;

    void Start(){}

    // Update is called once per frame
    void Update(){}

    private int int_ify(string str){
        int strToInt = -1;  
        if (int.TryParse(str, out strToInt)) {
            // Conversion was successful
            Debug.Log("Conversion successful! idToInt = " + strToInt);
        }
        else {
            // Conversion failed
            Debug.LogError("Conversion failed! Could not convert id to int.");
        }
        return strToInt;
    }

    public void submitUser()
    {
        string g = group.text;
        string n = numberList.text;
        int gr = int_ify(g);
        int nl = int_ify(n);
        StartCoroutine(SendLoginData(gr, nl));
    }
    

    [System.Serializable]
    public class JugadorData{
        public int grupo;
        public int numLista;
    }   

    [System.Serializable]
    public class Session{
        public string message;
        public int id_session;
    }
    // public IEnumerator SendLoginData(string grupo, string numLista)
    public IEnumerator SendLoginData(int grupo, int numLista)
    {
        // Hacer clase y asignar variables
        JugadorData jugadorData = new JugadorData();
        jugadorData.grupo = grupo;
        jugadorData.numLista = numLista;
        // Hacerlo Json 
        string player = JsonUtility.ToJson(jugadorData);
        Debug.Log("player" + player);


        WWWForm form = new WWWForm();
        form.AddField("player", player);
        // Create a UnityWebRequest using the PUT method
        using (UnityWebRequest www = UnityWebRequest.Post(LOGIN_URL, form))
        {   
            Debug.Log("Sending form...");
            yield return www.SendWebRequest();
            Debug.Log("Done!");

            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);

                Session ms = JsonUtility.FromJson<Session>(txt);
                Debug.Log("ID de Sesion = " + ms.id_session);
                GameObject po = GameObject.Find("PlayerX");

                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.grupo = grupo;
                pd.player.numLista = numLista;
                pd.player.id_session = ms.id_session;
                SceneManager.LoadScene("Assets/Scenes/LevelSelector.unity");
            }
        }
    }
    

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
