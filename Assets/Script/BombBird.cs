using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : Bird
{
    public bool hasBomb = false;

    public override void OnCollisionEnter2D(Collision2D col)
    {
        string tag = col.gameObject.tag;
        if (col.gameObject.tag == "Obstacle")
        {
            Debug.Log("i hit wood. destroy it(how?)");
        }
        Bomb(col);
    }

    public void Bomb(Collision2D col)
    {
        if (State == BirdState.Thrown && !hasBomb)
        {
            Rigidbody2D wood = col.gameObject.GetComponent<Rigidbody2D>();
            hasBomb = true;

            if (wood)
            {
                //mendapatkan komponen game object bullet
                Collider2D collider = GetComponent<Collider2D>();

                //melakukan pengecekan null variable atau tidak
                if (collider)
                {
                    Destroy(wood.gameObject);
                }
            }
        }
    }
    

}
