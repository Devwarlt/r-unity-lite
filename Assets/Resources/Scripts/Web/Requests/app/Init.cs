namespace Assets.Resources.Scripts.Web.Requests.app
{
    public class Init : GameWebRequest
    {
        public override void Configure()
        {
            method = GameWebMethod.post;
            request = "app/init";
        }
    }
}