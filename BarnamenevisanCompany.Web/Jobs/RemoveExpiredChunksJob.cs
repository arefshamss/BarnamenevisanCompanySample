using Quartz;
using tusdotnet.Interfaces;

namespace BarnamenevisanCompany.Web.Jobs;

[DisallowConcurrentExecution]
public sealed class RemoveExpiredChunksJob : IJob
{
    private readonly ITusExpirationStore _expirationStore;
    
    #region Constructor

    public RemoveExpiredChunksJob(ITusStore tusStore)
    {
        _expirationStore = tusStore as ITusExpirationStore ?? throw new InvalidOperationException("The store must implement ITusTerminationStore.");
    }

    #endregion

    public async Task Execute(IJobExecutionContext context)
    {
        await _expirationStore.RemoveExpiredFilesAsync(context.CancellationToken);
    }
}