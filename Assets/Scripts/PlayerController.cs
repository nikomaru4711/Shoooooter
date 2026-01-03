using System;
using UnityEngine;
using WiimoteApi;

public class PlayerController : MonoBehaviour
{
    [SerializeField] IgaguriGenerator igaguriGenerator;
    [SerializeField] private UIManager uiManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] private Transform cameraBody;

    private Wiimote wiimote = null;
    private bool oldFrame_isPressed_A_Button = false;
    [NonSerialized] public bool isShootEnable = false;
    [NonSerialized] public int shootedIgaguriCount = 0;
    [NonSerialized] public int shootedBulletCount = 0;
    private int remainingBullet = 3;
    private float localTime = 0;
    void FixedUpdate()
    {
        //弾が減っていたら時間カウント
        if(remainingBullet < 3)
        {
            localTime += Time.deltaTime;
            if(localTime > 5.0f)
            {
                remainingBullet++;
                uiManager.AddAmmo(remainingBullet);
                localTime = 0;
            }
        }

        if(gameManager.deviceType == Enum.DeviceType.WiiController)
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
            // WiiリモコンLEDの制御（例：1番目のLEDを点灯）
            wiimote.SendPlayerLED(true, false, false, false);
            //Aボタンでいがぐり発射
            if (isShootEnable && wiimote.Button.a)
                ShootIgaguri();
            else
                oldFrame_isPressed_A_Button = false;
        }

        //マウス左クリックでいがぐり発射
        if (isShootEnable && Input.GetMouseButtonDown(0))
            ShootIgaguri();
        else if (isShootEnable && Input.GetMouseButtonDown(2))
            ShootAmmo();
        else
            oldFrame_isPressed_A_Button = false;
    }

    private void ShootIgaguri()
    {
        if(oldFrame_isPressed_A_Button) return;
        oldFrame_isPressed_A_Button = true;
        GameObject igaguri = igaguriGenerator.GenerateIgaguri();
        igaguri.transform.localPosition = transform.position;
        igaguri.GetComponent<IgaguriController>().Shoot(cameraBody.forward);
        shootedIgaguriCount++;
    }

    private void ShootAmmo()
    {
        if (oldFrame_isPressed_A_Button || remainingBullet <= 0) return;
        oldFrame_isPressed_A_Button = true;
        uiManager.useAmmo(remainingBullet);
        remainingBullet -= 1;
        GameObject ammo = igaguriGenerator.GenerateAmmo();
        ammo.transform.localPosition = transform.position;
        ammo.transform.localRotation = cameraBody.rotation;
        ammo.GetComponent<IgaguriController>().Shoot(cameraBody.forward);
        shootedBulletCount++;
    }

    //private void OnApplicationQuit()
    //{
    //    // Wiiリモコンの接続解除
    //    if (wiimote != null)
    //    {
    //        wiimote.Disconnect();
    //    }
    //    WiimoteManager.Cleanup(wiimote);
    //}
}
