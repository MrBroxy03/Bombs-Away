using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rB;
    public float Speed = 5f;
    private CameraController camController;
    // Start is called before the first frame update
    void Start()
    {
        camController = GetComponent<CameraController>();
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0, moveZ = 0;

        if (!camController.reading)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveZ = Speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveZ = -Speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -Speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX = Speed;
            }
        }
        rB.velocity = ((this.transform.forward * moveZ) + (this.transform.right * moveX));
    }
}
