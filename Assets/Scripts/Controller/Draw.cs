using System.Collections;
using System.IO;
using UnityEngine;


public class Draw : MonoBehaviour
{
    public GameObject Brush;
    public float BrushSize = 0.05f;
    [SerializeField]private GameObject Roller;
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //cast a ray to the plane
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                //instanciate a brush

                var go = Instantiate(Brush, hit.point-Vector3.forward*.01f, Quaternion.Euler(Vector3.zero), transform);
                go.transform.eulerAngles = new Vector3(-90,0,0);
                go.transform.localScale += Vector3.one * BrushSize;
                Roller.transform.position = new Vector3(go.transform.position.x,go.transform.position.y-.5f,Roller.transform.position.z);
              
            }

        }
    }

   
}