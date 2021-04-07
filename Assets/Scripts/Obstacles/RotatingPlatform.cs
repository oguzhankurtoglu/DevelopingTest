using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] private bool _rightTurn;
    [SerializeField] private float _slepPlayerDegree;
    [SerializeField] protected VectorType vectorType;
    public enum VectorType
    {
        VectorUp,
        VectorForward
    }

    public void LocalRotate(VectorType selectedType)
    {
        if (selectedType==VectorType.VectorUp)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _rotateSpeed);
        }
        else if (selectedType==VectorType.VectorForward)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * _rotateSpeed);

        }
    }
    private void FixedUpdate()
    {
        LocalRotate(vectorType);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_rightTurn)
        {
            collision.transform.position += new Vector3(-_slepPlayerDegree * Time.deltaTime, 0, 0);
        }
        else if (!_rightTurn)
        {
            collision.transform.position += new Vector3(_slepPlayerDegree * Time.deltaTime, 0, 0);

        }

    }

}
