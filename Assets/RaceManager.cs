using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{

    public Enemy[] enemies;
    public ParticleSystem startingLine;
    public bool hasRaceStarted;
    public TextMeshProUGUI raceNumbers;
    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();

    }

    public float timeRemaining = 10;
    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0 && !hasRaceStarted)
        {
            timeRemaining -= Time.deltaTime;
            raceNumbers.text = timeRemaining.ToString();
        }else if (!hasRaceStarted)
        {
            raceNumbers.text = "GO";
            startingLine.gameObject.SetActive(false);
            hasRaceStarted = true;
            foreach (Enemy enemy in enemies)
                enemy.StartRace();
        }
    }
}
