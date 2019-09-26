using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebhookApi.Controllers
{
    [Route("webhook")]
    public class WebhookController : Controller
    {

        [HttpPost]
        public dynamic PostWithCloudResponse([FromBody] WebhookRequest dialogflowRequest)
        {
            var intentName = dialogflowRequest.QueryResult.Intent.DisplayName;
            var actualQuestion = dialogflowRequest.QueryResult.QueryText;
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'";
            var dialogflowResponse = new WebhookResponse
            {
                FulfillmentText = testAnswer,
                FulfillmentMessages =
                { new Intent.Types.Message
                    { SimpleResponses = new Intent.Types.Message.Types.SimpleResponses
                        { SimpleResponses_ =
                            { new Intent.Types.Message.Types.SimpleResponse
                                {
                                   DisplayText = testAnswer,
                                   TextToSpeech = testAnswer,
                                   //Ssml = $"<speak>{testAnswer}</speak>"
                                }
                            }
                        }
                    }
            }
            };
            var jsonResponse = dialogflowResponse.ToString();
            return new ContentResult { Content = jsonResponse, ContentType = "application/json" }; ;
        }
    }
}
