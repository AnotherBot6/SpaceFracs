using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    private float rollSpeed = 90f, rollAccelearion = 3.5f;

    public bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        //Centro de cualquier pantalla
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento con mouse
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        //Desde el centro, movernos con el mouse
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        //Girar
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAccelearion * Time.deltaTime);

        //Rotar con mouse
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        //Movernos: izquierda-derecha, arriba-abajo. Y deslizarnos
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        // Realizar un raycast para detectar si hay un objeto en la dirección del movimiento de la nave
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, activeForwardSpeed * Time.deltaTime))
        {
            if (hit.collider.gameObject.tag == "Cube" || hit.collider.gameObject.tag == "Asteroid")
            {
                isColliding = true;
            }
        }
        else
        {
            isColliding = false;
        }

        //Mover la nave solo si no está colisionando con un objeto
        if (!isColliding)
        {
            transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
        }
    }
}
