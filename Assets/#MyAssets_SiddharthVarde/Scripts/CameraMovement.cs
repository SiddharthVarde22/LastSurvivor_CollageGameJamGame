using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float camRotSpeedUp = 10f;
    [SerializeField]
    float maxCamUpRotPositive = 45f;
    [SerializeField]
    float maxCamRotDownnegative = -45f;

    float camRotInputUp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camRotInputUp = Input.GetAxis("Mouse Y") * -1 * camRotSpeedUp * Time.deltaTime;

        CamRotate();
    }

    void CamRotate()
    {
        transform.localRotation *= Quaternion.Euler(camRotInputUp, 0, 0);

        if(transform.rotation.eulerAngles.x > 180 && transform.rotation.eulerAngles.x < (360 + maxCamRotDownnegative))
        {
            transform.localRotation = Quaternion.Euler(maxCamRotDownnegative, 0, 0);
        }

        if(transform.rotation.eulerAngles.x < 180 && transform.rotation.eulerAngles.x > maxCamUpRotPositive)
        {
            transform.localRotation = Quaternion.Euler(maxCamUpRotPositive, 0, 0);
        }
    }
}
