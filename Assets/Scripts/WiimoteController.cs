using UnityEngine;
using WiimoteApi;

public class WiimoteController : MonoBehaviour
{
    private Wiimote wiimote;

    void Update()
    {
        // リモコンが接続されていない場合は検索
        if (!WiimoteManager.HasWiimote())
        {
            WiimoteManager.FindWiimotes();
            return;
        }

        wiimote = WiimoteManager.Wiimotes[0];

        // データの読み取り（毎フレーム必要）
        int ret;
        do
        {
            ret = wiimote.ReadWiimoteData();
        } while (ret > 0);

        // ボタン入力の検知
        if (wiimote.Button.a)
        {
            Debug.Log("Aボタンが押されています");
        }

        // LEDの制御（例：1番目のLEDを点灯）
        wiimote.SendPlayerLED(true, false, false, false);
    }
}
