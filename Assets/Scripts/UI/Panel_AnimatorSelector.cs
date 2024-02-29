using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_AnimatorSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Transform buttonsContainer;

    [SerializeField]
    private Button buttonContinue;

    private void Start()
    {
        SpawnAnimationButtons();
    }

    public void ChangeCurrentAnimation(C_AnimatorController.AnimationStates animationState)
    {
        CharacterManager.changeAnimation?.Invoke(animationState);

        Debug.Log("Current Animation " + animationState.ToString());
    }

    private void SpawnAnimationButtons() 
    {
        if(buttonPrefab ==  null) return;

        if(buttonsContainer == null) return;

        for(int i = 0; i < Enum.GetValues(typeof(C_AnimatorController.AnimationStates)).Length; i++) 
        {
            C_AnimatorController.AnimationStates valueToSet = (C_AnimatorController.AnimationStates)i;

            if (valueToSet == C_AnimatorController.AnimationStates.None) continue;

            GameObject button = Instantiate(buttonPrefab, buttonsContainer);

            if (button == null) break;

            button.gameObject.SetActive(true);

            button.transform.SetAsFirstSibling();

            Button eventButton = button.GetComponent<Button>();

            if (eventButton == null) break;

            eventButton.onClick.AddListener(() => ChangeCurrentAnimation(valueToSet));

            TextMeshProUGUI text = button.transform.GetComponentInChildren<TextMeshProUGUI>();

            if(text ==  null) break;

            text.SetText(((C_AnimatorController.AnimationStates)i).ToString());
        }

        if(buttonContinue ==  null) return;

        buttonContinue.onClick.AddListener(() => 
        {
            SetNewEscene();
            Destroy(this.gameObject);
        });
    }

    private void SetNewEscene() 
    {
        if(AppManager.Instance != null)
            AppManager.Instance.sceneManager.LoadBasicScene();

        if (CharacterManager.Instance == null) return;
        if (CharacterManager.Instance.characterPlayer == null) return;

        CharacterManager.Instance.SetCameraAsFPS();
        CharacterManager.Instance.characterPlayer.currentMovementState = CharacterPlayer.CharacterMovementState.Move;
        
    }
}
