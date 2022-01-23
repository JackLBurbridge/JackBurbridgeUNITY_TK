using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraTrigger : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            camera1.enabled = false;
            camera2.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

}
