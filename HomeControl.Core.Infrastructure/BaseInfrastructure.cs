namespace HomeControl.Core.Infrastructure
{
    public class BaseInfrastructure : IBaseInfrastructure
    {
        protected string Endpoint;

        public virtual void ChangeEndpoint(string endpoint)
        {
            Endpoint = endpoint;
        }
    }
}
