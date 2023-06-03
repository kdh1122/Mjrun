using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionDirectorController2 : MonoBehaviour
{
    Dictionary<string, bool> quiz_ans;
    string quiz = null;
    GameObject player;
    GameObject quizText;
    float seedtime;
    // Start is called before the first frame update
    void Start()
    {
        quiz_ans = new Dictionary<string, bool>();
        this.player = GameObject.Find("CollegeStudent");
        this.quizText = GameObject.Find("quiz");
        seedtime = Time.time * 100f;
        AddQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.quiz == null && player.transform.position.x >= 520)
        {
            string q = PickQuiz();
            this.quizText.GetComponent<Text>().text=q;
        }
    }
    private void AddQuiz()
    {
        quiz_ans.Add("DNS란 Host의 Domain Name을 Host의 IP로 변환해주는 서비스이다.",true);
        quiz_ans.Add("TCP의 3Way-HandShake는 세션을 종료하기 위한 절차이다.", false);
        quiz_ans.Add("HTTP는 상태정보를 저장하는 특징이 있다.", false);
        quiz_ans.Add("UDP는 3Way-HandShake를 사용하지 않는다.", true);
        quiz_ans.Add("HTTP의 Method에서 PATCH는 일부 데이터만 변경한다.",true);
        quiz_ans.Add("HTTP의 Method에서 POST는 멱등하다", false);
        quiz_ans.Add("HTTP의 상태코드 400번대는 클라이언트 오류이다.", true);
        quiz_ans.Add("Javascript는 멀티 스레드 언어이다.", false);
        quiz_ans.Add("HTTP는 중요한 프라이버시가 오가는 서비스에 부적합하다.", true);
        quiz_ans.Add("Accept는 HTTP의 Response Header이다", false);
    }

    public void Erase()
    {
        this.quiz = null;
    }

    public string PickQuiz()
    {
        List<string> keyList = new List<string>(quiz_ans.Keys);
        Random.InitState((int)seedtime);
        this.quiz = keyList[Random.Range(0,keyList.Count)];
        return this.quiz;
    }

    public bool compareAns(bool p_ans)
    {
        bool answer = quiz_ans[this.quiz];
        if(answer == p_ans)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
