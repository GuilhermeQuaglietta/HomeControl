using HomeControl.Core.Infrastructure.Contract;

namespace HomeControl.Core.Infrastructure.Implementation
{
    public class BaseInfrastructure : IBaseInfrastructure
    {
        protected string ConnectionString;

        public virtual void ChangeConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
