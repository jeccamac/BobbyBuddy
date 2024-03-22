using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] public Animator _bobbyAnim = null;
    [SerializeField] private ActionCountTimer actionTimer = null;
    [SerializeField] private float cleanTimer = 5f;
    [SerializeField] private SpriteRenderer _areaHighlight = null;
    [SerializeField] private ParticleSystem _confetti, _sparks = null;
    private Animator _animAreaHL;

    [Header("Dentist Settings")]
    [SerializeField] private GameObject[] dentistObjects = {};
    private Animator _animClean;
    [SerializeField] private GameObject _cleaningAction = null;
    [SerializeField] private string[] speech = {};
    private Vector3[] startPos;
    private bool xrayEnabled, hasCleaned, canParty = false;

    private void Awake() 
    {
        _animAreaHL = _areaHighlight.gameObject.GetComponent<Animator>();
        _animClean = dentistObjects[1].gameObject.GetComponent<Animator>();
        _bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    private void Start() 
    {
        //save start positions of all dentist office objects
        startPos = new Vector3[dentistObjects.Length];
        for (int i=0; i<dentistObjects.Length; i++)
        {
            startPos[i] = dentistObjects[i].transform.position;
        }

        _areaHighlight.enabled = false;
        _animClean.enabled = false;
        _cleaningAction.SetActive(false);
    }

    private void Update() 
    {
        HasCleaned();
        UpdateDental();
        ClampXray();
    }

    public void ToggleXRay()
    {
        _cleaningAction.SetActive(false);

        //gameobj.setactive
        if (xrayEnabled == false) 
        { 
            //xrayObject.SetActive(true); 
            dentistObjects[0].SetActive(true);
            AudioManager.Instance.PlaySFX("XRay On");
            xrayEnabled = true;
            CallSpeech(0);
        }
        else if (xrayEnabled == true)
        { 
            //xrayObject.SetActive(false);
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
    }
    private void ClampXray()
    {
        //get transform position
        var xpos = dentistObjects[0].transform.position.x;
        var ypos = dentistObjects[0].transform.position.y;
        var zpos = dentistObjects[0].transform.position.z;

        //clamp xray position
        xpos = Mathf.Clamp(xpos, -0.3f, 0.3f);
        ypos = Mathf.Clamp(ypos, 1.72f, 2.72f);
        zpos = Mathf.Clamp(zpos, -0.8f, -0.8f);

        //apply clamp
        var clampPos = new Vector3(xpos, ypos, zpos);
        dentistObjects[0].transform.position = clampPos;
    }

    public void SelectScaler()
    {
        if (xrayEnabled == true)
        {
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
        
        _cleaningAction.SetActive(true);
        _animClean.enabled  = true;
        _animClean.Play("Select");
        AudioManager.Instance.PlaySFX("Button Click");

        CallSpeech(1);
    }

    public void CleanTeeth()
    {
        _animClean.enabled = false;
        _bobbyAnim.Play("OpenMouth");

        _areaHighlight.enabled = true;
        _animAreaHL.Play("Glow");

        AudioManager.Instance.PlaySFX("Drill");

        actionTimer.StartTimer(cleanTimer);
    }

    public void CancelClean()
    {
        if (actionTimer.timeRemaining > 0 && !actionTimer.timerEnded)
        {
            actionTimer.CancelTimer();
            hasCleaned = false;
        }

        _bobbyAnim.Play("CloseMouth");

        _areaHighlight.enabled = false;
        AudioManager.Instance.StopSFX("Drill");
    }

    private void HasCleaned()
    {
        if (actionTimer.timeRemaining <= 0 && actionTimer.timerEnded && actionTimer.timerComplete)
        {
            hasCleaned = true;
            canParty = true;
            actionTimer.timerComplete = false;

            _areaHighlight.enabled = false;
            AudioManager.Instance.StopSFX("Drill");

            ObjectReset();
        }
    }

    public void PartyFanfare()
    {
        if (xrayEnabled == true)
        {
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }

        if (canParty)
        {
            _cleaningAction.SetActive(false);
            
            AudioManager.Instance.PlaySFX("Fanfare");
            CallSpeech(2);

            //particle effects here
            if (_confetti != null && _sparks != null)
            {
                _confetti.Play();
                _sparks.Play();
            }

            canParty = false;

        } else { TextDisplay.Instance.ShowText("Bobby hasn't cleaned his teeth yet.", 3f); }
        
    }

    private void UpdateDental()
    {
        if (hasCleaned)
        {
            DataManager.Instance.UpHealth();
            hasCleaned = false;
            Debug.Log("cleaning teeth increased dental health state by 1");
        }
    }

    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        TextDisplay.Instance.ShowText(speak, 3f);
    }

    public void CallRandomSpeech()
    {
        string speak = speech[Random.Range(0, speech.Length)];
        TextDisplay.Instance.ShowText(speak, 3f);
    }

    public void ObjectReset()
    {
        for (int i=0; i<dentistObjects.Length; i++)
        {
            dentistObjects[i].transform.position = startPos[i];
        }
    }
}
