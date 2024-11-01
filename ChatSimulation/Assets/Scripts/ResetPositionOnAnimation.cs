using UnityEngine;

public class ResetPositionOnAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 initialPosition; // �ʱ� ��ġ
    private Quaternion initialRotation; // �ʱ� ȸ�� ��

    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation; // �ʱ� ȸ���� ����
    }

    void Update()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ������ Ʈ������ ���� �ƴ� �� ��ġ�� ȸ�� �ʱ�ȭ
        if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0))
        {
            // Root Motion ����
            animator.applyRootMotion = false;
            ResetTransform(); // ��ġ�� ȸ�� �ʱ�ȭ
        }
        else
        {
            // �ִϸ��̼��� ���� ���̸� Root Motion �ٽ� �ѱ�
            animator.applyRootMotion = true;
        }
    }

    private void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation; // ȸ�� �ʱ�ȭ
    }
}
