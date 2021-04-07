using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerController : MonoBehaviour
{
   [SerializeField] private float _rollerSensivity;
    private void MoveRoller()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,transform.position.z);
    }
    private void Update()
    {
        MoveRoller();
    }
}
