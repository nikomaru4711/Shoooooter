using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioClip startCountdownSound;

    public Enum.DeviceType deviceType = Enum.DeviceType.None;
    void Start()
    {
         Application.targetFrameRate = 60;
        uiManager.init();
        uiManager.TitleUI(true);
    }

    public void PrepareGame()
    {
        //スコアのinit()
        //UIのゲーム開始init()
        //カーソルをロック
        Cursor.lockState = CursorLockMode.Locked;
        //マウスの場合は視点移動を可能に
        cameraController.isMoveEnabled = true;

        audioManager.PlaySound(startCountdownSound);
        Invoke("StartGame", 3.0f);
    }

    public void StartGame()
    {
        //ゲーム開始処理
        //いがぐりの発射を可能に
        //タイマーがあるなら作動させる
    }

    public void SetDeviceType(Enum.DeviceType type)
    {
        deviceType = type;
    }
}
