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

    private Coroutine walkCoroutine; 

    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myGameManager = FindFirstObjectByType<GameManager>();
        myrigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        

    }

    void Update()
    {
        float horizontalMovement = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalMovement = -playerSpeed;
            transform.localScale = new Vector3(-1, 1, 1); 
            StartWalkingAnimation(); 
        }
        // Salto del jugador
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myrigidbody2D.linearVelocity = new Vector2(myrigidbody2D.linearVelocity.x, playerJumpForce);
            mySpriteRenderer.sprite = jumpSprite;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalMovement = playerSpeed;
            transform.localScale = new Vector3(1, 1, 1); 
            StartWalkingAnimation(); 
        }
        else
        {
            StopWalkingAnimation(); 
        }

        myrigidbody2D.linearVelocity = new Vector2(horizontalMovement, myrigidbody2D.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myrigidbody2D.linearVelocity = new Vector2(myrigidbody2D.linearVelocity.x, playerJumpForce);
        }

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

    void StartWalkingAnimation()
    {
        if (walkCoroutine == null)
        {
            walkCoroutine = StartCoroutine(WalkCoroutine());
        }
    }

    void StopWalkingAnimation()
    {
        if (walkCoroutine != null)
        {
            StopCoroutine(walkCoroutine);
            walkCoroutine = null;
            mySpriteRenderer.sprite = walkSprites[0]; 
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
        else if (collision.CompareTag("FinishLine")){
            PlayerWins();


        }
    }

    void PlayerWins(){
        SceneManager.LoadScene("Final");

    }

    void PlayerDeath()
    {
        SceneManager.LoadScene("Level 3");
    }
}
