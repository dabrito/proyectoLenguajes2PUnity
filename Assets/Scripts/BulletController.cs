using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  private Rigidbody2D myrigidbody2D;
  public float bulletSpeed = 10f;
  public GameManager myGameManager;

  // Start is called before the first frame update
  void Start()
  {
    myrigidbody2D = GetComponent<Rigidbody2D>();
    myGameManager = FindFirstObjectByType<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {
    myrigidbody2D.linearVelocity = new Vector2(bulletSpeed, myrigidbody2D.linearVelocity.y);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Coiun"))
    {
      Destroy(collision.gameObject);
    }
    else if (collision.CompareTag("Enemy"))
    {
      myGameManager.AddScore();
      Destroy(collision.gameObject);
    }
  }
}
