using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverContoller : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool state;
    float timer;
    int waitingTime;
    

    void Start()
    {
        //state = false;
        timer = 0;
        waitingTime = 2;   

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > waitingTime) {
            this.gameObject.layer = 4;
            //state = true;
        }

        // 게임씬 이름으로 바꿔야 함
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetMouseButtonUp(0)) {
            SceneManager.LoadScene("startScene");
        }
    }
}
