using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioClip startCountdownSound;

    public Enum.DeviceType deviceType = Enum.DeviceType.None;
    private bool isGameStart = false;
    private float time = 0.0f;

    void Start()
    {
         Application.targetFrameRate = 60;
        uiManager.init();
        uiManager.TitleUI(true);
    }

    private void Update()
    {
        if (isGameStart)
            time += Time.deltaTime;
        uiManager.UpdateTime(time);
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
}
