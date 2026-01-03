using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject titleUISet;
    [SerializeField] private GameObject gameRuleUISet;
    [SerializeField] private GameObject instructMouseUISet;
    [SerializeField] private GameObject instructWiiControllerUISet;
    [SerializeField] private GameObject inGameUISet;
    [SerializeField] private GameObject pauseUISet;
    [SerializeField] private GameObject resultUISet;
    [SerializeField] private Image ammo_1;
    [SerializeField] private Image ammo_2;
    [SerializeField] private Image ammo_3;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text resultTimeText;
    [SerializeField] private TMP_Text resultIgaguriCountText;
    [SerializeField] private TMP_Text resultBulletCountText;
    [SerializeField] private TMP_Text resultEvaluatiomText;


    public void init()
    {
        titleUISet.SetActive(false);
        gameRuleUISet.SetActive(false);
        instructMouseUISet.SetActive(false);
        instructWiiControllerUISet.SetActive(false);
        inGameUISet.SetActive(false);
        pauseUISet.SetActive(false);
        resultUISet.SetActive(false);
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
    public void InGameUI(bool index)
    {
        inGameUISet.SetActive(index);
    }

    public void PauseUI(bool index)
    {
        pauseUISet.SetActive(index);
    }
    public void ResultUI(bool index)
    {
        resultUISet.SetActive(index);
    }

    ///タイトルで押した際の処理
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
                    InstructMouseUI(true);
                else
                    InstructWiiControllerUI(true);
                break;
            case 30://ゲーム開始の呼び出し
                gameManager.PrepareGame();
                break;
            default:
                Debug.LogErrorFormat("UIManager:onClickNext:Invalid NextStep. {0}",NextStep);
                break;
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "スコア: " + score.ToString();
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)(time / 60);
        float seconds = (float)(time % 60);
        timeText.text = string.Format("タイム: {0:00}:{1:00.00}", minutes, seconds);
    }

    public void useAmmo(int remainingAmmo)
    {
        Color color;
        switch (remainingAmmo)
        {
            case 1:
                color = ammo_1.color;
                color.a = 0.3f;
                ammo_1.color = color;
                break;
            case 2:
                color = ammo_2.color;
                color.a = 0.3f;
                ammo_2.color = color;
                break;
            case 3:
                color = ammo_3.color;
                color.a = 0.3f;
                ammo_3.color = color;
                break;
            default :
                break;
        }
    }

    public void AddAmmo(int totalAmmo)
    {
        Color color;
        switch (totalAmmo)
        {
            case 1:
                color = ammo_1.color;
                color.a = 1f;
                ammo_1.color = color;
                break;
            case 2:
                color = ammo_2.color;
                color.a = 1f;
                ammo_2.color = color;
                break;
            case 3:
                color = ammo_3.color;
                color.a = 1f;
                ammo_3.color = color;
                break;
            default:
                break;
        }
    }
    
    public void ShowScore(float time, int shootedIgaguriCount, int shootedBulletCount)
    {
        int minutes = (int)(time / 60);
        float seconds = (float)(time % 60);
        resultTimeText.text = string.Format("タイム: {0:00}:{1:00.00}", minutes, seconds);
        resultIgaguriCountText.text = "発射したいがぐりの数：" + shootedIgaguriCount.ToString();
        resultBulletCountText.text = "発射した弾の数：" + shootedBulletCount.ToString();
        int score = 0;

        //時間をスコア化
        if (time <= 30)
        {
            score += 2;
        }
        else if (time <= 60)
        {
            score += 1;
        }
        //発射したいがぐりの数をスコア化
        if(shootedIgaguriCount <= 25)
        {
            score += 2;
        } else if (shootedIgaguriCount <= 50)
        {
            score += 1;
        }

        //発射した弾の数をスコア化
        if (shootedBulletCount <= 2)
        {
            score += 2;
        }
        else if (shootedBulletCount <= 4)
        {
            score += 1;
        }

        if (score >= 6)
        {
            resultEvaluatiomText.text = "伝説のガンマン級";
        }
        else if (score >= 4)
        {
            resultEvaluatiomText.text = "射的のプロ級";
        }
        else if(score >= 2)
        {
            resultEvaluatiomText.text = "アマチュアガンナー級";
        }
        else
        {
            resultEvaluatiomText.text = "射的愛好家";
        }
        ResultUI(true);
    }
}
