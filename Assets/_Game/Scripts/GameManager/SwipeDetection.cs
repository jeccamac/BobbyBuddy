using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class SwipeDetection : MonoBehaviour
{
    [Header("Swipe Settings")]
    [SerializeField] private GameObject swipeTrail;
    [SerializeField] private float minSwipeDistance = 0.2f;
    [SerializeField] private float maxSwipeTime = 1f;
    [SerializeField, Range(0f, 10f)] private float directionThreshold = 0.9f;
    public bool enableSwiping = false;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;
    private ActionCountTimer actionCounter;

    private void Start() 
    {
        actionCounter = FindObjectOfType<ActionCountTimer>();
        
        swipeTrail.SetActive(false);
    }
    private void Update() 
    {
        if (enableSwiping)
        {    
            if (Input.touches.Length > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 v3 = new Vector3(touch.position.x, touch.position.y, 1);
                Vector2 touchSwipe = Camera.main.ScreenToWorldPoint(v3);

                if (touch.phase == TouchPhase.Began)
                {
                    SwipeStart(touch.position, Time.time);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    SwipeEnd(touch.position, Time.time);
                }

                swipeTrail.transform.position = touchSwipe;
            }
        }   
    }

    public void SwipeEnable()
    {
        enableSwiping = true;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        
        // trail renderer
        swipeTrail.SetActive(true);
        swipeTrail.transform.position = position;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();

        swipeTrail.SetActive(false);
    }

    public void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minSwipeDistance
            && (endTime - startTime) <= maxSwipeTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 dir = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(dir);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            //get action counter here? and count++
            actionCounter.AddCount();
            //Debug.Log("swipe up");
        }
        if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            actionCounter.AddCount();
            //Debug.Log("swipe down");
        }
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            actionCounter.AddCount();
            //Debug.Log("swipe left");
        }
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            actionCounter.AddCount();
            //Debug.Log("swipe right");
        }
    }
}
