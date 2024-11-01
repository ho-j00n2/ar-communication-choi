using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public SkinnedMeshRenderer Renderer;

    public int MouthIndex;
    public int RightEyeTopIndex;
    public int RightEyeBottomIndex;
    public int LeftEyeTopIndex;
    public int LeftEyeBottomIndex;

    float MouthMaxWeight = 30f;
    public float MouthMaxWeightMin = 30f;
    public float MouthMaxWeightMax = 30f;
    public float EyeTopMaxWeight = 70f;
    public float EyeBottomMaxWeight = 40f;

    float blinkInterval = 20f;
    float blinkTimer = 0f;
    float mouthDt = 0f;
    float talkDt = 0f;
    float blinkDt = 0f;
    public float BlinkIntervalMin = 15f;
    public float BlinkIntervalMax = 45f;
    public float TalkLength = 3f;
    public float MouthOpenLength = 0.25f;
    public float BlinkLength = 0.25f;

    bool eyeClosing = true;
    bool mouthClosing = true;
    bool isTalking = false;
    bool isBlinking = false;


    // Start is called before the first frame update
    void Start()
    {
        //if no renderer specified, try to get the face on this game object
        if(Renderer == null)
        {
            Renderer = GetComponent<SkinnedMeshRenderer>();
        }

        //get first random numbers
        changeBlinkInterval();
        changeMouthMaxOpenWeight();
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer += Time.deltaTime;

        //blink at the next interval
        if (blinkTimer > blinkInterval)
        {
            ToggleBlinking();
            blinkTimer = 0f;
        }

        talkUpdate(); //mouth controls
        blinkUpdate();//eye controls

        //test talk button
        if (Input.GetKeyDown(KeyCode.T) && !isTalking)
        {
            ToggleTalk();
        }
    }

    //call when want to talk
    public void ToggleTalk()
    {
        isTalking = !isTalking;

        //reset mouth position
        mouthClosing = true;
        Renderer.SetBlendShapeWeight(MouthIndex, 0f);
    }

    public void ToggleBlinking()
    {
        if (!isBlinking)
        {
            changeBlinkInterval();
        }

        //reset eye position
        Renderer.SetBlendShapeWeight(RightEyeTopIndex, 0f);
        Renderer.SetBlendShapeWeight(RightEyeBottomIndex, 0f);
        Renderer.SetBlendShapeWeight(LeftEyeTopIndex, 0f);
        Renderer.SetBlendShapeWeight(LeftEyeTopIndex, 0f);

        isBlinking = !isBlinking;
    }

    void talkUpdate()
    {
        if(isTalking)
        {
            talkDt += Time.deltaTime;
            mouthDt += Time.deltaTime;

            Renderer.SetBlendShapeWeight(MouthIndex, mouthClosing?Mathf.Lerp(0f, MouthMaxWeight, mouthDt / MouthOpenLength): 
                                                                  Mathf.Lerp(MouthMaxWeight, 0f, mouthDt / MouthOpenLength));

            if(mouthDt > MouthOpenLength)
            {
                mouthDt = 0f;
                mouthClosing = !mouthClosing;

                //mouth closed, get next mouth openness
                if(mouthClosing)
                {
                    changeMouthMaxOpenWeight();
                }
            }

            if (talkDt > TalkLength)
            {
                ToggleTalk();
                talkDt = 0f;
                mouthDt = 0f;
            }
        }
    }

    void blinkUpdate()
    {
        if(isBlinking)
        {
            blinkDt += Time.deltaTime;

            Renderer.SetBlendShapeWeight(RightEyeTopIndex, eyeClosing ? Mathf.Lerp(0f, EyeTopMaxWeight, blinkDt / BlinkLength) :
                                                                        Mathf.Lerp(EyeTopMaxWeight, 0f, blinkDt / BlinkLength));
            Renderer.SetBlendShapeWeight(RightEyeBottomIndex, eyeClosing ? Mathf.Lerp(0f, EyeBottomMaxWeight, blinkDt / BlinkLength) :
                                                                           Mathf.Lerp(EyeBottomMaxWeight, 0f, blinkDt / BlinkLength));
            Renderer.SetBlendShapeWeight(LeftEyeTopIndex, eyeClosing ? Mathf.Lerp(0f, EyeTopMaxWeight, blinkDt / BlinkLength) :
                                                                       Mathf.Lerp(EyeTopMaxWeight, 0f, blinkDt / BlinkLength));
            Renderer.SetBlendShapeWeight(LeftEyeBottomIndex, eyeClosing ? Mathf.Lerp(0f, EyeBottomMaxWeight, blinkDt / BlinkLength) :
                                                                       Mathf.Lerp(EyeBottomMaxWeight, 0f, blinkDt / BlinkLength));

            if (blinkDt > BlinkLength)
            {
                //if eye is fully closed, open it up. if eye fully open end blink animation
                if(eyeClosing)
                {
                    eyeClosing = false;
                    blinkDt = 0f;
                }
                else
                {
                    blinkDt = 0f;
                    ToggleBlinking();
                    eyeClosing = true;
                }
            }
        }
    }

    //change blink interval to random between the specified ranges
    void changeBlinkInterval()
    {
        //get next blinking interval
        blinkInterval = Random.Range(BlinkIntervalMin, BlinkIntervalMax);
    }

    //change mouth max openness to random between the specified ranges
    void changeMouthMaxOpenWeight()
    {
        //get random mouth openness
        MouthMaxWeight = Random.Range(MouthMaxWeightMin, MouthMaxWeightMax);
    }
}
