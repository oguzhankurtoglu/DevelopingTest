using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatorStick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController._canMove = false;
        var direction = transform.position - collision.transform.position;
        Vector3 difference = new Vector3(direction.x, 0, direction.z);
        collision.gameObject.transform.DOMove((collision.gameObject.transform.position + difference * -2), 1f).OnComplete(() => PlayerController._canMove = true);
    }
}
