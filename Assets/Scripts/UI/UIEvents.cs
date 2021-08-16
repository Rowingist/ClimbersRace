using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private UnityEvent _onLevelStarted;
    [SerializeField] private UnityEvent _onBonusStarted;
    [SerializeField] private UnityEvent _onGameWon;
    [SerializeField] private UnityEvent _onGameLost;

    private void OnEnable()
    {
        _level.LevelStarted += () => _onLevelStarted?.Invoke();
        _level.BonusGameStarted += () => _onBonusStarted?.Invoke();
        _level.GameWon += () => _onGameWon?.Invoke();
        _level.GameLost += () => _onGameLost?.Invoke();
    }
    private void OnDisable()
    {
        _level.LevelStarted -= () => _onLevelStarted?.Invoke();
        _level.BonusGameStarted -= () => _onBonusStarted?.Invoke();
        _level.GameWon -= () => _onGameWon?.Invoke();
        _level.GameLost -= () => _onGameLost?.Invoke();
    }
}
