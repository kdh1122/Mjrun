using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hp;
    GameObject attendance;
    // Start is called before the first frame update
    void Start()
    {
        this.hp = GameObject.Find("hp");
        this.attendance = GameObject.Find("attendance");
    }

    public void DecreasHp()
    {
        this.hp.GetComponent<Image>().fillAmount -= 0.34f;
    }

    public void IncreasAttendance()
    {
        this.attendance.GetComponent<Image>().fillAmount += 0.2f;
    }

    public void IncreasHp()
    {
        this.hp.GetComponent<Image>().fillAmount += 0.34f;
    }

    public float hpState()
    {
        return this.hp.GetComponent<Image>().fillAmount;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
