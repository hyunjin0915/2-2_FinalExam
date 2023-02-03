using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState //게임상태를나타냄
{
    Ready,
    Play
}

public class GameMng : MonoBehaviour
{
    static public GameMng instance; //전역클래스로 사용하기
    public GameObject[] stages; //스테이들 배열
    int curStage = 0; //현재 스테이지 0, 1, 2
    public Text ScoreText; //점수
    public Text StateText; //게임상태

    public static int numCoin; //코인개수
    public int hp; //체력
    public Text HpText; //체력표시

    public GameState gameState = GameState.Ready; //초기설정은ready 로
    
 
    // Start is called before the first frame update
    void Start()
    {
        stages[curStage].SetActive(false); //처음에는 스테이지 비활성화

        if (instance == null)
        {
            instance = this; //전역클래스로사용하기위해instance에할당
        }

     
  
    }
    void Update()
    {

        if(hp<0) //만약체력이 0보다 떨어지면 게임오버화면으로넘어감
        {
            SceneManager.LoadScene("GameOver");
        }

       
    }

    public void GoNextStage()
    {
        StartCoroutine(ChangeStage()); 
    }

    IEnumerator ChangeStage() //스테이지변경
    {
       

        stages[curStage].SetActive(false); //현재스테이지비활성화
        ++curStage; //스테이지 +1 
       

        if(curStage<stages.Length) //마지막 스테이지가 아니라면
        {
            yield return new WaitForSeconds(0.5f); //0.5초 뒤에
            stages[curStage].SetActive(true); //다음스테이지활성화
        } 
        else //마지막스테이지라면
        {
            SceneManager.LoadScene("EscapeMaze"); //탈출씬으로이동
        }
    }

    public void AddCoin() //코인개수늘리고화면하단에표시
    {
        ++numCoin;
   
        ScoreText.text = "High-Score ★ :" + numCoin; 
    }
    public void getHurt() // 몬스터와 충돌시 체력 감소
    {
        hp--;
        HpText.text = "HP★ :" + hp;

    }

    public void OnDetected() //이미지 인식시에만 스테이지 나타나도록 해주는함수
    {
        if(gameState == GameState.Ready) //레디상태일때
        {
            //화면에 정보들 표시
            HpText.text = "HP★ :" + hp; 
            ScoreText.text = "High-Score ★ :" + numCoin;

            gameState = GameState.Play; //플레이중으로상태변경
            stages[curStage].SetActive(true); //스테이지활성화

            StateText.text = "☺"; // 상태 나타내는 메세지 변경
        }
    }
}
