using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [SerializeField] private List<Transform> AIs;
    [SerializeField] private Transform Player;
    [SerializeField] private Text score;
  
    private int Sorted()
    {
        int count = 1;
        for (int i = 0; i < AIs.Count-1; i++)
        {
            if (Player.transform.position.z < AIs[i].transform.position.z)
            {
                count++;
            }
        }
        return count;
    }
    private void Update()
    {
        score.text = Sorted().ToString();
    }


}

