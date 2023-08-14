using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タッチすると、キャンバスを切り換える
public class OnButtonDown_SwitchCanvas : MonoBehaviour {

	public GameObject NowCanvas;  // シーン名：Inspectorで指定
    public GameObject NextCanvas;  // シーン名：Inspectorで指定
    public float DelayTime=5f;
    public ButtonState SceneMoveButton;

    void FixedUpdate() 
	{ // タッチしたら
      // シーンを切り換える
        Invoke("SwitchCanvas", DelayTime);

    }
	void Update()
    {

	}
    void SwitchCanvas()
    {
        if (SceneMoveButton.IsDown() == true)
        {
            NowCanvas.SetActive(false);
            NextCanvas.SetActive(true);
        }
    }
}
