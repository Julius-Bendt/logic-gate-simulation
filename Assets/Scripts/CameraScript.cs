using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float moveSpeed, zoomSensitivity;

    public float zoomMin, zoomMax;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float fov = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        transform.Translate(input.normalized * moveSpeed * Time.deltaTime);


        cam.orthographicSize += fov;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomMin, zoomMax);


    }
}
