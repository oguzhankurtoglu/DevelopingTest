using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class HalfDonut : MonoBehaviour
{
    [SerializeField] private Vector3 vector;
    
    private void DonutLoop()
    {
        transform.DOLocalMove(vector,2f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutExpo);
    }
    private void Start()
    {
       DonutLoop();
        
    }
}
