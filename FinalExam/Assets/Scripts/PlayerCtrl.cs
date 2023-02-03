using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform camTr; //카메라
    public TextMesh infoTM; //정보화면에 나타내기 위한 
    public MeshRenderer PlayerR; //플레이어 색상을 바꿔주기 위한 변수

    public int coin; //코인개수카운트
    public bool power; //아이템 먹었는지 여부 
    public bool hurt; //몬스터와 부딪혔는지 여부 
    Rigidbody rb; //플레이어
    // Start is called before the first frame update
    void Start()
    {
        PlayerR = GetComponent<MeshRenderer>(); 

        rb = GetComponent<Rigidbody>(); //자기자신 가져오기
        SetInfo();  //현재코인개수

        //시작시 두 변수 모두 false로 설정
        power = false; 
        hurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = camTr.up * -0.5f;
        //중력을 카메라 아래쪽으로 변경
 

    }

    public void OnTargetFound()
    {
        gameObject.SetActive(true); //인식되면 오브젝트활성화 
    }
    public void OnTargetLost()
    {
        gameObject.SetActive(false); //인식해제되면비활성화 
    }
    public void OnFound() //카메라가이미지발견하면호출되는함수
    {
        if(rb!=null) //rb에rigidbody할당되면
        {
            rb.isKinematic = false; //dynamic,중력에영향을받음
        }
    }
    public void OnLost()
    {
    if (rb != null)
    {
            rb.isKinematic = true; //중력영향x
    }
   }

    private void OnTriggerEnter(Collider other) //충돌검사함수
    {
        if(other.gameObject.tag== "item") //아이템과충돌시
        {
            power = true; // 파워 true로 변환
            PlayerR.material.color = Color.white; //power가 true인 동안 플레이어 흰색으로 변신
            StartCoroutine(PlayerPower()); //코루틴함수호출
        }
        if (other.gameObject.tag == "Monster" && power == true) //아이템을 먹은 상태에서 몬스터와 충돌시
        {
            Destroy(other.gameObject); //몬스터 제거
            coin++; SetInfo(); //점수 증가
        }
        if (other.gameObject.tag == "Monster" && power == false) //아이템을 안 먹은 상태에서 몬스터와 충돌시
        {
            hurt = true; //hurt 를 ture로 바꿈
            GameMng.instance.getHurt(); //hp 감소시키는 함수호출
            PlayerR.material.color = Color.black; // 플레이어 색상 검정으로 변경
            StartCoroutine(PlayerHurt()); //코루틴함수호출
        }
    }

    private void OnCollisionEnter(Collision collision) //충돌검사
    {
        if(collision.gameObject.tag=="Coin" && hurt == false) //다친상태에서는 코인획득불가
        {
            GameMng.instance.AddCoin(); //코인개수증가함수호출
            Destroy(collision.gameObject); //코인제거

            coin++; SetInfo();
            
        }

        if(collision.gameObject.name=="Exit") //탈출구 도착시
        {
            GameMng.instance.GoNextStage(); //다음스테이지로이동
        }
       
       
    }

    public void SetInfo() // 얻은 코인 개수 표시
    {
        infoTM.text = "★ " + coin;
    }

    IEnumerator PlayerPower() // 5초동안 아이템효과 유지를 위한 코루틴함수
    {
        yield return new WaitForSeconds(5.0f);
        power = false; //5초가 지나면 파워 끄고
        PlayerR.material.color = Color.yellow; //플레리어 색 다시 노란색으로 바꾸기
    }
    IEnumerator PlayerHurt() //5초동안 플레이어색상&&다친상태 유지를 위한 코루틴함수
    {
         yield return new WaitForSeconds(5.0f);
        hurt = false; //5초가 지나면 다친상태 종료
        PlayerR.material.color = Color.yellow; //플레이어 색 다시 노란색으로 바꾸기

    }
}
