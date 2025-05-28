using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    public float maxYCameraRotation = 70f;

    private GameObject ui;

    public bool reading = false;
    // Start is called before the first frame update
    void Start()
    {
        cam.fieldOfView = 60.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CloseUI()
    {
        Debug.Log("Hi");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        reading = false;
        ui.SetActive(false);
        ui = null;
    }
    // Update is called once per frame
    void Interactable()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit itemCheck, 4f);
        if (itemCheck.collider != null && itemCheck.collider.gameObject.CompareTag("Interactable"))
        {
            ui = itemCheck.collider.gameObject.GetComponent<Interactable>().objectUI;
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
    void Update()
    {
        if (ui != null) { 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Cursor.visible = true;
                reading = true;
                ui.SetActive(true);
                Cursor.lockState = CursorLockMode.None;  
            }
        }

    }
        void FixedUpdate()
        {
        if (!reading) {
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraVerticalRotation -= inputY;

            if (cameraVerticalRotation > maxYCameraRotation || cameraVerticalRotation < -maxYCameraRotation)
            {
                if (cameraVerticalRotation < 0f)
                {
                    cameraVerticalRotation = -maxYCameraRotation;
                }
                else
                {
                    cameraVerticalRotation = maxYCameraRotation;
                }
            }
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            Camera.main.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            player.Rotate(Vector3.up * inputX);

            Interactable();
        }
    }
}
