using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    void Update()
    {
        CameraViewMovement();
    }

    private void CameraViewMovement() 
    {
        if (CharacterManager.Instance == null) return;
        if (CharacterManager.Instance.characterPlayer == null) return;
        if (CharacterManager.Instance.characterPlayer.currentMovementState == CharacterPlayer.CharacterMovementState.Stop) return;

        float inputMouse = CInputManager.Instance.GetMouseAxis(CInputManager.AxisToMove.Y_Axis);

        CInputManager.Instance.ClampValuesTorotateX((inputMouse), out Vector3 posMouse);

        AppManager.Instance.cameraController.transform.localRotation = Quaternion.Euler(CInputManager.Instance.rotationX, 0f, 0f);

        AppManager.Instance.cameraController.transform.Rotate(posMouse);
    }

    


    
}
