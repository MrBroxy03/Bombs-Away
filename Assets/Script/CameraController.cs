using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    public float maxYCameraRotation = 70f;
    public float itemrange = 2f;

    private GameObject interactableobj;

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
        reading = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        interactableobj.GetComponent<Interactable>().objectUI.SetActive(false);
        interactableobj = null;
    }
    // Update is called once per frame
    void Interactable()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit itemCheck, itemrange);
        if (itemCheck.collider != null && itemCheck.collider.gameObject.CompareTag("Interactable"))
        {
            interactableobj = itemCheck.collider.gameObject;
            Cursor.visible = true;
        }
        else
        {
            interactableobj = null;
            Cursor.visible = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CloseUI();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (interactableobj != null) { 
            
                Cursor.visible = true;
                reading = true;
                interactableobj.GetComponent<Interactable>().objectUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;  

                if (interactableobj.GetComponent<Interactable>().customTxt != null && interactableobj.GetComponent<Interactable>().customTxt != "")
                {
                    interactableobj.GetComponent<Interactable>().objectUI.GetComponentInChildren<TextMeshProUGUI>().text = interactableobj.GetComponent<Interactable>().customTxt;
                }
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
