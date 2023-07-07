using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_BroomLeft : MonoBehaviour
{
    public float SweepLength = 22f;
    public float BroomPower = 20f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid2D;
    private Vector2 broomPosition;
    private GameObject player;
    private Rigidbody2D playerRigid2D;
    private float deltaX;
    [SerializeField] private AnimationCurve curve;
    private Animator animator;

    public static bool IsSweeped = false;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        broomPosition = rigid2D.position;
        player = GameObject.FindWithTag("Player");
        playerRigid2D = player.GetComponent<Rigidbody2D>();
        spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, 1);
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine(SweepBroomToLeft());

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsSweeped = true;
        }
    }

    IEnumerator SweepBroomToLeft()
    {
        int count = 0;
        while (true)
        {
            if (count == 0) // 0초째
            {
                rigid2D.position = broomPosition;
                rigid2D.velocity = Vector2.zero;
                //Debug.Log("FadeIn");
                StartCoroutine(FadeIn());
            }

            if (count == 60) // 0.6초째
            {
                animator.SetBool("IsMoving", true);
                StartCoroutine(MoveBroom());
            }


            if (count == 140) // 1.4초쨰
            {
                //Debug.Log("FadeOut");
                StartCoroutine(FadeOut());
            }
            if (count == 160) // 1.6초째
            {
                animator.SetBool("IsMoving", false);
            }

            if (count == 200) // 2초째
            {
                rigid2D.position = new Vector2(0, -50);
            }

            //Debug.Log(IsSweeped);
            if (IsSweeped && animator.GetBool("IsMoving"))
            {
                deltaX = playerRigid2D.position.x - rigid2D.position.x;
                StartCoroutine(SweepPlayer());
                IsSweeped = false;
            }

            count += 2;
            if (count == 350) // 3.5sec
            {
                count = 0;
            }
            //Debug.Log("SweepBroom: " + count);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator MoveBroom()
    {
        float count = 0.0f;
        float duration = 0.8f;

        while (count < duration)
        {
            rigid2D.position = Vector2.Lerp(broomPosition, broomPosition + new Vector2(-1 * SweepLength, 0), curve.Evaluate(count / duration));
            count += 0.02f;

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator SweepPlayer()
    {
        int count = 0;
        int duration = 9; //0.09초: 0.07초 이동, 0.02초 가속

        while (count < duration)
        {
            if (count < duration - 3)
            {
                playerRigid2D.position = new Vector2(rigid2D.position.x + deltaX, playerRigid2D.position.y);
            }
            else
            {
                //playerRigid2D.AddForce(Vector2.right * 75f, ForceMode2D.Impulse);
                playerRigid2D.velocity += new Vector2(-1 * BroomPower, 0);
            }

            count += 2;

            //Debug.Log("BroomLeft: " + playerRigid2D.velocity);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator FadeIn()
    {
        float fadeCount = 0;
        spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.05f;
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        }
    }

    IEnumerator FadeOut()
    {
        float fadeCount = 1;
        spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.05f;
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, fadeCount);
        }
    }
}
