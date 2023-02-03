using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    public GameObject monster;
    int a = 1;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
      if(transform.localPosition.z<-0.5f)
        {
            a = -1;
        }
      else if (transform.localPosition.z> 0.5f)
      {
          a = 1;
      }
        transform.Translate(Vector3.forward* 0.3f * Time.deltaTime * a);
    }

  
}
