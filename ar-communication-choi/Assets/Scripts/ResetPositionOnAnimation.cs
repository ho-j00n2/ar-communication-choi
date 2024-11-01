using UnityEngine;

public class ResetPositionOnAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 initialPosition; // 초기 위치
    private Quaternion initialRotation; // 초기 회전 값

    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation; // 초기 회전값 저장
    }

    void Update()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 끝나고 트랜지션 중이 아닐 때 위치와 회전 초기화
        if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0))
        {
            // Root Motion 끄기
            animator.applyRootMotion = false;
            ResetTransform(); // 위치와 회전 초기화
        }
        else
        {
            // 애니메이션이 진행 중이면 Root Motion 다시 켜기
            animator.applyRootMotion = true;
        }
    }

    private void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation; // 회전 초기화
    }
}
