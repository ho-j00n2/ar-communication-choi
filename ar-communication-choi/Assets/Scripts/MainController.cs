using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public CameraChanger cameraChanger;
    public TextManager textManager;

    public void OnButtonClick()
    {
        cameraChanger.ChangeCamera();
        textManager.ChangeText();
    }

}
