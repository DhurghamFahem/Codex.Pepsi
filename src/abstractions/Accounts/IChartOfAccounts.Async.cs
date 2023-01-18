using Codex.Pepsi.Abstractions.Accounts.DTOs.Queries;

namespace Codex.Pepsi.Abstractions.Accounts;

public partial interface IChartOfAccounts
{
    Task<TAccount> AddAsync<TAccount>(TAccount account) where TAccount : class, IAccount;
    Task<TAccount?> FindAsync<TAccount>(Guid id) where TAccount : class, IAccount;
    Task<IAccount?> FindAsync(Guid id);
    Task<TAccount[]> GetAsync<TAccount>(IGetAccountsQuery query) where TAccount : class, IAccount;
    Task<IAccount[]> GetAsync(IGetAccountsQuery query);
    Task<Dictionary<Guid, TAccount>> GetAccountsAsync<TAccount>(IEnumerable<Guid> ids) where TAccount : class, IAccount;
    Task<Dictionary<Guid, IAccount>> GetAccountsAsync(IEnumerable<Guid> ids);
    Task UpdateAsync<TAccount>(TAccount account) where TAccount : class, IAccount;
    Task UpdateAsync(IAccount account);
    Task UpdateRangeAsync<TAccount>(IEnumerable<TAccount> account) where TAccount : class, IAccount;
    Task UpdateRangeAsync(IEnumerable<IAccount> account);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}