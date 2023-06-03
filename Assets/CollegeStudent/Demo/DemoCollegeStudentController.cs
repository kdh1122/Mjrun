using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {

        private CapsuleCollider2D[] col;
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 25f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        private int jumpCount = 0;
        bool isJumping = false;
        bool isSliding = false;
        private bool alive = true;
        private bool isKickboard = false;
        private bool getBook = false;
        private bool sojuState = false;
        private int attendanceCount = 0;
        private bool hitState = false;
        float now = 0.0f;
        GameObject quizDirector;
        AudioSource[] audioSource;


        // Start is called before the first frame update
        void Start()
        {
            col = GetComponents<CapsuleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            quizDirector = GameObject.Find("QuizDirector");
            audioSource = GetComponents<AudioSource>();
        }

        private void Update()
        {
            if (alive)
            {
                managerCheat();
                Run();
                Jump();
                Slide();
                KickBoard();
                Over();
            }

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag == "Ground")
            {
                isJumping = false;
                jumpCount = 0;
                anim.SetBool("isJump", false);
                Run();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if(other.gameObject.tag == "Attendance")
            {
                GameObject director = GameObject.Find("GameDirector");
                attendanceCount+=1;
                director.GetComponent<GameDirector>().IncreasAttendance();
                other.GetComponent<AttendanceController>().destroyAttendance();
                other.GetComponent<AudioSource>().Play();
            }
            
            if(other.gameObject.tag == "Hurdle")
            {
                if(isKickboard){
                    other.GetComponent<HurdleController>().Out();
                }
                if(!hitState && !isKickboard)
                {
                    hitState=true;
                    GameObject director = GameObject.Find("GameDirector");
                    director.GetComponent<GameDirector>().DecreasHp();
                    Hurt();
                    if(director.GetComponent<GameDirector>().hpState()==0){
                        Die();
                    }
                    Invoke("setHitState",1f);
                }
            }

            if(other.gameObject.tag == "Book")
            {
                other.GetComponent<BookController>().destroyBook();
                getBook = true;
                other.GetComponent<AudioSource>().Play();
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isSlide", false);
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
                KickBoard();
            }

            if(other.gameObject.tag == "Posion")
            {
                GameObject director = GameObject.Find("GameDirector");
                other.GetComponent<AudioSource>().Play();
                director.GetComponent<GameDirector>().IncreasHp();
                other.GetComponent<PosionController>().destroyPosion();
            }

            if(other.gameObject.tag == "Soju")
            {
                GameObject director = GameObject.Find("GameDirector");
                other.GetComponent<SojuController>().destroySoju();
                sojuState = true;
                col[0].enabled = false;
                col[1].enabled = true;
                anim.SetBool("isSlide", false);
                isSliding = false;
                movePower = 10f;
                Run();
                Invoke("setSojuState", 3f);
            }

            if(other.gameObject.tag == "AnswerO")
            {
                if(SceneManager.GetActiveScene().name == "Stage1")
                {
                    bool check1 = quizDirector.GetComponent<QuestionDirectorController>().compareAns(true);
                    nextStage(check1);
                }
                if(SceneManager.GetActiveScene().name == "Stage2")
                {
                    bool check1 = quizDirector.GetComponent<QuestionDirectorController2>().compareAns(true);
                    nextStage(check1);
                }
            }

            if(other.gameObject.tag == "AnswerX")
            {
                if(SceneManager.GetActiveScene().name == "Stage1")
                {
                    bool check2 = quizDirector.GetComponent<QuestionDirectorController>().compareAns(false);
                    nextStage(check2);
                }

                if(SceneManager.GetActiveScene().name == "Stage2")
                {
                    bool check2 = quizDirector.GetComponent<QuestionDirectorController2>().compareAns(false);
                    nextStage(check2);
                }
            }        
        }

        void KickBoard()
        {
            if(getBook)
            {
                now += Time.deltaTime;
                if(now>4.0f)
                {
                    isKickboard = false;
                    anim.SetBool("isKickBoard", false);
                    now = 0.0f;
                    getBook = false;
                    anim.SetBool("isRun", true);
                }
            }
        }

        void managerCheat()
        {
            if(!isKickboard && Input.GetKeyDown(KeyCode.Alpha4))
            {
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isSlide", false);
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }
            
            else if(isKickboard && Input.GetKeyDown(KeyCode.Alpha4))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetBool("isRun", true);
            }
        }
        

        void Run()
        {
            if (!isKickboard && isJumping == false && isSliding == false)
            {
                Vector3 moveVelocity = Vector3.zero;
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                anim.SetBool("isRun", true);
                transform.position += moveVelocity * movePower * Time.deltaTime;
            }
    
            if (isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
        }

        void Jump()
        {
            if ((Input.GetKeyDown(KeyCode.Space))&& jumpCount<2 && !sojuState && !isSliding)
            {
                isJumping = true;
                jumpCount += 1;
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", true);
                rb.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(10, jumpPower);
                rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                audioSource[0].Play();
            }

        }

        void Slide()
        {
            if(Input.GetKey(KeyCode.DownArrow) && !sojuState && !isJumping &&!isKickboard)
            {
                isSliding = true;
                col[0].enabled = true;
                col[1].enabled = false;
                Vector3 moveVelocity = Vector3.zero;
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                movePower = 15f;
                anim.SetBool("isRun", false);
                anim.SetBool("isSlide", true);
                transform.position += moveVelocity * movePower * Time.deltaTime;
                audioSource[0].Play();
            }

            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                col[0].enabled = false;
                col[1].enabled = true;
                anim.SetBool("isSlide", false);
                isSliding = false;
                movePower = 10f;
                anim.SetBool("isRun", true);
            }
        }

        void Hurt()
        {
            anim.SetTrigger("hurt");
            audioSource[1].Play();
            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        }

        void Die()
        {
            isKickboard = false;
            anim.SetBool("isKickBoard", false);
            anim.SetTrigger("die");
            Invoke("changeOverScene", 2f);
            alive = false;
            audioSource[2].Play();
        }

        void Over()
        {
            if (transform.position.x >=521f && attendanceCount<=3)
            {
                Die();
            }
        }


        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetBool("isSlide", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }

        private void nextStage(bool check)
        {
            if(check)
            {
                if(SceneManager.GetActiveScene().name == "Stage1")
                {
                    SceneManager.LoadScene("Stage2");
                }
                if(SceneManager.GetActiveScene().name == "Stage2")
                {
                    SceneManager.LoadScene("ClearScene");
                }
            }
            else
            {
                SceneManager.LoadScene("OverScene");
            }
        }

        private void setSojuState()
        {
            sojuState = false;
        }

        private void setHitState()
        {
            hitState = false;
        }

        private void changeOverScene()
        {
            SceneManager.LoadScene("OverScene");
        }
    }
}
