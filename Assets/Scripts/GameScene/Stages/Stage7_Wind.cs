using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7_Wind : MonoBehaviour
{
    private Rigidbody2D playerRigid2D;

    public static bool WindDirection = false; //false면 왼쪽, true면 오른쪽
    public static bool ChangeDirection = false;

    [SerializeField]
    private GameObject windParticle1;
    [SerializeField]
    private GameObject windParticle2;

    private void Start()
    {
        playerRigid2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        windParticle1.transform.rotation = Quaternion.Euler(0, -90, 0);
        windParticle2.transform.rotation = Quaternion.Euler(0, -90, 0);
        StartCoroutine(Wind());
    }

    IEnumerator Wind()
    {
        int count = 0;
        //바람 구현
        while (true)
        {
            if (GameStageManager.PlayerStageCounter == 6)
            {
                if (count < 750)
                {
                    //왼쪽
                    playerRigid2D.transform.Translate(new Vector2(-0.015f, 0));
                }
                if (count >= 750 && count < 1500)
                {
                    //오른쪽
                    playerRigid2D.transform.Translate(new Vector2(0.015f, 0));
                }
            }
            if (count == 750)
            {
                windParticle1.transform.rotation = Quaternion.Euler(0, 90, 0);
                windParticle2.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (count == 1500)
            {
                count = 0;
                windParticle1.transform.rotation = Quaternion.Euler(0, -90, 0);
                windParticle2.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            count += 2;
            yield return new WaitForFixedUpdate();
        }
    }
}
