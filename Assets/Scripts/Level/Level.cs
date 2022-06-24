using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using Agava.YandexGames.Utility;

public class Level : MonoBehaviour
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    [SerializeField] private BonusGame _bonusGame;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Character _playerCharacter;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private bool _bonusLevel = false;

    private bool _isLevelStarted = false;

    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public event UnityAction LevelPreStart;
    public event UnityAction LevelOpenSetting;
    public event UnityAction LevelCloseSetting;
    public event UnityAction LevelStarted;
    public event UnityAction BonusGameStarted;
    public event UnityAction LevelWon;
    public event UnityAction LevelLost;

    private void Start()
    {
        PreStart();
    }

    private void OnEnable()
    {
        _playerCharacter.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        _playerCharacter.Dying -= OnPlayerDying;
    }

    private void OnPlayerDying(Character playerCharacter)
    {
        LoseGame();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("OnApplicationFocus");
        Silence(!hasFocus);
    }

    private void OnApplicationPause(bool isPaused)
    {
        Debug.Log("OnApplicationPause");
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        // Or / And
        AudioListener.volume = silence ? 0 : 0.2f;
    }

    private void Update()
    {
        if (_isLevelStarted)
            return;

        if (_cameraMover.IsStartReached)
            StartLevel();
    }

    public void StartBonusGame(FinishBalk finishBalk, Character character)
    {
        if (character == _playerCharacter)
        {
            _bonusGame.StartBonusGame(finishBalk);
            BonusGameStarted?.Invoke();
        }
        else
        {
            LoseGame();
            _cameraMover.ChangeTarget(character);
        }
    }

    public void NextLevel()
    {
        if (_bonusLevel)
        {
            _sceneChanger.LoadLevel(CurrentLevel);
        }
        else if (CurrentLevel % 5 == 0)
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

            _sceneChanger.LoadBonusLevel();
        }
        else
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);

            _sceneChanger.LoadLevel(CurrentLevel);
        }
    }

    public void RestartLevel()
    {
        if (_bonusLevel)
            _sceneChanger.LoadBonusLevel();
        else
            _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void PreStart()
    {
        LevelPreStart?.Invoke();
    }

    public void StartLevel()
    {
        _isLevelStarted = true;
        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        LevelWon?.Invoke();
    }

    public void LoseGame()
    {
        LevelLost?.Invoke();
    }

    public void OpenSettingMenu()
    {
        LevelOpenSetting?.Invoke();
    }

    public void CloseSettingMenu()
    {
        LevelCloseSetting?.Invoke();
    }

    public void StartShopScene()
    {
        SceneManager.LoadScene(54);
    }
}