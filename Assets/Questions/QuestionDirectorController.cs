using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionDirectorController : MonoBehaviour
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
        quiz_ans.Add("파이썬은 Tim Sort라고 불리는 알고리즘을 사용한다.",true);
        quiz_ans.Add("튜플이 리스트보다 더 많은 메모리를 소모한다.",false);
        quiz_ans.Add("딕셔너리는 순서가 정의되어 있다.", false);
        quiz_ans.Add("컴프리 헨션은 리스트, 딕셔너리, 튜플에서 가능하다.", false);
        quiz_ans.Add("파이썬 3.8에는 딕셔너리와 작동하는 reversed() 함수가 추가되었다.",true);
        quiz_ans.Add("파이썬은 다중 상속을 지원한다.", true);
        quiz_ans.Add("PIP는 Python Interpreter Package의 줄임말이다.", false);
        quiz_ans.Add("파이썬 식별자의 최대 길이는 정해져있다.", false);
        quiz_ans.Add(".pyc 파일은 .py파일의 컴파일된 버전이다.", true);
        quiz_ans.Add("메모리를 관리하는 힙스페이스는 프로그래머가 접근이 불가능하다.", true);
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
