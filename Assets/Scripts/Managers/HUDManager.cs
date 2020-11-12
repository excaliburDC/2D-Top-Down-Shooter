using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
	[SerializeField]
	private TMP_Text scoreText;

	[SerializeField]
	private TMP_Text hiScoreText;

	[SerializeField]
	private GameObject[] livesGO;

	void OnEnable()
	{
		for (int i = 0; i < livesGO.Length; i++)
		{
			livesGO[i].SetActive(true);
		}
		
	}

	public void UpdateHiScore()
    {
		int hiScore = PlayerPrefs.GetInt("HiScore");
		hiScoreText.text = "HI-SCORE:" + hiScore.ToString();
    }

	/// <summary>
	/// Updates the points.
	/// </summary>
	/// <param name="points">The points.</param>
	public void UpdateScore(int points)
	{
		scoreText.text = "SCORE:"+points.ToString();
	}

	/// <summary>
	/// Updates the lives.
	/// </summary>
	/// <param name="lives">The lives.</param>
	public void UpdateLives(int lives)
	{
		for (int i = 0; i < livesGO.Length; i++)
		{
			if (i >= lives)
			{
				livesGO[i].SetActive(false);
			}
		}
	}
}
