using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentContoller : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid2D;
    float jumpForce = 680.0f;
    float fly = -580.0f;
    float rotSpeed = 0;

    void kicked() {
        this.rigid2D.AddForce(transform.up * this.jumpForce);
        this.rigid2D.AddForce(transform.right * this.fly);
        this.rotSpeed = 10;
        GetComponent<AudioSource>().Play();
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        Invoke("kicked", 4.8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, this.rotSpeed);
    }
    /*
    void OnTriggerEnter2D(Collider2D other) {
        this.rigid2D.AddForce(transform.up * this.jumpForce);
        this.rigid2D.AddForce(transform.right * this.fly);
        this.rotSpeed = 10;
        GetComponent<AudioSource>().Play();
    }
    */
}
