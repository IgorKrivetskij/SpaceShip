using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAndGameScore : MonoBehaviour
{
    [SerializeField] private Text _currentScoreText;
    [SerializeField] private Text _hiScoreText;
    [SerializeField] private Text _distanceScoreText;
    [SerializeField] private Text _asteroidScoreText;
    [SerializeField] private Text _asteroidsPassedText;
    [SerializeField] private AsteroidCounter _asteroidCounter;
    [SerializeField] private GameObject _finalPanel;
    [SerializeField] private GameObject _startPanel;

    private int _distanceScore;
    private int _asteroidScore;
    private int _hiScore;
    private int _currentScore;
    private float _scoreInFastMooving;
    private bool _isMooveFast;
    private bool _isGameStarted;

    private void Start()
    {
        Time.timeScale = 0;
        _distanceScore = 0;
        _asteroidScore = 0;
        _distanceScoreText.text = (_distanceScore).ToString();
        _asteroidScoreText.text = (_asteroidScore).ToString();
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.anyKey)
            {
                _isGameStarted = true;
                Time.timeScale = 1;
                _startPanel.SetActive(false);
            }            
        }
        if (_isMooveFast)
        {
            _scoreInFastMooving += 2 * Time.deltaTime;
        }
        else
        {
            _distanceScore = (int)Time.time;
        }
        _distanceScoreText.text = (_distanceScore + (int)_scoreInFastMooving).ToString();
    }

    public void ChangeAsteroidScore()
    {
        _asteroidScore = _asteroidCounter.AsteroidCount * 5;
        _asteroidScoreText.text = (_asteroidScore).ToString();
    }

    public void FinalScore()
    {
        _currentScore = _asteroidScore + _distanceScore + (int)_scoreInFastMooving;
        _currentScoreText.text = (_currentScore).ToString();
        _hiScore = PlayerPrefs.GetInt("HiScore");
        if (_hiScore < _currentScore)
        {
            _hiScore = _currentScore;
            PlayerPrefs.SetInt("HiScore", _currentScore);
        }
        _hiScoreText.text = _hiScore.ToString();
        Time.timeScale = 0;
        _asteroidsPassedText.text = "You passed "+ (_asteroidCounter.AsteroidCount).ToString() + " asteroids!";
        _finalPanel.SetActive(true);
    }

    public void FastMoove()
    {
        _isMooveFast = true;
    }

    public void NormalMoove()
    {
        _isMooveFast = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SpaceShip");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
