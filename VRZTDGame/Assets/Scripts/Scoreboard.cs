using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
   public Text listNames;
   public Text listWaves;

   private FirebaseFirestore db;

   void Start()
   {
      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
      {
         FirebaseApp app = FirebaseApp.DefaultInstance;
         db = FirebaseFirestore.DefaultInstance;

         FetchScores();
      });
   }

   private void FetchScores()
   {
      db.Collection("GlobalScores").OrderByDescending("wavesSurvived").GetSnapshotAsync().ContinueWithOnMainThread(task =>
      {
         if (task.IsFaulted)
         {
            Debug.LogError("Error fetching documents: " + task.Exception);
         }
         else
         {
            QuerySnapshot snapshot = task.Result;
            listNames.text = "";
            listWaves.text = "";

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
               Dictionary<string, object> data = document.ToDictionary();
               string playerName = data["playerName"].ToString();
               int wavesSurvived = System.Convert.ToInt32(data["wavesSurvived"]);

               listNames.text += playerName + "\n";
               listWaves.text += wavesSurvived.ToString() + "\n";
            }
         }
      });
   }

}
