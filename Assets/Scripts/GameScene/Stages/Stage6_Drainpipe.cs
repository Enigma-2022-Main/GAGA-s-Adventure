using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_Drainpipe : MonoBehaviour
{
    public static int DrainpipeCount = 0;
    
    private Rigidbody2D playerRigid2D;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private AnimationCurve curve1; //배수관 입장

    [SerializeField] private float duration; //애니메이션 길이

    private int animationCounter = 0;

    private void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            DrainpipeCount++;

            if (PlayerController.PlayerPosition.y < 120.7f)
            {
                animationCounter = 2;
                StartCoroutine(DrainPipe_2());
            }
            else
            {
                animationCounter = 1;
                StartCoroutine(DrainPipe_1());
            }
        }
    }

    IEnumerator DrainPipe_1()
    {
        while (animationCounter == 1)
        {
            //Debug.Log(PlayerController.PlayerPosition.x);
            /*if (PlayerController.PlayerPosition.x > 9f)
            {
                PlayerController.ForceLeft = true;
                PlayerController.Stuck = true;
            }*/

            PlayerController.Stuck = true;
            PlayerController.ForceLeft = true;
            if (PlayerController.PlayerPosition.x <= 9f)
            {
                PlayerController.ForceLeft = false;
                animationCounter = 2;
                StartCoroutine(DrainPipe_2());
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DrainPipe_2()
    {
        float count = 0.0f;
        boxCollider2D.isTrigger = true;

        while (animationCounter == 2)
        {
            if (PlayerController.PlayerPosition.x <= 11.4f)
            {
                PlayerController.ForceRight = true;
            }
            else
            {
                spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, 0);
                playerRigid2D.position = new Vector2(21.2f, playerRigid2D.position.y);
                PlayerController.ForceRight = false;
                if (count < 1f)
                {
                    count += 0.005f;
                    playerRigid2D.gravityScale = 0f;
                    playerRigid2D.position = Vector2.Lerp(playerRigid2D.position, new Vector2(playerRigid2D.position.x, 126.7f), curve1.Evaluate(count / 12f));
                }
                else
                {
                    playerRigid2D.gravityScale = 2f;
                    animationCounter = 3;
                    StartCoroutine(DrainPipe_3());
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator DrainPipe_3()
    {
        while (animationCounter == 3)
        {
            if (PlayerController.PlayerPosition.y < 21f)
            {
                spriteRenderer.color = new Color(0xFF, 0xFF, 0xFF, 0xFF);

                playerRigid2D.position = new Vector2(10.87f, 21f);
                playerRigid2D.velocity = Vector2.left * new Vector2(5f, 0f);
                PlayerController.Stuck = false;
                boxCollider2D.isTrigger = false;
                animationCounter = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

}
