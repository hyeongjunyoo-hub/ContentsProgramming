using UnityEngine;

public class CreatePlayerController : MonoBehaviour
{
    // 걷기 기본 속도 (Inspector에서 설정 가능)
    public float moveSpeed = 5.0f;
    
    // 달리기 배율 (기본 속도의 2배로 설정)
    public float sprintMultiplier = 2.0f;

    // 현재 프레임에 실제로 적용될 속도 (걷기 또는 달리기)
    private float currentActualSpeed;
    
    // Animator 컴포넌트 참조
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        
        // 초기 속도를 기본 걷기 속도로 설정
        currentActualSpeed = moveSpeed;

        if (animator != null)
        {
            Debug.Log("Animator 컴포넌트를 찾았습니다!");
        }
        else
        {
            Debug.LogError("Animator 컴포넌트가 없습니다!");
        }
    }
    
    void Update()
    {
        // 1. Shift 키 입력에 따라 실제 속도 결정
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Shift를 누를 때 moveSpeed를 2배로 증가
            currentActualSpeed = moveSpeed * sprintMultiplier;
            Debug.Log("달리기 모드!");
        }
        else
        {
            // Shift를 뗄 때 원래 속도로 복구
            currentActualSpeed = moveSpeed;
        }
        
        // 2. 이동 벡터 계산
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            // X축 스케일만 뒤집고 Y, Z 스케일은 1로 유지
            transform.localScale = new Vector3(-1, 1, 1); 
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            // 원래 스케일로 유지
            transform.localScale = new Vector3(1, 1, 1); 
        }

        // 3. 실제 이동 적용
        if (movement != Vector3.zero)
        {
            // 현재 결정된 속도(currentActualSpeed) 사용
            transform.Translate(movement * currentActualSpeed * Time.deltaTime);
        }

        // 4. 애니메이션 속도 계산 및 전달
        // 이동 중이면 현재 속도(걷기 or 달리기), 아니면 0
        float currentSpeedForAnimator = movement != Vector3.zero ? currentActualSpeed : 0f;

        if (animator != null)
        {
            // Animator의 "Speed" 파라미터에 현재 속도를 전달
            // 속도가 2배로 증가하면 애니메이션도 더 빠르게 재생됩니다.
            animator.SetFloat("Speed", currentSpeedForAnimator);
            Debug.Log("Current Speed: " + currentSpeedForAnimator);
        }
        
        // 5. 점프 입력 (점프는 속도와 별개로 처리)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetBool("isJumping", true);
                Debug.Log("점프!");
            }
        }
    }
}