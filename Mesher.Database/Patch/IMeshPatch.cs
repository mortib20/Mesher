using Microsoft.Extensions.Logging;

namespace Mesher.Database.Patch;

public interface IMeshPatch<in T>
{
    public Task ApplyPatch(MesherContext dbContext, CancellationToken cancellationToken = default);
}