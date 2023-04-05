using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject winGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject waveEndPanel;
    [SerializeField] private GameObject lossPanel;
    [SerializeField] private GameObject[] shopBanners;
    [SerializeField] private TMP_Text currentWaveText;
    [SerializeField] private GameObject arrowEndGame;
    [SerializeField] private TMP_Text textEndGame;
    private SpawnBots _spawnManager;
    private PlatformManager _platformManager;
    private bool _isWinGame;

    private void OnEnable()
    {
        _spawnManager = FindObjectOfType<SpawnBots>();
        _spawnManager.OnWavesOver += WinGame;
        _spawnManager.OnWaveEnd += EndWave;
    }

    private void OnDisable()
    {
        _spawnManager.OnWavesOver -= WinGame;
        _spawnManager.OnWaveEnd -= EndWave;
    }

    private void Start()
    {
        _platformManager = FindObjectOfType<PlatformManager>();
        OnPause(false);
        StateGameManager.StateGame = StateGameManager.State.Game;
        if (currentWaveText != null) currentWaveText.text = 1.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && StateGameManager.StateGame == StateGameManager.State.Game)
            SetActivePausePanel(true);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnPause(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnPause(false);
    }

    public void Did()
    {
        OnPause(true);
        StateGameManager.StateGame = StateGameManager.State.Pause;
        lossPanel.SetActive(true);
        shopBanners[Random.Range(0, shopBanners.Length)].gameObject.SetActive(true);
    }

    public void Respawn()
    {
        OnPause(false);
        StateGameManager.StateGame = StateGameManager.State.Game;
        lossPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Life>().Respawn();
    }

    public void SetActivePausePanel(bool value)
    {
        if (value) StateGameManager.StateGame = StateGameManager.State.Pause;
        else StateGameManager.StateGame = StateGameManager.State.Game;
        pausePanel.SetActive(value);
        OnPause(value);
    }

    public void TryRewardRespawn()
    {
        GSConnect.ShowRewardedAd(GSConnect.ContinueReward);
    }

    public void StartWinGamePanel()
    {
        if (!_isWinGame) return;
        GSConnect.ShowMidgameAd();
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(false);
        SetActiveWinPanel(true);
        FindObjectOfType<Level>().NextLevel();
        FindObjectOfType<BattlePassRewarder>(true).RewardPerLevel();
    }

    private void WinGame()
    {
        _isWinGame = true;
        arrowEndGame.gameObject.SetActive(true);
        textEndGame.gameObject.SetActive(true);
    }

    private void SetActiveWinPanel(bool value)
    {
        if (value) StateGameManager.StateGame = StateGameManager.State.GameOver;
        else StateGameManager.StateGame = StateGameManager.State.Game;
        winGamePanel.SetActive(value);
        shopBanners[Random.Range(0, shopBanners.Length)].gameObject.SetActive(true);
        OnPause(value);
    }

    private void EndWave()
    {
        GSConnect.ShowMidgameAd();
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(true);
    }

    private void SetActiveWaveEndPanel(bool value)
    {
        if (value) StateGameManager.StateGame = StateGameManager.State.WaveEnd;
        else StateGameManager.StateGame = StateGameManager.State.Game;
        waveEndPanel.SetActive(value);
        if (!value && currentWaveText != null)
            currentWaveText.text = (int.Parse(currentWaveText.text) + 1).ToString();
        OnPause(value);
    }

    private void OnPause(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        if (!_platformManager.IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}