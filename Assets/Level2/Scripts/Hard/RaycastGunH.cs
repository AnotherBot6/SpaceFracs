using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGunH : MonoBehaviour
{
    
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 80f;
    public float fireRate = 0.4f;
    public float laserDuration = 3f;
    public int asteroidsToDestroy; // Nuevo: cantidad de asteroides a destruir para ganar
    public AudioSource audioLaser;
    public AudioSource audioExplosion;

    public GameObject explosion;
    float explosionTime = 0f;

    LineRenderer laserLine;
    float fireTimer;
    public int asteroidsDestroyed = 0; // Nuevo: contador de asteroides destruidos

    //Win-Lose
    private bool activated = false;

    // Operaciones 
    public int num1;
    public int num2;
    public int num3;
    public string operation1;
    public string operation2;

    void Start()
    {
        GenerateMathProblem();
    }

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
    // Poner un nuevo asset, para que al entrar a el haga un check point y diga: has ganado, has perdido.
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            audioLaser.Play(); //laser
            audioLaser.volume = 0.2f;
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOring = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOring, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.transform.gameObject.CompareTag("Asteroid")) // Destruir asteroides
                {
                    Destroy(hit.transform.gameObject);
                    asteroidsDestroyed++;
                    audioExplosion.Play(); //explosion
                    audioExplosion.volume = 1f;
                    if (explosionTime <= 0) // Si no hay una explosión activa, crea una nueva
                    {
                        Instantiate(explosion, hit.transform.position, Quaternion.identity);
                        explosionTime = Time.time + 1f; // El tiempo de vida de la explosión será de 2 segundos
                    }
                }
                if (hit.transform.gameObject.CompareTag("AsteroidPlus")) // Destruir planetas
                {
                    Destroy(hit.transform.gameObject);
                    asteroidsDestroyed += 5;
                    audioExplosion.Play(); //explosion
                    audioExplosion.volume = 1f;
                    if (explosionTime <= 0) // Si no hay una explosión activa, crea una nueva
                    {
                        Instantiate(explosion, hit.transform.position, Quaternion.identity);
                        explosionTime = Time.time + 1f; // El tiempo de vida de la explosión será de 2 segundos
                    }
                }
                if (hit.transform.gameObject.CompareTag("AsteroidSad")) // Destruir planetas
                {
                    Destroy(hit.transform.gameObject);
                    asteroidsDestroyed -= 5;
                    audioExplosion.Play(); //explosion
                    audioExplosion.volume = 1f;
                    if (explosionTime <= 0) // Si no hay una explosión activa, crea una nueva
                    {
                        Instantiate(explosion, hit.transform.position, Quaternion.identity);
                        explosionTime = Time.time + 1f; // El tiempo de vida de la explosión será de 2 segundos
                    }
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOring + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShoorLaser());
        }
        if (explosionTime > 0 && Time.time >= explosionTime) // Si hay una explosión activa, destrúyela después de un tiempo
        {
            Destroy(GameObject.FindGameObjectWithTag("Explosion"));
            explosionTime = 0f;
        }
    }

    public void GenerateMathProblem()
    {
        // Generate random numbers
        /*
        num1 = Random.Range(1, 101);
        num2 = Random.Range(1, 31);
        */
        // Generate random operation
        int opIndex = Random.Range(0, 4);
        switch (opIndex)
        {
            case 0:
                num1 = Random.Range(21, 51);
                num2 = Random.Range(1, 21);
                num3 = Random.Range(1, 11);
                operation1 = "+";
                operation2 = "-";
                asteroidsToDestroy = num1 + num2 - num3;
                break;
            case 1:
                num1 = Random.Range(21, 51);
                num2 = Random.Range(1, 21);
                num3 = Random.Range(1, 11);
                operation1 = "-";
                operation2 = "+";
                asteroidsToDestroy = num1 - num2 + num3;
                break;
            case 2:
                num1 = Random.Range(1, 11);
                num2 = Random.Range(1, 15);
                num3 = Random.Range(1, 21);
                operation1 = "*";
                operation2 = "+";
                asteroidsToDestroy = num1 * num2 + num3;
                break;
            case 3:
                num1 = Random.Range(11, 51);
                num2 = Random.Range(1, 21);
                num3 = Random.Range(1, 21);
                operation1 = "/";
                operation2 = "+";
                num1 *= num2; // multiply num1 to get a divisible number
                asteroidsToDestroy = (num1 / num2) + num3;
                break;
        }

    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 34;
        float labelWidth = 250;
        float labelHeight = 80;
        GUI.Label(new Rect(10, Screen.height - labelHeight - 10, labelWidth, labelHeight), "Asteroides destruidos: " + asteroidsDestroyed);
        GUI.skin.label.alignment = TextAnchor.LowerCenter;
        GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height - labelHeight - 10, labelWidth, labelHeight), "Operación:        " + num1 + " " + operation1 + " " + num2 + " " + operation2 + " " + num3 + " = ?");
    }

    IEnumerator ShoorLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
    
}