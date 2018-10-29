using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreList : MonoBehaviour {

    [SerializeField] ScoreBar[] scoreBars;
    [Header("Caches")]
    [SerializeField] Options options;
    [SerializeField] List<ScoreItem> scoreRecords;

	// Use this for initialization
	void Start () {
        InicializeData();
        ProcessScoreBars();
    }

    private void ProcessScoreBars() {
        for (int i = 0; i < scoreBars.Length; i++) {
            if (i < scoreRecords.Count) {
                if (scoreBars[i].dateText) scoreBars[i].dateText.text = buildDate(scoreRecords[i]);
                if (scoreBars[i].scoreText) scoreBars[i].scoreText.text = scoreRecords[i].Score.ToString() + " pts";
                if (scoreBars[i].levelText) scoreBars[i].levelText.text = "Lvl. " + scoreRecords[i].HighestLevel;
            } else scoreBars[i].gameObject.SetActive(false);
        }
    }

    private string buildDate(ScoreItem score) {
        return score.Date.Day + "."
            + score.Date.Month + "."
            + score.Date.Year;
            ;
    }

    private void InicializeData() {
        options = FindObjectOfType<Options>();
        scoreBars = GetComponentsInChildren<ScoreBar>();
        if (options) scoreRecords = options.GetScoreItems();
        else if (!options) print("ScoreList/inicializeData: missing Options");
    }

    public void ExpandScores() {
        GetComponent<Animator>().SetBool(triggers.SHOW_UP, true);
    }

    public void CollapseScores() {
        GetComponent<Animator>().SetBool(triggers.SHOW_UP, false);
    }

}
