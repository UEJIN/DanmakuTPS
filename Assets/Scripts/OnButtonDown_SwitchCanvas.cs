using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タッチすると、キャンバスを切り換える
public class OnButtonDown_SwitchCanvas : MonoBehaviour {

	public GameObject NowCanvas;  // シーン名：Inspectorで指定
    public GameObject NextCanvas;  // シーン名：Inspectorで指定
    //public float DelayTime=5f;
    public ButtonState SceneMoveButton;

    void FixedUpdate() 
	{ // タッチしたら
      // シーンを切り換える


    }
	void Update()
    {
        //Invoke("SwitchCanvas", DelayTime);
        SwitchCanvas();

    }
    public void SwitchCanvas()
    {
        if (SceneMoveButton.IsUp() == true)
        {
            //Debug.Log("ボタンダウン" + this.gameObject.name);
            NowCanvas.SetActive(false);
            NextCanvas.SetActive(true);
        }
    }
}
