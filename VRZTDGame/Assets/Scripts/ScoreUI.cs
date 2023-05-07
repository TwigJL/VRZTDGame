using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class ScoreUI : MonoBehaviour
{
   public Text wavesSurvivedText;
   public InputField playerNameInputField;
   public Button submitButton;
   public GameManager gameManager;
   private FirebaseFirestore db;


   void Start()
   {
      // Get the GameManager instance
      gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

      // Initialize Firebase
      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
      {
         FirebaseApp app = FirebaseApp.DefaultInstance;
         db = FirebaseFirestore.DefaultInstance;
         
         submitButton.onClick.AddListener(SubmitScore);
         int wavesSurvived = gameManager.waveCT - 1;
         wavesSurvivedText.text = $"Waves Survived: {wavesSurvived}";
      });
   }

   public void SubmitScore()
   {
      string playerName = playerNameInputField.text;

      if (!string.IsNullOrEmpty(playerName))
      {
         // Prepare the data to be sent to Firestore
         int wavesSurvived = gameManager.waveCT - 1;
         Dictionary<string, object> scoreData = new Dictionary<string, object>
            {
                { "playerName", playerName },
                { "wavesSurvived", wavesSurvived },
            };

         // Send data to Firestore under the "GlobalScores" collection
         db.Collection("GlobalScores").Document(playerName).SetAsync(scoreData).ContinueWithOnMainThread(task =>
         {
            if (task.IsFaulted)
            {
               Debug.LogError("Error adding document: " + task.Exception);
            }
            else
            {
               Debug.Log("Score submitted successfully!");
            }
         });
      }
      else
      {
         Debug.LogError("Player name is empty!");
      }
   }
}
