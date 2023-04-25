using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EarthLevel : MonoBehaviour
{
    public SendToServer script;
    public  GameObject LosePanel;
    public  GameObject WinPanel;
    [SerializeField] private static ControladorTiempo controlarTiempo;
    public enum DifficultyLevel { Easy, Medium, Hard }

    public DifficultyLevel difficultyLevel;
    public int minListSize;
    public int maxListSize;


    // List of tags
    private readonly List<string> tags = new List<string> { "Asteroid", "Planet", "Star", "Coin", "Alien", "Ufo", "Rocket" };
    private readonly Dictionary<string, int> maxCounts = new Dictionary<string, int>
{
    { "Asteroid", 6 },
    { "Planet", 5 },
    { "Star", 4 },
    { "Coin", 3 },
    { "Alien", 6 },
    { "Ufo", 4 },
    { "Rocket", 3 }
};

    // Dictionary to store the colors for each object type
    private readonly Dictionary<string, string[]> objectColors = new Dictionary<string, string[]>
{
    { "Asteroid", new string[] { "Gray" } },
    { "Planet", new string[] { "Blue", "Pink", "Orange" } },
    { "Star", new string[] { "Yellow", "Purple", "Green", "Pink" } },
    { "Coin", new string[] { "Purple", "Green", "Orange" } },
    { "Alien", new string[] { "Blue", "Orange", "Purple", "Green", "Pink" } },
    { "Ufo", new string[] { "Green", "Blue", "Purple" } },
    { "Rocket", new string[] { "Yellow", "Green" } }
};


    private List<string> objects = new List<string>();
    private List<GameObject> errors = new List<GameObject>();
    private string[] spriteArrays = {"","","","","",""};
    public GameObject objectsUI;
    public GameObject errorUI;
    private bool crossLost = false, win = false;
    private int objsCompleted = 0;
    public static int calificación;

    private int indexGame = 0;
    public static int currObjAmount;
    public static string currObj;
    private void Start()
    {
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        errorUI.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        errorUI.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        errorUI.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);

        objects = GenerateRandomObjectList(difficultyLevel);

        ShuffleList(objects);
        foreach (string obj in objects)
        {
            Debug.Log(obj);
        }

        ActivateSpawners(objects);

        List<GameObject> tagObjs = new List<GameObject>();
        int ind = 0;
        foreach (Transform child in objectsUI.transform)
        {
            int count = 0;
            string newSprite = "";
            foreach (char c in objects[ind])
            {
                if (c == '_')
                {
                    count++;
                    if (count == 2)
                    {
                        break;
                    }
                }
                newSprite += c;
            }
            spriteArrays[ind] = newSprite;
            child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(newSprite);
            child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = objects[ind][objects[ind].Length - 1].ToString();
            ind++;
            currObjAmount = objects[indexGame][objects[indexGame].Length - 1] - 48;
            currObj = spriteArrays[indexGame];
            Debug.Log("Current Object: " + currObj);
        }
        foreach (Transform child in errorUI.transform)
        {
            errors.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (indexGame <= objects.Count -1)
            objectsUI.transform.GetChild(indexGame).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currObjAmount.ToString();
        //Debug.Log("Current object amount: " + currObjAmount);
        if (currObjAmount == 0)
        {
            objsCompleted++;
            objectsUI.transform.GetChild(indexGame).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            objectsUI.transform.GetChild(indexGame).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            indexGame++;
            if(indexGame > objects.Count-1)
            {
                WinPanel.SetActive(true);
                
                Debug.Log("YOU WIN");
                win = true;
                if (difficultyLevel == DifficultyLevel.Easy)
                {
                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 1;
                    pd.player.dificultad = 1;
                    pd.player.calificacion = 100;
                    script.LevelComplete();
                }
                if (difficultyLevel == DifficultyLevel.Medium)
                {
                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 1;
                    pd.player.dificultad = 2;
                    pd.player.calificacion = 100;
                    script.LevelComplete();
                }
                if (difficultyLevel == DifficultyLevel.Hard)
                {
                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 1;
                    pd.player.dificultad = 3;
                    pd.player.calificacion = 100;
                    script.LevelComplete();
                }




            }
            else
            {
                currObjAmount = objects[indexGame][objects[indexGame].Length - 1] - 48;
                currObj = spriteArrays[indexGame];
                /*Debug.Log("New Current object: " + currObj);
                Debug.Log("New current object amount: " + currObjAmount);*/
            }
        }
        if(PortalEarthLevel.errorCount == 1)
        {
            errorUI.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            errorUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("RedCross");
        }
        else if(PortalEarthLevel.errorCount == 2)
        {
            errorUI.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            errorUI.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("RedCross");
        }
        else if(PortalEarthLevel.errorCount == 3)
        {
            errorUI.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            errorUI.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("RedCross");
            crossLost = true;
            LosePanel.SetActive(true);
            Debug.Log("Perdiste");
            getCalificación();
        }
    }

    public List<string> GenerateRandomObjectList(DifficultyLevel difficulty)
    {
        List<string> objects = new List<string>();

        // Define the number of objects to include in the list
        int objectCount = 6;


        // Check difficulty level to determine which colors to use
        Dictionary<string, string[]> colors = objectColors;

        if (difficulty == DifficultyLevel.Easy)
        {
            colors = new Dictionary<string, string[]>
        {
            { "Asteroid", new string[] { "Gray" } },
            { "Planet", new string[] { "Blue", "Pink" } },
            { "Star", new string[] { "Yellow", "Purple" } },
            { "Coin", new string[] { "Purple", "Green" } },
            { "Alien", new string[] { "Blue", "Orange" } },
            { "Ufo", new string[] { "Green", "Blue" } },
            { "Rocket", new string[] { "Yellow" } }

        };
        }
        else if (difficulty == DifficultyLevel.Medium)
        {
            colors = new Dictionary<string, string[]>
        {
            { "Asteroid", new string[] { "Gray" } },
            { "Planet", new string[] { "Blue", "Pink", "Orange" } },
            { "Star", new string[] { "Yellow", "Purple", "Green" } },
            { "Coin", new string[] { "Purple", "Green", "Orange" } },
            { "Alien", new string[] { "Blue", "Orange", "Purple" } },
            { "Ufo", new string[] { "Green", "Blue", "Purple" } },
            { "Rocket", new string[] { "Yellow", "Green" } }
        };
        }
        else if (difficulty == DifficultyLevel.Hard)
        {
            colors = objectColors;
        }

        // Loop through the tags and select a color randomly from the available colors
        for (int i = 0; i < objectCount; i++)
        {
            string tag = tags[i];
            string[] availableColors = colors[tag];
            string color = availableColors[Random.Range(0, availableColors.Length)];
            int count = Random.Range(1, maxCounts[tag] + 1);

            objects.Add(tag + "_" + color + "_" + count);
        }

        return objects;
    }

    public static void ActivateSpawners(List<string> objects)
    {
        // Find the GameObject that contains the spawners
        GameObject spawnerParent = GameObject.Find("Spawners");
        if (spawnerParent == null)
        {
            Debug.LogError("No se pudo encontrar el GameObject que contiene los spawners.");
            return;
        }

        // Enable the spawners for the objects in the list
        foreach (string obj in objects)
        {
            string[] objInfo = obj.Split('_');
            string tag = objInfo[0];
            string color = objInfo[1];

            // Find the spawner for the object
            string spawnerName = tag + "_" + color + "_Spawner";
            Transform spawner = spawnerParent.transform.Find(spawnerName);

            if (spawner != null)
            {
                spawner.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se pudo encontrar el spawner para el objeto " + obj);
            }
        }
    }
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private void getCalificación()
    {
        if(crossLost || FuelBehaviour.outOfFuel || !win) {
            if (difficultyLevel == DifficultyLevel.Easy) {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 1;
                pd.player.dificultad = 0;
                pd.player.calificacion = 10;
                script.LevelComplete();
            }
            if (difficultyLevel == DifficultyLevel.Medium)
            {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 1;
                pd.player.dificultad = 1;
                pd.player.calificacion = 10;
                script.LevelComplete();
            }
            if (difficultyLevel == DifficultyLevel.Hard)
            {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 1;
                pd.player.dificultad = 2;
                pd.player.calificacion = 10;
                script.LevelComplete();
            }

        }
        else if(objsCompleted < 5)
        {

        }else if(objsCompleted < 3)
        {

        }
    }
}





