using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

// タッチすると、シーンを切り換える
public class OnButtonDown_SwitchScene : MonoBehaviour {

	public string sceneName;  // シーン名：Inspectorで指定
	ButtonState SceneMoveButton;

    void OnEnable()
    {
        SceneMoveButton = this.gameObject.GetComponent<ButtonState>();
    }
    void Update() 
	{ // タッチしたら
      // シーンを切り換える

        if (SceneMoveButton.IsUp() == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
	void FixedUpdate()
    {

	}
}
