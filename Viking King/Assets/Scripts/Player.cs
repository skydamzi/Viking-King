using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> itemNames;
    public Rigidbody2D rb;
    public int jumpCount;
    public AudioSource audioSource;
    public Animator animator;
    public GameObject prefab_HP;
    public GameObject canvas;
    public RectTransform HP;

    private void Start()
    {
        HP = Instantiate(prefab_HP,canvas.transform).GetComponent<RectTransform>();
    }

    private void Update()
    {
        Movement();
        moveHPbar();
        if (Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("isRuning", false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpCount < 2)
            {
                rb.velocity = new Vector2(0,0);
                rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
                jumpCount++;
                audioSource.Play();
                animator.SetBool("isJumping", true);
            }
        }

    }

    void moveHPbar()
    {
        Vector3 hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.55f, 0));
        HP.position = hpbarPos;
    }
    void Movement()
    {
        /* 방법1.Axis활용
            float hori = Input.GetAxis("Horizontal");
            this.transform.position += new Vector3(hori, 0, 0) * Time.deltaTime * 6f;
        */

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 6f;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isRuning", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 6f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isRuning", true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }

        if (collision.gameObject.CompareTag("item"))
        {
            itemNames.Add(collision.gameObject.name);

            if(itemNames.Count == 2)
            {
                if(GM.instance.eProgress == GM.Progress.퀘스트받음_수행X)
                {
                    GM.instance.eProgress = GM.Progress.퀘스트받음_수행O;
                }
            }

        }
    }
}
