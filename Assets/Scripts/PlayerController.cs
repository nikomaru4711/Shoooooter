using System;
using UnityEngine;
using WiimoteApi;

public class PlayerController : MonoBehaviour
{
    [SerializeField] IgaguriGenerator _igaguriGenerator;
    [SerializeField] private Transform cameraBody;

    private Wiimote wiimote;
    private bool oldFrame_isPressed_A_Button = false;

    void FixedUpdate()
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

        //マウス左クリック || WiiリモコンAボタンでいがぐり発射
        if (Input.GetMouseButtonDown(0) || wiimote.Button.a)
        {
            if (oldFrame_isPressed_A_Button) return;
            oldFrame_isPressed_A_Button = true;
            GameObject igaguri = _igaguriGenerator.GenerateIgaguri();
            igaguri.transform.localPosition = transform.position;
            igaguri.GetComponent<IgaguriController>().Shoot(cameraBody.forward);
        }
        else
        {
             oldFrame_isPressed_A_Button = false;
        }

            // WiiリモコンLEDの制御（例：1番目のLEDを点灯）
            wiimote.SendPlayerLED(true, false, false, false);
    }
}
