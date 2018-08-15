using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	public UILabel NameLabel;
	public GameObject BestData;
	public UILabel BestUserData_Label;

	void GoPlay(){
		if(NameLabel.text == "Type your name"){
			return;
		}
		PlayerPrefs.SetString("UserName",NameLabel.text);

		Application.LoadLevel("MainPlay");
	
	}
	void BestScore(){
		BestUserData_Label.text = string.Format(
			"{0}:{1:N0}",
			PlayerPrefs.GetString("BestPlayer"),
			PlayerPrefs.GetFloat("BestScore"));
		if(BestUserData_Label.text  != ":0"){
			BestData.SetActive(true);
		} 

	}
	void Quit(){
		Application.Quit();
	}
}