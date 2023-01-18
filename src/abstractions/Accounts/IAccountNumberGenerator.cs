namespace Codex.Pepsi.Abstractions.Accounts;

public interface IAccountNumberGenerator
{
    string Generate<TAccount>() where TAccount : IAccount;
    Task<string> GenerateAsync<TAccount>() where TAccount : IAccount;
}
