using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectScript : MonoBehaviour
{
    GameObject Enemy;
    public int killCount = 0;
    public GameObject enemyFeet;

    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "OnGround")
        {
            //StartCoroutine(DamageOff());
            GameObject.Find("Player").GetComponent<PlayerScript>().takingDamange = true;
            Enemy.tag = "Finish";
            GetComponent<Collider2D>().enabled = false;
            enemyFeet.GetComponent<Collider2D>().enabled = false;
            Enemy.GetComponent<SpriteRenderer>().flipY = true;
            Enemy.GetComponent<Collider2D>().enabled = false;
            Enemy.GetComponent<SpriteRenderer>().color = Color.red;
            // Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(-40, 40), 0f);
            // Enemy.transform.position += movement * Time.deltaTime;
            Enemy.GetComponent<Rigidbody2D>().gravityScale = 5;
            GameObject.Find("Player").GetComponent<PlayerScript>().killCount++;
            GameObject.Find("Player").GetComponent<PlayerScript>().takingDamange = false;
        }
    }

}
