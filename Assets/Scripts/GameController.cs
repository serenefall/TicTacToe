using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player {
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor {
	public Color panelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {
	public Text[] buttonList;
	private string playerSide;
	private int moveCount;
	public GameObject gameOverPanel;
	public Text gameOverText;
	public GameObject restartButton;

	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;

	private void Awake () {
		SetGameControllerReferenceOnButtons ();
		moveCount = 0;
		gameOverPanel.SetActive (false);
		restartButton.SetActive (false);
	}

	void SetGameControllerReferenceOnButtons () {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent<GridSpace> ().SetGameControllerReference (this);
		}
	}

	public string GetPlayerSide () {
		return playerSide;
	}

	public void EndTurn () {
		moveCount++;

		if (
			(buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) ||
			(buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) ||
			(buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) ||
			(buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) ||
			(buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) ||
			(buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) ||
			(buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) ||
			(buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
		) {
			GameOver (playerSide);
		} else if (moveCount >= 9) {
			GameOver ("draw");
		} else {
			ChangeSide ();
		}
	}

	void GameOver (string whoWin) {
		gameOverPanel.SetActive (true);
		restartButton.SetActive (true);
		SetGameOverText (whoWin);
		SetBoardInteractable (false);
		if (whoWin == "draw") SetPlayerColorsInactive();
	}

	void ChangeSide () {
		playerSide = (playerSide == "X") ? "O" : "X";
		if (playerSide == "X") {
			SetPlayerColors (playerX, playerO);
		} else {
			SetPlayerColors (playerO, playerX);
		}

	}

	void SetGameOverText (string whoWin) {
		gameOverText.text = (whoWin == "draw") ? "It's a draw!" : "\"" + whoWin + "\"" + " Wins!";
	}

	public void RestartGame () {
		moveCount = 0;
		gameOverPanel.SetActive (false);
		restartButton.SetActive (false);
		SetPlayerButtons (true);
		SetPlayerColorsInactive ();
		ResetButtonText ();
		startInfo.SetActive (true);
	}

	public void SetBoardInteractable (bool status) {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent<GridSpace> ().button.interactable = status;
		}
	}

	public void ResetButtonText () {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].text = "";
		}
	}

	public void SetPlayerColors (Player newPlayer, Player oldPlayer) {
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	public void SetStartingSide (string startingSide){
		playerSide = startingSide;
		if (playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		}
		else
		{
			SetPlayerColors(playerO, playerX);
		}
		StartGame();
	}

	public void StartGame ()
	{
		SetBoardInteractable (true);
		SetPlayerButtons (false);
		startInfo.SetActive (false);
	}

	void SetPlayerButtons (bool status)
	{
		playerX.button.interactable = status;
		playerO.button.interactable = status;
	}

	void SetPlayerColorsInactive ()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;
	}

}