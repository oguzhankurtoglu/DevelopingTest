using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 _offset;
    private float _smoothRatio = 1;


    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        CameraFollow();
    }
    private void CameraFollow()
    {
        if (GameManager.state==GameManager.State.Running)
        {
            Vector3 destination = target.position + _offset;
            Vector3 smooth = Vector3.Lerp(transform.position, destination, _smoothRatio);
            transform.position = new Vector3(smooth.x, transform.position.y, smooth.z);
        }
    }
}
