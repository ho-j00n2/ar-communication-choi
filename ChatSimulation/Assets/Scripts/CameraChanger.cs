using UnityEngine;
using UnityEngine.UI;  // UI 요소를 제어하기 위해 추가

public class CameraChanger : MonoBehaviour
{
    public Transform[] cameraPositions;
    private int currentCameraIndex = 0;
    public float transitionSpeed = 2.0f;

    private bool isTransitioning = false;
    public Button dialogueButton;  // 버튼을 참조하기 위한 변수

    void Start()
    {
        // 초기 카메라 위치와 회전 설정
        transform.position = cameraPositions[currentCameraIndex].position;
        transform.rotation = cameraPositions[currentCameraIndex].rotation;
    }

    void Update()
    {
        // 전환 중일 때만 Lerp로 부드럽게 이동
        if (isTransitioning)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[currentCameraIndex].position, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPositions[currentCameraIndex].rotation, Time.deltaTime * transitionSpeed);

            // 목표 위치에 도달하면 전환 완료
            if (Vector3.Distance(transform.position, cameraPositions[currentCameraIndex].position) < 0.01f &&
                Quaternion.Angle(transform.rotation, cameraPositions[currentCameraIndex].rotation) < 0.1f)
            {
                isTransitioning = false;
                dialogueButton.interactable = true;  // 버튼 활성화
                Debug.Log("Camera transition completed.");
            }
        }
    }

    public void ChangeCamera()
    {
        // 전환이 시작되면 버튼을 비활성화
        if (!isTransitioning)
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameraPositions.Length;
            isTransitioning = true;
            dialogueButton.interactable = false;  // 버튼 비활성화
            Debug.Log("Transition working . . . ");
        }
    }
}
