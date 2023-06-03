using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleController : MonoBehaviour
{
    private bool isConflict = false;
    private Rigidbody2D rb;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isConflict)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(200,100,0));
            transform.Rotate(new Vector3(0,0,90) * Time.deltaTime * 10);
            rb.gravityScale = 10;
            time += Time.deltaTime;
        }

        if(time >= 3f){
            Destroy(gameObject);
        }
    }

    public void Out()
    {
        isConflict=true;   
    }
}
