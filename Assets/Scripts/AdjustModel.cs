using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class AdjustModel : MonoBehaviour
{
    public enum Transforms
    {
        MOVE_X,
        MOVE_Y,
        MOVE_Z,
        ROTATE_X,
        ROTATE_Y,
        ROTATE_Z,
        SCALE
    }
    Vector3 savedPosition;
    Vector3 savedScale;
    Transforms actualTransform = Transforms.MOVE_X;

    Vector3 rotationAngles;

    Vector3 savedRotation;

    public float interval = 0.01f;
    bool firstPosition = false;


    public void GetFirstControllerPosition()
    {
        //Si aun no hay player prefs, usar posicion defecto editor
        if (!PlayerPrefs.HasKey("PositionX"))
        {
            SavePrefs();
        }

        // Cargar la posición guardada
        transform.localPosition = new Vector3(
            PlayerPrefs.GetFloat("PositionX"),
            PlayerPrefs.GetFloat("PositionY"),
            PlayerPrefs.GetFloat("PositionZ")
        );
        // Cargar la rotación guardada

        savedRotation = new Vector3(
           PlayerPrefs.GetFloat("RotationX"),
           PlayerPrefs.GetFloat("RotationY"),
           PlayerPrefs.GetFloat("RotationZ")
       );

        //Cargar la escala guardada
        transform.localScale = new Vector3(
            PlayerPrefs.GetFloat("Scale"),
            PlayerPrefs.GetFloat("Scale"),
            PlayerPrefs.GetFloat("Scale")
        );
        savedPosition = transform.localPosition;
        transform.localRotation = Quaternion.Euler(savedRotation);
        savedScale = transform.localScale;

        if (!firstPosition)
        {
            //Cuando se detecta el mando en el primer frame, deja de seguir al mando
            GameObject sceneParent = new GameObject("Cinta");
            sceneParent.tag = "Cinta";
            Transform controller = transform.parent;
            sceneParent.transform.position = controller.transform.position;
            sceneParent.transform.rotation = controller.transform.rotation;
            sceneParent.transform.localScale = controller.transform.localScale;

            transform.parent = sceneParent.transform;
            firstPosition = true;
        }

       
    }

    /*void Start()
    {
        //Si aun no hay player prefs, usar posicion defecto editor
        if (!PlayerPrefs.HasKey("PositionX"))
        {
            SavePrefs();
        }

        // Cargar la posición guardada
        transform.localPosition = new Vector3(
            PlayerPrefs.GetFloat("PositionX"),
            PlayerPrefs.GetFloat("PositionY"),
            PlayerPrefs.GetFloat("PositionZ")
        );
        // Cargar la rotación guardada

         savedRotation = new Vector3(
            PlayerPrefs.GetFloat("RotationX"),
            PlayerPrefs.GetFloat("RotationY"),
            PlayerPrefs.GetFloat("RotationZ")
        );

        //Cargar la escala guardada
        transform.localScale = new Vector3(
            PlayerPrefs.GetFloat("Scale"),
            PlayerPrefs.GetFloat("Scale"),
            PlayerPrefs.GetFloat("Scale")
        );
        savedPosition = transform.localPosition;
        transform.localRotation = Quaternion.Euler(savedRotation) ;
        savedScale = transform.localScale;
    }*/

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.Alpha1)) MoveX();
        if (Input.GetKeyDown(KeyCode.Alpha2)) MoveY();
        if (Input.GetKeyDown(KeyCode.Alpha3)) MoveZ();
        if (Input.GetKeyDown(KeyCode.Alpha4)) RotateX();
        if (Input.GetKeyDown(KeyCode.Alpha5)) RotateY();
        if (Input.GetKeyDown(KeyCode.Alpha6)) RotateZ();
        if (Input.GetKeyDown(KeyCode.Space)) SavePrefs();*/

        if (OVRInput.GetUp(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.A))
        {
            switch (actualTransform)
            {
                case Transforms.MOVE_X:
                    //savedPosition.x += interval

                    transform.Translate(Vector3.right*interval, Space.World);
                    break;
                case Transforms.MOVE_Y:
                    //savedPosition.y += interval;
                    transform.Translate(Vector3.up * interval, Space.World);
                    break;
                case Transforms.MOVE_Z:
                    //savedPosition.z += interval;
                    transform.Translate(Vector3.forward * interval, Space.World);
                    break;
                case Transforms.ROTATE_X:
                   // rotationAngles.x += 1f;
                    transform.Rotate(Vector3.right, 1f, Space.World);
                    break;
                case Transforms.ROTATE_Y:
                    rotationAngles.y += 1f;
                    transform.Rotate(Vector3.up, 1f, Space.World);
                    break;
                case Transforms.ROTATE_Z:
                    rotationAngles.z += 1f;
                    transform.Rotate(Vector3.forward, 1f, Space.World);
                    break;
                case Transforms.SCALE:
                    savedScale.x += interval;
                    savedScale.y += interval;
                    savedScale.z += interval;
                    break;
                default:
                    break;
            }

            savedPosition = transform.localPosition;
            savedRotation = transform.localEulerAngles;
            transform.localScale = savedScale;
        }

        else if (OVRInput.GetUp(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.S))
        {
            switch (actualTransform)
            {
                case Transforms.MOVE_X:
                    //savedPosition.x -= interval;
                    transform.Translate(Vector3.right * -interval, Space.World);
                    break;
                case Transforms.MOVE_Y:
                    //savedPosition.y -= interval;
                    transform.Translate(Vector3.up * -interval, Space.World);
                    break;
                case Transforms.MOVE_Z:
                    //savedPosition.z -= interval;
                    transform.Translate(Vector3.forward * -interval, Space.World);
                    break;
                case Transforms.ROTATE_X:
                    //rotationAngles.x -= 1f;
                    transform.Rotate(Vector3.right, -1f, Space.World);
                    break;
                case Transforms.ROTATE_Y:
                    transform.Rotate(Vector3.up, -1f, Space.World);
                    break;
                case Transforms.ROTATE_Z:
                    transform.Rotate(Vector3.forward, -1f, Space.World);
                    break;
                default:
                    break;
            }


            savedPosition = transform.localPosition;
            savedRotation = transform.localEulerAngles ;
            transform.localScale = savedScale;
        }

    }




    public void MoveX()
    {
        actualTransform = Transforms.MOVE_X;
    }

    public void MoveY()
    {
        actualTransform = Transforms.MOVE_Y;
    }

    public void MoveZ()
    {
        actualTransform = Transforms.MOVE_Z;
    }

    public void RotateX()
    {
        actualTransform = Transforms.ROTATE_X;
    }

    public void RotateY()
    {
        actualTransform = Transforms.ROTATE_Y;
    }

    public void RotateZ()
    {
        actualTransform = Transforms.ROTATE_Z;
    }

    public void Scale()
    {
        actualTransform = Transforms.SCALE;
    }

    // Guardar los valores en PlayerPrefs
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("PositionX", savedPosition.x);
        PlayerPrefs.SetFloat("PositionY", savedPosition.y);
        PlayerPrefs.SetFloat("PositionZ", savedPosition.z);

        PlayerPrefs.SetFloat("RotationX", savedRotation.x);
        PlayerPrefs.SetFloat("RotationY", savedRotation.y);
        PlayerPrefs.SetFloat("RotationZ", savedRotation.z);

        PlayerPrefs.SetFloat("Scale", savedScale.x);

        PlayerPrefs.Save();
    }
}

