using CSharpFunctionalExtensions;
using Shared.SharedKernel.Errors;

namespace Shared.Core.Abstractions.Database;

public interface ITransactionScope : IDisposable
{
    UnitResult<Error> Commit();

    UnitResult<Error> Rollback();
}