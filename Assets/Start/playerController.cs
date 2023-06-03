using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 15f;


    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    int run=1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
        {
            if(run==1){
                Vector3 moveVelocity = Vector3.zero;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                anim.SetBool("isRun", true);
                transform.position += moveVelocity * movePower * Time.deltaTime;
            }
            else if(run==2){
                Jump();
                run+=1;
            }
        }

    void Jump()
    {
        anim.SetBool("isRun", false);
        rb.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Flag"){
            run+=1;
        }
    }
}
