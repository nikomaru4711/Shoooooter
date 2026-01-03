using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioClip startCountdownSound;
    [SerializeField] private AudioClip endGameSound;

    [NonSerialized] public Enum.DeviceType deviceType = Enum.DeviceType.None;
    private bool isGameStart = false;
    private float time = 0.0f;
    private bool isPause = false;

    void Start()
    {
         Application.targetFrameRate = 60;
        uiManager.init();
        uiManager.TitleUI(true);
    }

    private void Update()
    {
        if (isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                {//Resume
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1.0f;
                    isPause = false;
                    uiManager.PauseUI(false);
                }
                else
                {//Pause
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0f;
                    isPause = true;
                    uiManager.PauseUI(true);
                }
            }
            time += Time.deltaTime;
            uiManager.UpdateTime(time);
            if (scoreManager.score >= 200)
            {//200点を越えたら
                 //ゲーム終了
                 isGameStart = false;
                //ゲーム終了SE再生
                audioManager.PlaySound(endGameSound, 0.2f);
                //UIで結果表示
                uiManager.ShowScore(time, playerController.shootedIgaguriCount, playerController.shootedBulletCount);
                Cursor.lockState = CursorLockMode.None;
                cameraController.isMoveEnabled = false;
                playerController.isShootEnable = false;
            }
        }
    }

    public void PrepareGame()
    {
        //タイマーinit()
        time = 0.0f;
        //スコアのinit()
        scoreManager.init();
        //UIのゲーム開始init()
        uiManager.InGameUI(true);
        //カーソルをロック
        Cursor.lockState = CursorLockMode.Locked;
        //マウスの場合は視点移動を可能に
        if (deviceType == Enum.DeviceType.Mouse)
            cameraController.isMoveEnabled = true;
        audioManager.PlaySound(startCountdownSound, 0.25f);
        Invoke("StartGame", 3.0f);
    }

    public void StartGame()
    {
        Debug.Log("ゲームスタート！");
        //ゲーム開始処理
        //いがぐりの発射を可能に
        playerController.isShootEnable = true;
        //タイマーがあるなら作動させる
        isGameStart = true;
    }

    public void SetDeviceType(Enum.DeviceType type)
    {
        deviceType = type;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        isPause = false;
        uiManager.PauseUI(false);
    }

    public void ReloadGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }
}
