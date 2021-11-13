using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePad : MonoBehaviour
{
	public float JumpPower;//①ジャンプ力
	public Transform verRot;//①縦の視点移動の変数(カメラに合わせる)
	public Transform horRot;//①横の視点移動の変数(プレイヤーに合わせる)

    Camera _camera;
	

    //上下の視点範囲を制御
    [Range(0.3f, 0.5f)]
    public float maxYAngle = 0.5f;

    //プレイヤーの速度制御
    [Range(0.01f,10f)]
    public float MoveSpeed = 0.01f;

    //プレイヤーの視点移動速度制御
    [Range(0.01f,0.5f)]
    public float ViewMoveSpeed = 0.05f;

    //アナログコントローラーの微反応の制御
    [Range(0.01f,0.02f)]
    public float ControllerClearRate = 0.015f;

    [Range(0.01f,120f)]
    public float view = 0.01f;

    float X_Run = 0;
    float Z_Run = 0;

    float X_Rotationf = 0;
    float Y_Rotationf = 0;

    float time = 60;

    float a = 60;

    float min = 0;


    


    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {

        //左のアナログスティックの値を取得
        float X_Rotation = Input.GetAxis("RstickHorizontal") ;
        float Y_Rotation = Input.GetAxis("RstickVertical") ;

        //左のアナログスティックの値を取得
        float X_Move = Input.GetAxis("LstickHorizontal");
        float Z_Move = -Input.GetAxis("LstickVertical");
        
        if(Input.GetMouseButton(1))
        {
            a -= Time.deltaTime * 500f;
            _camera.fieldOfView = a;
            Debug.Log(a);
        }

        else if(Input.GetKeyUp(KeyCode.R))
        {
            a += Time.deltaTime * 5000f;
            _camera.fieldOfView = a;

        }

        //アナログスティックの微反動の値を出力しないようにする
        X_Run = (-ControllerClearRate <= X_Move && X_Move <= ControllerClearRate) ? 0 : X_Move;
        Z_Run = (-ControllerClearRate <= Z_Move && Z_Move <= ControllerClearRate) ? 0 : Z_Move;
        X_Rotationf = (-ControllerClearRate <= X_Rotation && X_Rotation <= ControllerClearRate) ? 0 : X_Rotation;
        Y_Rotationf = (-ControllerClearRate <= Y_Rotation && Y_Rotation <= ControllerClearRate) ? 0 : Y_Rotation;
  
        //プレイヤーの歩行
        transform.position += transform.forward * Z_Run * MoveSpeed;
        transform.position += transform.right * X_Run * MoveSpeed;

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
