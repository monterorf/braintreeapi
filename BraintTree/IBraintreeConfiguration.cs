using Braintree;

namespace BraintTree
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway CreateGateway();
        string GetConfigurationSetting(string setting);
        IBraintreeGateway GetGateway();
    }
}
