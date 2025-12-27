using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject titleUISet;
    [SerializeField] private GameObject gameRuleUISet;
    [SerializeField] private GameObject instructMouseUISet;
    [SerializeField] private GameObject instructWiiControllerUISet;

    public void init()
    {
        titleUISet.SetActive(false);
        gameRuleUISet.SetActive(false);
        instructMouseUISet.SetActive(false);
        instructWiiControllerUISet.SetActive(false);
    }

    public void TitleUI(bool index)
    {
        titleUISet.SetActive(index);
    }
    public void GameRuleUI(bool index)
    {
        gameRuleUISet.SetActive(index);
    }
    public void InstructMouseUI(bool index)
    {
        instructMouseUISet.SetActive(index);
    }
    public void InstructWiiControllerUI(bool index)
    {
        instructWiiControllerUISet.SetActive(index);
    }

    //各ボタンを押した際の処理
    public void onClickNext(int NextStep)
    {
        //すべてのUIを非表示に
        init();
        switch (NextStep)
        {
            case 00://タイトル画面
                TitleUI(true);
                gameManager.SetDeviceType(Enum.DeviceType.None);

                break;
            case 10://ゲームルール画面
                GameRuleUI(true);
                break;
            case 11://ゲームルール画面(useMouse)
                gameManager.SetDeviceType(Enum.DeviceType.Mouse);
                GameRuleUI(true);
                break;
            case 12://ゲームルール画面(useWiiController)
                gameManager.SetDeviceType(Enum.DeviceType.WiiController);
                GameRuleUI(true);
                break;
            case 20://操作説明画面
                if (gameManager.deviceType == Enum.DeviceType.Mouse)
                {
                    InstructMouseUI(true);
                }
                else
                {
                    InstructWiiControllerUI(true);
                }
                break;
            case 30://ゲーム開始の呼び出し
                Debug.Log("ここまでで一旦停止！");
                //gameManager.PrepareGame();
                break;
            default:
                Debug.LogErrorFormat("UIManager:onClickNext:Invalid NextStep. {0}",NextStep);
                break;
        }
    }

}
