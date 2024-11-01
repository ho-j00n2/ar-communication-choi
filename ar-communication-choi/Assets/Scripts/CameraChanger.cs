using UnityEngine;
using UnityEngine.UI;  // UI ��Ҹ� �����ϱ� ���� �߰�

public class CameraChanger : MonoBehaviour
{
    public Transform[] cameraPositions;
    private int currentCameraIndex = 0;
    public float transitionSpeed = 2.0f;

    private bool isTransitioning = false;
    public Button dialogueButton;  // ��ư�� �����ϱ� ���� ����

    void Start()
    {
        // �ʱ� ī�޶� ��ġ�� ȸ�� ����
        transform.position = cameraPositions[currentCameraIndex].position;
        transform.rotation = cameraPositions[currentCameraIndex].rotation;
    }

    void Update()
    {
        // ��ȯ ���� ���� Lerp�� �ε巴�� �̵�
        if (isTransitioning)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[currentCameraIndex].position, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPositions[currentCameraIndex].rotation, Time.deltaTime * transitionSpeed);

            // ��ǥ ��ġ�� �����ϸ� ��ȯ �Ϸ�
            if (Vector3.Distance(transform.position, cameraPositions[currentCameraIndex].position) < 0.01f &&
                Quaternion.Angle(transform.rotation, cameraPositions[currentCameraIndex].rotation) < 0.1f)
            {
                isTransitioning = false;
                dialogueButton.interactable = true;  // ��ư Ȱ��ȭ
                Debug.Log("Camera transition completed.");
            }
        }
    }

    public void ChangeCamera()
    {
        // ��ȯ�� ���۵Ǹ� ��ư�� ��Ȱ��ȭ
        if (!isTransitioning)
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameraPositions.Length;
            isTransitioning = true;
            dialogueButton.interactable = false;  // ��ư ��Ȱ��ȭ
            Debug.Log("Transition working . . . ");
        }
    }
}
