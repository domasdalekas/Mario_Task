using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float score = scoreManager.GetCurrentTime();
        scoretext.text = score.ToString("0");
    }
}
