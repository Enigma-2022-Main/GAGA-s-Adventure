using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollisionManager : MonoBehaviour
{
    public static string FloorType;
    public static bool IsOnCollision = false; // RayCast가 아닌 Collision 처리 (벽 부딫히는 경우를 위함)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FloorType = gameObject.tag;
            //Debug.Log(gameObject.tag);
            IsOnCollision = true;
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        FloorType = "";
        IsOnCollision = false;
    }*/
}
