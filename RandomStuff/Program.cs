using System;
using Google.Cloud.Storage.V1;


using System.Linq;
using System.Collections.Generic;
using Google.Cloud.Dialogflow.V2;



namespace Rnadomstuff
    {
        class StorageQuickstart
        {

            static void Main(string[] args)
            {
                // Your Google Cloud Platform project ID.
                string projectId = "assiant-pc";
            string sessionId = "123456789";
            string text = "What is the weather in Aalborg";
            String[] texts = new string[] { text };


            DetectIntentFromTexts(projectId, sessionId, texts);



        }

        public static int DetectIntentFromTexts(string projectId,
                                         string sessionId,
                                         string[] texts,
                                         string languageCode = "en-US")
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\Mathias\source\repos\DialogFlowDemoMathias\RandomStuff\json\GoogleCred.json");
            var client = SessionsClient.Create();


            foreach (var text in texts)
            {
                var response = client.DetectIntent(
                    session: new SessionName(projectId, sessionId),
                    queryInput: new QueryInput()
                    {
                        Text = new TextInput()
                        {
                            Text = text,
                            LanguageCode = languageCode
                        }
                    }
                );

                var queryResult = response.QueryResult;

                Console.WriteLine($"Query text: {queryResult.QueryText}");
                if (queryResult.Intent != null)
                {
                    Console.WriteLine($"Intent detected: {queryResult.Intent.DisplayName}");
                }
                Console.WriteLine($"Intent confidence: {queryResult.IntentDetectionConfidence}");
                Console.WriteLine($"Fulfillment text: {queryResult.FulfillmentText}");
                Console.WriteLine();
            }

            return 0;
        }

    }


            


        
    }

