using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    //플레이어 위 코인개수 나타내주는 텍스트가 항상 카메라를 바라보게해주는 스크립트
    public Transform camTr;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    { 
        //항상 카메라를 바라보게 설정
        tr.LookAt(camTr.position);
    }
}
