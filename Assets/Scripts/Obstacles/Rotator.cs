using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : RotatingPlatform
{
    private void FixedUpdate()
    {
        LocalRotate(vectorType);
    }
}
