using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx;
    float dy;
    float moveSpeed = 10f;
    [SerializeField] GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dx = Input.acceleration.x * moveSpeed;
        dy = Input.acceleration.y * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, -7.5f, 7.5f));

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dx, dy);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Tilemap_holl")
        {
            //Destroy(this.gameObject);
            gameOverText.SetActive(true);
        }

        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(this.gameObject);
            gameOverText.SetActive(true);
        }

    }

}
