using UnityEngine;

public class HelloUnity : MonoBehaviour
{
    // 변수 선언 (데이터를 담을 상자 만들기)
    private string playerName = "김철수";
    public int playerLevel = 1;
    public float walkSpeed = 5.0f;
    public bool canFly = false;
    
    void Start()
    {
        Debug.Log("=== 플레이어 정보 ===");
        Debug.Log("이름: " + playerName);
        Debug.Log("레벨: " + playerLevel);
        Debug.Log("이동 속도: " + walkSpeed + " m/s");
        
        if (canFly)
        {
            Debug.Log(playerName + "님은 날 수 있습니다!");
        }
        else
        {
            Debug.Log(playerName + "님은 날 수 없습니다.");
        }
    }
}
