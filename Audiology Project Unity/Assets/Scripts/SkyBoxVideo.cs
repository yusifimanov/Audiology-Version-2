using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class SkyBoxVideo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoClip[] Case1Clip;
    [SerializeField] private VideoClip[] Case1CounselingClip;
    private int _videoClipIndex;
    
    [SerializeField] public Material mainSkyBox;
    [SerializeField] public Material videoSkyBox;
    [SerializeField] public GameObject continueButton;
    [SerializeField] private GameObject _canvasCursor;

    [SerializeField] BackgroundScript _backgroundScript;
    
    public void StartVideo()
    {
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += ContinueButtonEnable;
    }
    public void Start()
    {
        continueButton.SetActive(false);
    }

    private void ChangeToMainSkyBox()
    {
        RenderSettings.skybox = mainSkyBox;
    }
    
    public void ChangeToVideoSkyBox(int index)
    {
        _backgroundScript.SetBackgroundToInactive();
        StateNameController.IsVideoPlaying = true;
        RenderSettings.skybox = videoSkyBox;
        _canvasCursor.SetActive(false);
        if (_videoPlayer.isPlaying == false)
        {
            _videoPlayer.clip = Case1Clip[index];
            StartVideo();
        }
    }

    void ContinueButtonEnable(VideoPlayer vp)
    {
        StateNameController.IsVideoPlaying = false;
        continueButton.SetActive(true);
        _canvasCursor.SetActive(true);
    }

    public void ExitVideoSkybox()
    {
        ChangeToMainSkyBox();
        continueButton.SetActive(false);

        if (StateNameController.CurrentActivePanel.name == "Case1_h_Instruction_02_Demographic")
        {
            // CaseOneHistory.GoToInstruction02();
        }
        
    }
    
}
