using UnityEngine;

public class Player : MonoBehaviour
{
    private bool alive;
    [SerializeField] private Rigidbody2D rb;
    public float gameSpeed;
    public float jumpHeight = 3;
    public float gravityConstant = 9.81f;
    public float gravityMax= 9.81f;
    public Transform camTf;
    public Vector3 startOffset;


    void Awake()
    {
        startOffset = transform.position - camTf.position;
    }

    // Update is called once per frame
    void Update()
    {
        camTf.position = transform.position - startOffset;
        if(Input.GetKeyDown(KeyCode.Space)){Jump();}
    }


    void FixedUpdate()
    {
        if (alive)
        {
            rb.linearVelocity = new Vector2(
                gameSpeed * Time.fixedDeltaTime,
                Mathf.Clamp(rb.linearVelocityY - gravityConstant * Time.fixedDeltaTime, -gravityMax, gravityMax)
            );
        }    
    }

    void Jump()
    {
        if (!alive) return;
        rb.linearVelocity += Vector2.up * jumpHeight;
    }

    public void SetAlive(bool alive)
    {
        this.alive = alive;
        // gameObject.SetActive(alive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "enemy")
        {
            GameManager.Instance.KillPlayer();
        }
    }
}
