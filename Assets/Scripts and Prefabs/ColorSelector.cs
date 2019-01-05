using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{

    public Gradient colorRange;
    public float gradientAdjustmentRate;
    public float constantFlowRate = 3;
    float gradientIndexPct;

    public static ColorSelector selectedBlock;

    void Start()
    {
        s = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
		HandleAutoInput();
    }

    Vector2 mouseDown = Vector2.zero;
    Vector2 mouseOffset = Vector2.zero;

    public bool canAddColorToCalendar = true;

    void HandleMouseInput()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            // if(GameManager.InGlossary()) { return; }
			Vector3 downPos = Extensions.ScreenToWorld(Input.mousePosition);
            RaycastHit hitinfo;

            Debug.DrawRay(downPos, Vector3.forward * 100, Color.red, 1);
                
            if (Physics.Raycast(downPos, Vector3.forward, out hitinfo, 100))
            {
                // Debug.Log("Ray");
                if (hitinfo.collider.gameObject == gameObject)
                {	
                    // Debug.Log("hit");
					selectedBlock = this;
					mouseDown = downPos;
                    if (Time.time - lastTapTime < doubleTapThreshold)
                    {
                        DoubleTap();
                    }
                    lastTapTime = Time.time;
                }
			} else {
				mouseDown = Vector2.zero;
			}
        }
        if (Input.GetMouseButton(0) && IsSelected())
        {
            mouseOffset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDown;
            mouseOffset = new Vector2(mouseOffset.x / (ScreenInfo.w / 2), mouseOffset.y / (ScreenInfo.h / 2));
        }

        if(Input.GetMouseButtonUp(0)) {
            selectedBlock = null;
            mouseOffset   = Vector2.zero;
        }

		UpdateColor(mouseOffset); // mouse Offset
    }

    public float stationaryMgn = .25f;

    public bool BeingHeld() {
        return mouseOffset.magnitude < stationaryMgn;
    }

    bool IsSelected() {
        return selectedBlock == this;
    }

	void HandleAutoInput() {
		if (autoInput)
        {
            if (Time.time - lastAutoCaptureTime > autoCaptureDelay)
            {
                DoubleTap();
                lastAutoCaptureTime = Time.time;
            }
        }
	}
    
	void UpdateColor(Vector2 offset)
    {
		float t = gradientIndexPct + (EZEasings.SmoothStart3(offset.x) * gradientAdjustmentRate * Time.deltaTime);

        if (useConstantFlow)
        {
            t += constantFlowRate * Time.deltaTime;
        }

        if (t < 0)
        {
            t = 1 - t;
        }
        else
        {
            t %= 1;
        }
        gradientIndexPct = t;


        bgColor = colorRange.Evaluate(t);
        s.color = bgColor;
    }


    public bool autoInput;
    float lastAutoCaptureTime;
    public float autoCaptureDelay = 1f;

    public bool useConstantFlow = false;

    SpriteRenderer s;

    void UpdateColor(float t)
    {
        bgColor = colorRange.Evaluate(t);
        s.color = bgColor;
    }

    void DoubleTap()
    {
        EventManager.TriggerEvent("DoubleTap");
        SelectColor();
    }

    void SelectColor()
    {
        if(!canAddColorToCalendar) { return; }

        Calendar.AddColor(selectedColorCount, bgColor);
        selectedColorCount++;
        if (selectedColorCount >= Calendar.totalDayCount)
        {
            selectedColorCount = 0;
        }
    }

    static int selectedColorCount = 0;

    Color bgColor;

    float lastTapTime;
    float doubleTapThreshold = .2f;
}
