using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       rb.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exited");
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(rb.linearVelocity.x), 1f);
    }

}
