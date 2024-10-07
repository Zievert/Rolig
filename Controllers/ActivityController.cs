using Google.Cloud.SecretManager.V1;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class ActivityController : Controller
    {
        public ActivityController()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                // Hent tjenestekonto-nøkkelen fra Google Cloud Secret Manager
                string serviceAccountKeyJson = GetSecret("projects/916113251154/secrets/firebase-service-account-key/versions/latest");

                // Initialiser FirebaseAdmin SDK med tjenestekonto-nøkkelen
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(serviceAccountKeyJson)
                });
            }
        }

        private string GetSecret(string secretName)
        {
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            AccessSecretVersionResponse result = client.AccessSecretVersion(secretName);
            return result.Payload.Data.ToStringUtf8();
        }

        public IActionResult Index()
        {
            return View();
        }

        // Resten av metoden og logikken for aktivitetshåndtering...
    }
}
