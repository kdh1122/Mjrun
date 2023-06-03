using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class info2Director : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void preScene(){
        SceneManager.LoadScene("startScene");
    }
    public void preScene2(){
        SceneManager.LoadScene("infoScene");
    }
}
