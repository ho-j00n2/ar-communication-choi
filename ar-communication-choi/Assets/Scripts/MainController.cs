using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public CameraChanger cameraChanger;
    public TextManager textManager;
    public FaceController faceController;

    public void OnButtonClick()
    {
        cameraChanger.ChangeCamera();
        textManager.ChangeText();
        faceController.ToggleTalk();
    }

}
