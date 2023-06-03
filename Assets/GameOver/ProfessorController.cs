using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    float walkForce = -0.04f;
    
    void kick() {
        this.animator = GetComponent<Animator>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.walkForce, 0, 0);

        float p1 = transform.position.x; //교수의 중심 좌표
        if (p1 < 0) { 
            this.walkForce = 0;
            Invoke("kick", 4f);
        }
    }
}
