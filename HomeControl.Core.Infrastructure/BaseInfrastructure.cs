using HomeControl.Core.Infrastructure.Contract;

namespace HomeControl.Core.Infrastructure.Implementation
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
