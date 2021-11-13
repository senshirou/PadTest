using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePadPlayer : MonoBehaviour
{
	public float JumpPower;//①ジャンプ力
	public Transform verRot;//①縦の視点移動の変数(カメラに合わせる)
	public Transform horRot;//①横の視点移動の変数(プレイヤーに合わせる)
	

    //上下の視点範囲を制御
    [Range(0.3f, 0.5f)]
    public float maxYAngle = 0.5f;

    //プレイヤーの速度制御
    [Range(0.01f,0.1f)]
    public float MoveSpeed = 0.01f;

    //プレイヤーの視点移動速度制御
    [Range(0.01f,0.5f)]
    public float ViewMoveSpeed = 0.05f;

    //アナログコントローラーの微反応の制御
    [Range(0.01f,0.02f)]
    public float ControllerClearRate = 0.015f;

    float X_Runf = 0;
    float Z_Runf = 0;

    float X_Rotationf = 0;
    float Y_Rotationf = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        //左のアナログスティックの値を取得
        float X_Rotation = Input.GetAxis("RstickHorizontal") ;
        float Y_Rotation = Input.GetAxis("RstickVertical") ;

        //左のアナログスティックの値を取得
        float X_Run = Input.GetAxis("LstickHorizontal");
        float Z_Run = -Input.GetAxis("LstickVertical");

        //アナログスティックの微反動の値を出力しないようにする
        X_Runf = (-ControllerClearRate <= X_Run && X_Run <= ControllerClearRate) ? 0 : X_Run;
        Z_Runf = (-ControllerClearRate <= Z_Run && Z_Run <= ControllerClearRate) ? 0 : Z_Run;
        X_Rotationf = (-ControllerClearRate <= X_Rotation && X_Rotation <= ControllerClearRate) ? 0 : X_Rotation;
        Y_Rotationf = (-ControllerClearRate <= Y_Rotation && Y_Rotation <= ControllerClearRate) ? 0 : Y_Rotation;
  
        //プレイヤーの歩行
        transform.position += transform.forward * Z_Runf * MoveSpeed;
        transform.position += transform.right * X_Runf * MoveSpeed;

        //プレイヤーのX視点移動
        horRot.transform.Rotate (new Vector3 (0, X_Rotationf * 0.1f, 0));//①プレイヤーのY軸の回転をX_Rotationに合わせる

        //プレイヤーのY視点移動。1回転しないように制御
        if(verRot.transform.rotation.x >= -maxYAngle && Y_Rotationf < 0) 
        {
            verRot.transform.Rotate (Y_Rotationf * ViewMoveSpeed, 0, 0);
        }

        else if(verRot.transform.rotation.x <= maxYAngle && Y_Rotationf > 0)
        {
            verRot.transform.Rotate (Y_Rotationf * ViewMoveSpeed, 0, 0);
        }
        
    }
}
