using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public bool isFadeIn;
    public GameObject panel;
    public Button targetButton; // 연결할 버튼
    public string sceneName;
    private CanvasGroup canvasGroup;
    private Action onCompleteCallback;

    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // FadeController 오브젝트가 씬 전환 후에도 유지되도록 설정
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트에 등록
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트에서 제거
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 후 panel과 canvasGroup을 다시 찾기
        if (panel == null)
        {
            panel = GameObject.Find("YourPanelName"); // 새 씬에서 panel을 찾음 (패널의 실제 이름으로 변경 필요)
        }
        if (panel != null)
        {
            canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = panel.AddComponent<CanvasGroup>();
            }
        }
    }

    void Start()
    {
        if (!panel)
        {
            Debug.LogError("Panel 오브젝트 찾을 수 없음");
            throw new MissingComponentException();
        }

        // CanvasGroup 컴포넌트가 없으면 추가
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // 버튼의 OnClick에 FadeOutAndLoadScene을 동적으로 추가
        if (targetButton != null)
        {
            targetButton.onClick.RemoveAllListeners(); // 중복 방지를 위해 모든 리스너 제거
            targetButton.onClick.AddListener(() => FadeOutAndLoadScene(sceneName));
        }

        // 초기 설정
        if (isFadeIn)
        {
            panel.SetActive(true);
            canvasGroup.alpha = 1f;
            StartCoroutine(CoFadeIn());
        }
        else
        {
            canvasGroup.alpha = 0f;
            panel.SetActive(false);
        }
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        if (panel == null || canvasGroup == null)
        {
            Debug.LogWarning("패널 또는 CanvasGroup이 존재하지 않음, 페이드 아웃을 수행할 수 없음");
            return;
        }

        panel.SetActive(true);
        canvasGroup.alpha = 0f;
        StartCoroutine(CoFadeOut(() =>
        {
            SceneManager.LoadScene(sceneName); // 씬 전환
            StartCoroutine(CoFadeIn()); // 씬 전환 후 페이드 인 실행
        }));
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;

        // canvasGroup이 유효한지 확인하여 null 방지
        if (canvasGroup == null) yield break;

        while (elapsedTime <= fadeDuration)
        {
            if (canvasGroup == null) yield break; // 씬 전환 중 canvasGroup이 파괴될 경우 종료
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            panel.SetActive(false);
            Debug.Log("Fade In completed.");
        }
    }

    IEnumerator CoFadeOut(Action onComplete)
    {
        float elapsedTime = 0f;

        // canvasGroup이 유효한지 확인하여 null 방지
        if (canvasGroup == null) yield break;

        while (elapsedTime <= fadeDuration)
        {
            if (canvasGroup == null) yield break; // 씬 전환 중 canvasGroup이 파괴될 경우 종료
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            onComplete?.Invoke(); // 페이드 아웃 완료 시 콜백 호출
            Debug.Log("Fade Out completed.");
        }
    }
}
