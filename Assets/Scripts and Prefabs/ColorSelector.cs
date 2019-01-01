using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{

    public Gradient colorRange;
    public float gradientAdjustmentRate;
    public float constantFlowRate = 3;
    float gradientIndexPct;

	public bool selected;

    void Start()
    {
        s = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
		HandleAutoInput();
    }

    Vector2 mouseDown = Vector2.zero;

    void HandleMouseInput()
    {
        Vector2 o = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
			Vector3 downPos = Extensions.ScreenToWorld(Input.mousePosition);
            RaycastHit hitinfo;
			// Debug.Log("down pos: " + downPos);
			// Debug.DrawRay(downPos, Vector3.forward, Color.red, 3);
            if (Physics.Raycast(downPos, Vector3.forward, out hitinfo, 100))
            {
				// Debug.Log("Raycasting. hit info: " + hitinfo.collider.gameObject);
                if (hitinfo.collider.gameObject == s.gameObject)
                {	
					// Debug.Log("successs");
					selected = true;
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
        if (Input.GetMouseButton(0) && selected)
        {
            o = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDown;
            o = new Vector2(o.x / (ScreenInfo.w / 2), o.y / (ScreenInfo.h / 2));
        }

		if(Input.GetMouseButtonUp(0)) {
			selected = false;
		}

		UpdateColor(o); // mouse Offset
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
