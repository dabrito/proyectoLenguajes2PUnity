using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  public float playerJumpForce = 20f;
  public float playerSpeed = 3f;
  public Sprite[] walkSprites;
  public Sprite jumpSprite;
  private int walkIndex = 0;


  private Rigidbody2D myrigidbody2D;
  private SpriteRenderer mySpriteRenderer;
  public GameObject Bullet;
  public GameManager myGameManager;

  void Start()
  {
    myrigidbody2D = GetComponent<Rigidbody2D>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
    StartCoroutine(WalkCoroutine());
    myGameManager = FindFirstObjectByType<GameManager>();
  }

  void Update()
  {
    float horizontalMovement = 0f;

    if (Input.GetKey(KeyCode.A))
    {
        horizontalMovement = -playerSpeed;
    }
    else if (Input.GetKey(KeyCode.D))
    {
        horizontalMovement = playerSpeed;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      myrigidbody2D.linearVelocity = new Vector2(myrigidbody2D.linearVelocity.x, playerJumpForce);
    }

    myrigidbody2D.linearVelocity = new Vector2(playerSpeed, myrigidbody2D.linearVelocity.y);

    if (Input.GetKeyDown(KeyCode.E))
    {
      Instantiate(Bullet, transform.position, Quaternion.identity);
    }
  }

  IEnumerator WalkCoroutine()
  {
    while (true)
    {
      walkIndex = (walkIndex + 1) % walkSprites.Length;
      mySpriteRenderer.sprite = walkSprites[walkIndex];
      yield return new WaitForSeconds(0.1f);
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Coiun"))
    {
      Destroy(collision.gameObject);
      myGameManager.AddScore();
    }
    else if (collision.CompareTag("Enemy"))
    {
      Destroy(collision.gameObject);
      PlayerDeath();
    }
    else if (collision.CompareTag("DeathZone"))
    {
      PlayerDeath();
    }
  }

  void PlayerDeath()
  {
    SceneManager.LoadScene("Level2D");
  }

}