using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInputManager : SubManagersBase
{
    public static CInputManager Instance = null;

    public enum AxisToMove
    {
        X_Axis,
        Y_Axis
    }
    
    public Vector2 rotateSpeed = new Vector2(90f, 90f);

    public float rotationX;
    public float rotationY;

    public override void Initialize(AppManager appManager)
    {
        base.Initialize(appManager);

        Instance = this;
    }

    public void ClampValuesTorotateX(float mouseValues, out Vector3 posMouse, bool mayClamp = true)
    {
        mouseValues = mouseValues * rotateSpeed.y * Time.deltaTime;

        rotationX -= mouseValues;

        if(mayClamp)
            rotationX = Mathf.Clamp(rotationX, -40f, 40f);

        posMouse = new Vector3(rotationX, 0f, 0f);
    }

    public void ClampValuesTorotateY(float mouseValues, out Vector3 posMouse, bool mayClamp = true)
    {
        mouseValues = mouseValues * rotateSpeed.x * Time.deltaTime;

        rotationY -= mouseValues;

        if(mayClamp)
            rotationY = Mathf.Clamp(rotationY, -40f, 40f);

        posMouse = new Vector3(0f, rotationY, 0f);
    }

    public float GetMouseAxis(CInputManager.AxisToMove typeOfAxis)
    {
        Vector2 inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float inputAxis = 0f;

        switch (typeOfAxis)
        {
            case CInputManager.AxisToMove.X_Axis:
                inputAxis = inputMouse.x;
                break;
            case CInputManager.AxisToMove.Y_Axis:
                inputAxis = inputMouse.y;
                break;
            default:
                inputAxis = inputMouse.x;
                break;

        }

        return inputAxis;
    }
}
