using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    //    [SerializeField] private TextMeshProUGUI characterText;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI infoMessageText;
    private int currentTextIndex = 0;
    public GameObject canvasInfo;
    [SerializeField] private CameraChanger cameraChanger;  // CameraChanger 참조


    public string[] characterNames =
    {
        "依诺", "宇轩", "浩宇", " "
    };
    public string[] dialogues =
    {
        "你们在走廊上踢球真是太危险了！如果打到人怎么办？打碎玻璃窗怎么办？你们这样吵闹还会影响邻居休息，太没有公德心了！",
        "我们并不是故意的啊，我们只是觉得好玩。",
        "对不起，我们错了。",
        " "
    };
    public string[] infoMessages =
    {
        "然后，他们带着心爱的球一声不响地离开了。让我们一起去看看他们去了哪里吧"
    };
    /*private string[] characterNames =
    {
        "A", "B", "C", "10"
    };
    private string[] dialogues =
    {
        "first text testtest",
        "SSSSSSSSecond text testtest",
        "TTTTTTTTTTThird text testtest",
        "1-0000000000"
    };
    private string[] infoMessages =
    {
        "INNNNNNNNNNNNfo MEssagesekjsekfje"
    };*/
    private void Start()
    {
        canvasInfo.SetActive(false);

    }
    public void ChangeText()
    {
        if (characterNames.Length == 0 || dialogues.Length == 0)
        {
            Debug.LogError("Character names or dialogues are not set!");
            return;
        }

        // 텍스트 업데이트 (현재 인덱스에 해당하는 내용 표시)
        characterText.text = characterNames[currentTextIndex];
        dialogueText.text = dialogues[currentTextIndex];

        // 인덱스를 다음으로 증가시킴
        currentTextIndex = (currentTextIndex + 1) % dialogues.Length;

        // 클릭 횟수가 3번째 이상일 때 infoMessages 표시
        if (currentTextIndex == 0)
        {
            canvasInfo.SetActive(true);
            infoMessageText.text = infoMessages[0];
            return; // 더 이상 진행하지 않음
        }
        
    }

}
