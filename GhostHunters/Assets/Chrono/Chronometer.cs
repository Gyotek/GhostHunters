using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    [SerializeField] private float timeChrono = 0;
    [SerializeField] private float timeChronoMin;
    [SerializeField] private float timeChronoSec;
    [SerializeField] private string timeChronoText;

    private int limit = 1;

    public Text chronoText;
    public Text finalScoreText;

    private bool timerEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnd == false)
        {
            timeChrono += Time.deltaTime;

            if (timeChrono >= 10)
            {
                limit = 2;
            }

            if (timeChrono >= 100)
            {
                limit = 3;
            }

            if (timeChrono >= 60)
            {
                timeChronoMin = Mathf.Floor(timeChrono / 60);
                timeChronoSec = timeChrono % 60;

                timeChronoText = timeChronoMin.ToString() + "," + Mathf.RoundToInt(timeChronoSec);

                if (timeChronoSec < 10)
                {
                    timeChronoText = timeChronoMin.ToString() + ",0" + Mathf.RoundToInt(timeChronoSec);
                }
                chronoText.text = timeChronoText;
            }

            else if (timeChrono < 60)
            {
                timeChronoText = timeChrono.ToString();
                timeChronoText = timeChronoText.Substring(0, limit);
                chronoText.text = timeChronoText;
            }
        }
    }

    public void SetActive()
    {
        this.gameObject.SetActive(true);
        finalScoreText.text = timeChronoText;
        timerEnd = true;
    }
}
