using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetMouseButtonUp(0)) {
            SceneManager.LoadScene("startScene");
        }
    }
}
