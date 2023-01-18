using Codex.Pepsi.Abstractions.Accounts.DTOs.Queries;

namespace Codex.Pepsi.Abstractions.Accounts;

public partial interface IChartOfAccounts
{
    TAccount Add<TAccount>(TAccount account) where TAccount : class, IAccount;
    TAccount? Find<TAccount>(Guid id) where TAccount : class, IAccount;
    IAccount? Find(Guid id);
    TAccount[] Get<TAccount>(IGetAccountsQuery query) where TAccount : class, IAccount;
    IAccount[] Get(IGetAccountsQuery query);
    Dictionary<Guid, TAccount> GetAccounts<TAccount>(IEnumerable<Guid> ids) where TAccount : class, IAccount;
    Dictionary<Guid, IAccount> GetAccounts(IEnumerable<Guid> ids);
    void Update<TAccount>(TAccount account) where TAccount : class, IAccount;
    void Update(IAccount account);
    void UpdateRange<TAccount>(IEnumerable<TAccount> account) where TAccount : class, IAccount;
    void UpdateRange(IEnumerable<IAccount> account);

    int SaveChanges();
}
