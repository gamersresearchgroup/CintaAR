using UnityEngine;

public class MoveScenario : MonoBehaviour
{
    public float velocity;
    bool startMoving = false;
    Rigidbody rb;
    public GameObject cinta;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Alinear escenario con la dirección de la cinta al inicio
        AlignWithCinta();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) || Input.GetKeyDown(KeyCode.Return))
        {
            startMoving = true;
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || Input.GetKeyDown(KeyCode.Space))
        {
            startMoving = false;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (startMoving)
        {
            rb.MovePosition(transform.position + (-transform.forward) * velocity * Time.fixedDeltaTime);
        }
    }

    void AlignWithCinta()
    {
        // Alinear rotación
        float rotationX = transform.rotation.eulerAngles.x;
        float rotationZ = transform.rotation.eulerAngles.z;
        transform.forward = cinta.transform.forward;

        // Ajustar posición para centrar con la cinta
        float cintaCenterX = cinta.transform.position.x;
        transform.position = new Vector3(cintaCenterX, transform.position.y, transform.position.z);

        // Mantener rotación adecuada
        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y +45f, rotationZ);
    }
}

