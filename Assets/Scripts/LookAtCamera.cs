using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode{
        LookAt,
        LookAtInverse,
        CameraForward,
        CameraForwardInverse
    }
    [SerializeField] private Mode mode;
    void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverse:
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverse:
                transform.forward = Camera.main.transform.forward;
                transform.Rotate(0, 180, 0);
                break;
            default:
                break;
        }
    }
}
