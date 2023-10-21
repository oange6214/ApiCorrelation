using ApiCorrelation.Configurations.Interfaces;

namespace ApiCorrelation.Configurations;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private string _correlateionId = Guid.NewGuid().ToString();

    public string Get() => _correlateionId;

    public void Set(string correlationId) => _correlateionId = correlationId;
}
