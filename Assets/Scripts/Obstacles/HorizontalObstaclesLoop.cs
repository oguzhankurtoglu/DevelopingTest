using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class HorizontalObstaclesLoop : MonoBehaviour
{
    [SerializeField] private Vector3 EndPosition;
    [SerializeField]private float duration;


    private void Start()
    {
        transform.DOMove(EndPosition,duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InQuad);
    }
}
