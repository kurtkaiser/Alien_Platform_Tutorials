using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private int moveSpeed = 6;
    private Vector3 movement;
    private float jumpHeight = 9f;
    public bool onPlatform = true;
    public Sprite[] sprites;
    public Text scoreText;
    public Text healthText;
    public float yForce = 0;
    public bool takingDamange = false;
    public int killCount;
    

    private int score = 0;

    Rigidbody2D rigidPlayer;

    void Update()
    {
        Jump();

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }

    void FixedUpdate()
    {
        if (!onPlatform)
        {
            yForce = yForce - 0.025f;

        }
        movement = new Vector3(Input.GetAxis("Horizontal"), yForce, 0f);
        transform.position = transform.position + movement *
            moveSpeed * Time.deltaTime;
        if (transform.position.y < -10)
        {
            gameOver();
        }
    }


    void Jump()
    {

        if (Input.GetButtonDown("Jump") && onPlatform)
        {
            rigidPlayer.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !takingDamange)
        {
            if(healthText.text.Length == 1)
            {
                gameOver();
            }
            else
            {
                StartCoroutine(TakeDamage());
            }
        }
        else if(collision.collider.tag == "Coin")
        {
            Destroy(collision.gameObject);
            score = score + 1;
            scoreText.text = score.ToString();
        }
        else if (collision.collider.name == "EndPortal")
        {
            if(killCount > 5 && int.Parse(scoreText.text) > 7)
            {
                healthText.text = "★♛★♛★♛★ YOU WIN!! ★♛★♛★♛★";
            }
            else if (int.Parse(scoreText.text) > 7)
            {
                healthText.fontSize = 30;
                healthText.text = "★$★ YOU FOUND ALL THE FOOD!! ★$★";
                GameObject.Find("Background").GetComponent<SpriteRenderer>().color = Color.green;
            } else if (killCount > 5)
            {
                healthText.fontSize = 30;
                healthText.text = "★✪★ YOU ELIMINATED ALL THE ENEMIES!! ★✪★";
                GameObject.Find("Background").GetComponent<SpriteRenderer>().color = Color.blue;
            } else
            {
                healthText.text = "★♛★ YOU FINISHED!! ★♛★";
                GameObject.Find("Background").GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }

    IEnumerator TakeDamage()
    {
        takingDamange = true;
        healthText.text = healthText.text.Substring(0, healthText.text.Length - 1);
        for (int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(3f);
        takingDamange = false;
    }

    private void gameOver()
    {
        gameObject.transform.GetChild(0).gameObject
            .GetComponent<Collider2D>().enabled = false;
        healthText.text = "☠☠☠ Game Over!! ☠☠☠";
        healthText.fontSize = 55;
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<SpriteRenderer>().flipY = true;
        GetComponent<Collider2D>().enabled = false;
        rigidPlayer.gravityScale = 2;
        GameObject.Find("Background").GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator HidePlayer()
    {
        yield return new WaitForSeconds(3.0f);
        rigidPlayer.gravityScale = 0;
        rigidPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
    }

        private void Awake()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
    }
}
