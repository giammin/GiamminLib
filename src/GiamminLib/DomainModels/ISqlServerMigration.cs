using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GiamminLib.DomainModels;

public interface ISqlServerMigration
{
    Task<bool> IsUpdateAvailable(int dbSchemaVersion, CancellationToken cancellationToken);
    Task InstallAsync(string sqlScript, int dbSchemaVersion, CancellationToken cancellationToken);
    Task InstallAsync(int dbSchemaVersion, Assembly assembly, string scriptResourceFullQualifiedName, CancellationToken cancellationToken);
    Task InstallAsync(int dbSchemaVersion, Stream dbInstallScriptStream, CancellationToken cancellationToken);
}