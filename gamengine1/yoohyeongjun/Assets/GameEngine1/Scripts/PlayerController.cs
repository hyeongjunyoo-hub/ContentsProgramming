using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    
    [Header("점프 설정")]
    public float jumpForce = 10.0f;
    
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int score = 0;  // 점수 추가
    private Vector3 startPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //게임 시작 시 위치를 저장 - 새로 추가;
        startPosition = transform.position;
        Debug.Log("시작 위치 저장: " + startPosition);
    }
    
    void Update()
    {
        // 좌우 이동
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        // 점프 (지난 시간에 배운 내용)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    
    // 바닥 충돌 감지 (Collision)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("장애물 충돌! 시작지점으로 돌아갑니다.");
            transform.position = startPosition;
            rb.linearVelocity = new Vector2(0,0);
        }

    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // 아이템 수집 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;  // 점수 증가
            Debug.Log("코인 획득! 현재 점수: " + score);
            Destroy(other.gameObject);  // 코인 제거
        }
    }
    
}