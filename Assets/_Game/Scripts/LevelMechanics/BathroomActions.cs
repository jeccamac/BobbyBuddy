using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BathroomActions : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] public Animator _bobbyAnim = null;
    [SerializeField] private ActionCountTimer actionTimer = null;
    [SerializeField] private SpriteRenderer _areaHighlight = null;
    [SerializeField] private SpriteRenderer _pointerHelp = null;
    [SerializeField] private ParticleSystem _bubbles = null;

    [Tooltip("Time counted in Seconds, Counter is total number of counts")]
    [SerializeField] private float brushTime = 5;
    [SerializeField] private float flossCount = 6;

    [Header("Bathroom Settings")]
    [SerializeField] private GameObject[] bathObjects = {};
    [SerializeField] private GameObject[] bathActions = {};
    
    [SerializeField] private GameObject _mwTapbox, _triggerMW, _triggerDrink = null;
    private Vector3[] startPos;
    private Animator _animAreaHL, _animPointer, _animBrush, _animFloss, _animMW = null;
    private bool hasBrushed, hasFlossed, hasMouthwash, clickedAway = false;
    
    [Tooltip("Series of speech text every time the function is called")]
    [SerializeField] private string[] speech = 
    {
        "Help me brush my teeth!",
        "Keep brushing for 2 minutes.",
        "Flossing is good for you.",
        "Freshen up with mouthwash."
    };

    private void Awake() 
    {
        _bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        _animBrush = bathObjects[0].GetComponent<Animator>();
        _animAreaHL = _areaHighlight.gameObject.GetComponent<Animator>();
        _animPointer = _pointerHelp.gameObject.GetComponent<Animator>(); 
        _animFloss = bathObjects[1].GetComponent<Animator>();
        _animMW = bathObjects[2].GetComponent<Animator>();
        //_mwTapbox = GetComponent<GameObject>();
    }

    private void Start() 
    {
        _areaHighlight.enabled = false;
        _animFloss.enabled = false;

        _pointerHelp.enabled = false;

        //set all actions and trigger boxes to false on start
        for (int i=0; i < bathActions.Length; i++)
        {
            bathActions[i].SetActive(false);
        }

        //save start position of all bathroom objects that will be moved around
        startPos = new Vector3[bathObjects.Length];
        for (int i=0; i < bathObjects.Length; i++)
        {
            startPos[i] = bathObjects[i].transform.position;
        }
    }

    private void Update()
    {
        HasBrushed();
        HasFlossed();
        UpdateDental();
    }
    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        TextDisplay.Instance.ShowText(speak, 3f);
    }

        //BRUSH ACTIONS

    public void SelectBrush()
    {
        ActionReset();
        
        //_playerAnimCont.Play("BareTeeth");

        AudioManager.Instance.PlaySFX("Button Click");
        bathActions[0].SetActive(true); //turn brush actions ON
        bathActions[1].SetActive(false); //turn floss actions OFF
        bathActions[2].SetActive(false); //turn mouthwash actions OFF
        
        CallSpeech(0);

        _pointerHelp.enabled = true;
        _animPointer.enabled = true;
        if (_pointerHelp != null) { _animPointer.Play("SwipeUp"); }
    }
    public void BrushTeeth()
    {
        _animPointer.enabled = false; //stop animation
        _pointerHelp.enabled = false; //disable renderer

        //animate brush to brush position
        if (_animBrush != null)
        {
            CallSpeech(1);
            _animBrush.Play("Brush");

            _areaHighlight.enabled = true;
            _animAreaHL.Play("Glow");

            //start timer
            actionTimer.StartTimer(brushTime);
        }

        _bobbyAnim.Play("BareTeeth");
        
        //particle effects
        if (_bubbles != null)
        {
            _bubbles.Play();
        }
    }

    public void StopBrushing()
    {
        if (clickedAway == true && actionTimer.timeRemaining != 0)
        {
            actionTimer.CancelTimer();
            actionTimer.timeRemaining = 0; //reset
        } 
        else if (clickedAway == false && actionTimer.timeRemaining > 0 && !actionTimer.timerEnded)
        {
            actionTimer.StopTimer();
            hasBrushed = false;
        }

        // stop animations
        _bobbyAnim.Play("IdleSitting");
        
        if (_animBrush != null && bathActions[0].activeSelf == true)
        {
            _animBrush.Play("Idle");
            _areaHighlight.enabled = false;
        }
        if (_bubbles != null)
        {
            _bubbles.Stop();
        }
    }

    public void HasBrushed()
    {
        //if action was completed, then brushing complete and add tooth health
        if (actionTimer.timeRemaining <= 0 && actionTimer.timerEnded && actionTimer.timerComplete)
        {
            hasBrushed = true;
            actionTimer.timerComplete = false;

            //stop animations
            _bobbyAnim.Play("IdleSitting");

            if (_animBrush != null)
            {
                _animBrush.Play("Idle");
                _areaHighlight.enabled = false;
            }

            if (_bubbles != null)
            {
                _bubbles.Stop();
            }

            //reset brush
            ObjectReset();
        }
    }


        //FLOSS ACTIONS

    public void SelectFloss()
    {
        ActionReset();
        AudioManager.Instance.PlaySFX("Button Click");
        bathActions[0].SetActive(false); //turn brush actions OFF
        bathActions[1].SetActive(true); //turn floss actions ON
        bathActions[2].SetActive(false); //turn mouthwash actions OFF
        CallSpeech(2);

        _pointerHelp.enabled = true;
        _animPointer.enabled = true;
        if (_pointerHelp != null) { _animPointer.Play("SwipeUp"); }
    }
    public void StartFlossing()
    {
        _animPointer.enabled = false;
        _pointerHelp.enabled = false;

        _bobbyAnim.Play("OpenMouth");

        actionTimer.StartCounter(flossCount);

        if (_animFloss != null)
        {
            _animFloss.enabled = true;
            _animFloss.Play("Open");
        }
    }

    public void StopFlossing()
    {
        if (actionTimer.counterRunning && !actionTimer.counterComplete)
        {
            actionTimer.CancelCounter();
            hasFlossed = false;

            _bobbyAnim.Play("CloseMouth");

            if (_animFloss != null)
            {
                StartCoroutine(StopFloss());
            }

            IEnumerator StopFloss()
            {
                _animFloss.Play("Close");

                yield return new WaitForSeconds(2f);
                _animFloss.enabled = false;
            }
        }
    }

    public void HasFlossed()
    {
        if ( actionTimer.counterComplete == true )
        {
            hasFlossed = true;

            actionTimer.counterComplete = false;

            //stop animations
            _bobbyAnim.Play("CloseMouth");

            if (_animFloss != null)
            {
                StartCoroutine(StopFloss());
            }

            IEnumerator StopFloss()
            {
                _animFloss.Play("Close");

                yield return new WaitForSeconds(2f);
                _animFloss.enabled = false;
            }

            ObjectReset();
        }
    }

        //MOUTHWASH ACTIONS
    public void SelectMouthwash()
    {
        ActionReset();
        AudioManager.Instance.PlaySFX("Button Click");
        bathActions[0].SetActive(false); //turn brush actions OFF
        bathActions[1].SetActive(false); //turn floss actions OFF
        bathActions[2].SetActive(true); //turn mouthwash actions ON
        _animMW.enabled = false;
        CallSpeech(3);

        _pointerHelp.enabled = true;
        _animPointer.enabled = true;
        if (_pointerHelp != null) { _animPointer.Play("SwipeUp"); }
    }
    public void StartMW()
    {
        _animPointer.enabled = false;
        _pointerHelp.enabled = false;

        if (_animMW != null)
        {
            StartCoroutine(StartMouthwash());
        }

        IEnumerator StartMouthwash()
        {
            CallSpeech(3);
            _animMW.enabled = true;
            _animMW.Play("Open");

            yield return new WaitForSeconds(2f);
            _mwTapbox.SetActive(true); 
            _animMW.Play("TapBottle");
        }
    }

    public void TapMW()
    {
        if (_animMW != null)
        {
            StartCoroutine(PourMouthwash());
        }

        IEnumerator PourMouthwash()
        {
            _animMW.enabled = true;
            _animMW.Play("Pour");
            _bobbyAnim.Play("OpenMouth");
            _mwTapbox.SetActive(false);

            yield return new WaitForSeconds(2f);
            _pointerHelp.enabled = true;
            _animPointer.enabled = true;
            if (_pointerHelp != null) { _animPointer.Play("SwipeUp"); }
            _animMW.enabled = false;
            _areaHighlight.enabled = true;
            _animAreaHL.Play("Glow");
            _triggerMW.SetActive(false);
            _triggerDrink.SetActive(true);
        }
    
    }

    public void DrinkMW()
    {
        _animPointer.enabled = false;
        _pointerHelp.enabled = false;

        if (_animMW != null)
        {
            StartCoroutine(DrinkMouthwash());
        }

        IEnumerator DrinkMouthwash()
        {
            _animMW.enabled = true;
            _animMW.Play("Drink");
            _bobbyAnim.Play("Chewing");
            _areaHighlight.enabled = false;
            _triggerDrink.SetActive(false);
            
            yield return new WaitForSeconds(1f);
            _animMW.enabled = false;
            _triggerMW.SetActive(true);
            hasMouthwash = true;
            CallSpeech(4);
            ObjectReset();
            
        }
    }

    private void UpdateDental()
    {
        if (hasBrushed)
        {
            DataManager.Instance.AddHealth(15);
            hasBrushed = false;
            Debug.Log("brushing added tooth health amount of 20");
        }

        if (hasFlossed)
        {
            DataManager.Instance.AddHealth(10);
            hasFlossed = false;
            Debug.Log("flossing added 15 tooth health");
        }

        if (hasMouthwash)
        {
            DataManager.Instance.AddHealth(5);
            hasMouthwash = false;
            Debug.Log("mouthwash added 5 tooth health");
        }
    }

    public void ActionReset()
    {
        clickedAway = true;
        
        StopBrushing();
        StopFlossing();
        ObjectReset();

        _bobbyAnim.Play("IdleSitting");

        _animFloss.enabled = false;

        clickedAway = false;
    }
    private void ObjectReset()
    {
        for (int i=0; i<bathObjects.Length; i++)
        {
            bathObjects[i].transform.position = startPos[i];
        }
    }
}
