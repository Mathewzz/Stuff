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
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        [HttpPost]
        public async Task<JsonResult> GetWebhookResponse()
        {
            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            var pas = request.QueryResult.Parameters;
            var whatShipIsThis = pas.Fields.ContainsKey("ship") && pas.Fields["ship"].ToString().Replace('\"', ' ').Trim().Length > 0;

            var response = new WebhookResponse();

            string name = "Veri nice ship";

            StringBuilder sb = new StringBuilder();

            if (whatShipIsThis)
            {
                sb.Append("The name of this ship is: " + name + "; ");
            }

            if (sb.Length == 0)
            {
                sb.Append("Sry what again?");
            }

            response.FulfillmentText = sb.ToString();

            return Json(response);
        }
    }
}