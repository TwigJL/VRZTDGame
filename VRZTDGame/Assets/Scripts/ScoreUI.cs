using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
   public Text wavesSurvivedText;
   public InputField playerNameInputField;
   public Button submitButton;
   public GameManager gameManager;
   private FirebaseFirestore db;

   void Start()
   {
      gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
      {
         FirebaseApp app = FirebaseApp.DefaultInstance;
         db = FirebaseFirestore.DefaultInstance;

         submitButton.onClick.AddListener(SubmitScore);
         int wavesSurvived = gameManager.waveCT - 1;
         wavesSurvivedText.text = $"Waves Survived: {wavesSurvived}";
      });
   }

   private void OnEnable()
   {
      SetGamePaused(true);
   }

   private void OnDisable()
   {
      SetGamePaused(false);
   }

   private void SetGamePaused(bool isPaused)
   {
      Time.timeScale = isPaused ? 0f : 1f;
   }

   public void SubmitScore()
   {
      string playerName = playerNameInputField.text;

      if (string.IsNullOrEmpty(playerName))
      {
         playerName = "NoName";
      }

      int wavesSurvived = gameManager.waveCT - 1;
      Dictionary<string, object> scoreData = new Dictionary<string, object>
    {
        { "playerName", playerName },
        { "wavesSurvived", wavesSurvived },
    };

      db.Collection("GlobalScores").Document(playerName).SetAsync(scoreData).ContinueWithOnMainThread(task =>
      {
         if (task.IsFaulted)
         {
            Debug.LogError("Error adding document: " + task.Exception);
         }
         else
         {
            Debug.Log("Score submitted successfully!");
            SceneManager.LoadScene("Lobby");
         }
      });

      SetGamePaused(false);
   }

}
