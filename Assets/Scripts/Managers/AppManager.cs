using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    [Header("Utils")]
    public GameObject characterPrefab;
    public CameraController cameraController;
    [Header("Scriptables")]
    public ScriptableWeapons_Settings weaponsSettings;

    [Header("Sub Managers")]
    [Space(10)]
    public MSceneManager sceneManager = new MSceneManager();

    [Space(20)]
    public UIManager UIManager = new UIManager();

    [Space(20)]
    public CharacterManager characterManager = new CharacterManager();

    [Space(20)]
    public CInputManager inputManager = new CInputManager();

    private void Awake()
    {
        InitializeManager();
    }
    private void Start()
    {
        InitializeSubManagers();
    }

    private void InitializeManager() 
    {
        if (Instance != null) return;
        
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void InitializeSubManagers()
    {
        sceneManager?.Initialize(this);

        UIManager?.Initialize(this);

        characterManager?.Initialize(this);

        inputManager?.Initialize(this);
    }

}
