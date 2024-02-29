using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterPlayer))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private CharacterPlayer characterPlayer;

    private Vector3 fixedPosition = Vector3.zero;

    private void Awake()
    {
        fixedPosition = transform.position;

        if(characterPlayer == null)
            characterPlayer = GetComponent<CharacterPlayer>();
    }

    private void Update()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        if (characterPlayer == null) return;

        if (characterPlayer.currentMovementState == CharacterPlayer.CharacterMovementState.Stop) 
        {
            transform.position = fixedPosition;

            return;
        }

        float inputMouse = CInputManager.Instance.GetMouseAxis( CInputManager.AxisToMove.X_Axis);

        CInputManager.Instance.ClampValuesTorotateY((-inputMouse), out Vector3 posMouse, false);

        transform.localRotation = Quaternion.Euler(0f, CInputManager.Instance.rotationY, 0f);

        transform.Rotate(posMouse);
    }
}
