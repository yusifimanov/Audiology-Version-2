using System;
using System.Collections;
using System.Threading.Tasks;
using Michsky.MUIP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float TotalTime = 2f;
    bool _gazedStatus;
    
    // public GameObject button;
    public Slider cursorTimer;

    private Image _image;
    private UIGradient _uiGradient;
    private Animator _animatorButton;
    
    public bool enableButtonSound = true;
    public bool isInteractable = true;
    private bool _isClick;
    
    private GameObject _continueButton;
    
    public float gazedTimer;
    
    public AudioSource soundSource;
    public AudioClip clickClip;
    
    public UnityEvent GVRClick;


    void Start()
    {
        _continueButton = GameObject.Find("ContinueButton");
        cursorTimer.value = 0;
        _gazedStatus = false;
        _animatorButton = GetComponent<Animator>();
        _image = GetComponent<Image>();
        _uiGradient = GetComponent<UIGradient>();
    }
    
    void Update()
    {
        if (isInteractable == false)
        {
            return;
        }

        
        // Hovering 
        if (_gazedStatus)
        {
            _animatorButton.StopPlayback();
            gazedTimer += Time.deltaTime;
            cursorTimer.value = gazedTimer / TotalTime;
            Hovering();
        }
        
        // Selecting 
        if (gazedTimer > TotalTime)
        {
            if (enableButtonSound == true && soundSource != null)
            {
                soundSource.PlayOneShot(clickClip);
            }
            gazedTimer = 0;
            cursorTimer.value = 0;
            _isClick = true;
            StateNameController.isClick = true;
            GVRClick.Invoke();
            _gazedStatus = false;
            
        }
        
        // Already selected and is not continuous button
        if (_isClick && !_continueButton)
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        _image.color = Color.grey;
        _uiGradient.enabled = false;
    }


    
    public void OnPointerOn()
    {
        _gazedStatus = true;
    }

    public void OnPointerOff()
    {
        _gazedStatus = false;
        gazedTimer = 0;
        cursorTimer.value = 0;
        _animatorButton.Play("HoverOffAnimation");
    }

    IEnumerator PlayHoverOn()
    {
        _animatorButton.Play("HoverOnAnimation");
        yield return null;
    }
    
    private void Hovering()
    {
        StartCoroutine(PlayHoverOn());
    }
}
