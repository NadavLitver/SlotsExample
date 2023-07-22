using System;
using UnityEngine;
/// <summary>
/// this class is responsible for accessing the player score key, in this case "PlayerPrefs" acts as our model and score handler is our presenter/controller for accessability
/// </summary>
namespace controller
{
    public static class ScoreHandler
    {
        private const string ScoreKey = "PlayerScore";
        private const int defaultScore = 100000;
        public static event Action<int> OnScoreChanged;
        public static int GetScore()
        {
            // Retrieve the player's score from PlayerPrefs, defaulting to defaultScore if not found.
            if (PlayerPrefs.HasKey(ScoreKey))
            {
                return PlayerPrefs.GetInt(ScoreKey, defaultScore);
            }
            else
            {
                SetScore(defaultScore);
                return defaultScore;
            }

        }

        public static void SetScore(int newScore)
        {
            // Save the player's new score to PlayerPrefs.
            OnScoreChanged?.Invoke(newScore);
            PlayerPrefs.SetInt(ScoreKey, newScore);
        }

        public static void AddToScore(int amount)
        {
            // Add an amount to the player's current score.
            int currentScore = GetScore();
            currentScore += amount;
            SetScore(currentScore);
        }
        public static void DeductFromScore(int amount)
        {
            // Deduct an amount to the player's current score.
            int currentScore = GetScore();
            currentScore -= amount;
            SetScore(currentScore);
        }
        public static void ResetScore()
        {
            // Reset the player's score to default.
            SetScore(defaultScore);
        }
    }
}