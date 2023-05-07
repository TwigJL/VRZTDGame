using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;

public class TestFirebase : MonoBehaviour
{
   private FirebaseFirestore db;

   // Start is called before the first frame update
   async void Start()
   {
      await InitializeFirebase();
      await TestConnection();
   }

   private async Task InitializeFirebase()
   {
      var initializationTask = FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
      {
         FirebaseApp.Create();
         db = FirebaseFirestore.DefaultInstance;
      });

      // Wait for the initialization to complete
      await initializationTask;

      // Give some time for Firebase to initialize
      await Task.Delay(1000);
   }

   private async Task TestConnection()
   {
      // Save a sample document to Firestore
      DocumentReference docRef = db.Collection("testConnection").Document("connectionTest");
      Dictionary<string, object> testDocument = new Dictionary<string, object>
        {
            { "timestamp", System.DateTime.UtcNow }
        };
      await docRef.SetAsync(testDocument).ContinueWith(task =>
      {
         if (task.IsFaulted)
         {
            Debug.LogError("Error saving document: " + task.Exception);
         }
         else
         {
            Debug.Log("Document saved successfully.");
         }
      });

      // Retrieve the document from Firestore
      await docRef.GetSnapshotAsync().ContinueWith(task =>
      {
         if (task.IsFaulted)
         {
            Debug.LogError("Error retrieving document: " + task.Exception);
         }
         else if (task.IsCompleted)
         {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
               Debug.Log("Document data: " + snapshot.ToDictionary());
            }
            else
            {
               Debug.LogError("Document not found.");
            }
         }
      });
   }

   // Update is called once per frame
   void Update()
   {

   }
}
