using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    //Private Variables
    private GameObject _playerCamera;

    void Start()
    {
        _playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject;
    }

    void Update()
    {
        //Perform a smoothed Look At the user
        Vector3 _lookDirection = _playerCamera.transform.position - transform.position;
        _lookDirection.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookDirection), 10 * Time.deltaTime);

        Vector3 _currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, _currentRotation.y, 0.0f);
    }
}
