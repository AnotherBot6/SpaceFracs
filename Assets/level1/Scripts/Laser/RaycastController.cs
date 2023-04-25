using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public AudioSource audioLaser;
    public float raycastDistance = 5000f; // la distancia del raycast
    public float objectDistance = 130f; // la distancia a la que se puede arrastrar el planeta
    public LayerMask layerMask; // la capa de objetos con la que colisionará el raycast

    private bool isFiring = false; // indica si el raycast está activo
    private GameObject currentObject = null; // el planeta que se está arrastrando actualmente

    void Update()
    {
        // Comprueba si el botón izquierdo del ratón está pulsado
        if (Input.GetMouseButton(0))
        {
            isFiring = true; // activa el raycast

        }
        else
        {
            isFiring = false; // desactiva el raycast

            if (currentObject != null)
            {
                // Detiene el movimiento del objeto que estaba siendo arrastrado
                Rigidbody objectRb = currentObject.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    objectRb.velocity = Vector3.zero;
                    objectRb.angularVelocity = Vector3.zero;
                }
            }
            currentObject = null; // limpia la variable del objeto arrastrado
        }
        if (Input.GetMouseButtonDown(0))
        {
            audioLaser.Play(); //laser
            audioLaser.volume = 0.2f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioLaser.Stop(); //laser

        }

    }

    void FixedUpdate()
    {
        // Comprueba si el raycast está activo
        if (isFiring)
        {
            RaycastHit hit;
            // Genera el raycast desde la posición de la nave en la dirección en la que está mirando
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, layerMask))
            {

                
                Debug.DrawLine(transform.position, hit.point, Color.red); // Dibuja el raycast
                                                                          // Comprueba la etiqueta del objeto colisionado
                //if (hit.collider.gameObject.CompareTag("Planet") || hit.collider.CompareTag("Asteroid") || hit.collider.CompareTag("Star") || hit.collider.CompareTag("Coin") || hit.collider.CompareTag("Rocket") || hit.collider.CompareTag("Ufo") || hit.collider.CompareTag("Alien"))
                //{
                    currentObject = hit.collider.gameObject;
                    // Calcula la dirección y la velocidad a la que se moverá el objeto
                    Vector3 objectDirection = hit.point - transform.position;
                    Vector3 objectVelocity = transform.forward * objectDirection.magnitude * 2f;
                    // Limita la velocidad para evitar que el objeto se mueva demasiado rápido
                    if (objectVelocity.magnitude > 20f)
                    {
                        objectVelocity = objectVelocity.normalized * 20f;
                    }
                    // Aplica la velocidad al planeta
                    Rigidbody planetRb = currentObject.GetComponent<Rigidbody>();
                    if (planetRb != null)
                    {
                        planetRb.velocity = objectVelocity;
                    }
                    // Actualiza la posición del objeto para que siempre esté a una distancia constante de la nave
                    Vector3 objectPosition = transform.position + transform.forward * objectDistance;
                    currentObject.transform.position = objectPosition;
                //}

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.forward * raycastDistance, Color.green); // Dibuja el raycast
                                                                                                                           // Si el raycast no colisiona con nada, detiene el movimiento del objeto arrastrado
                if (currentObject != null)
                {
                    Rigidbody objectRb = currentObject.GetComponent<Rigidbody>();
                    if (objectRb != null)
                    {
                        objectRb.velocity = Vector3.zero;
                        objectRb.angularVelocity = Vector3.zero;
                    }
                }
                currentObject = null; // limpia la variable del objeto arrastrado
            }
        }
    }
}
