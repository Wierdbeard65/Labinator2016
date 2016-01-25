using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labinator2016.Lib.Headers
{
    public enum EventIds
    {
        Starting = 1000,
        Started,
        Stopping,
        Stopped,
        Scanning,
        ConfigCreateFailure,
        RetrieveProjectFailure,
        RetrieveNetworkIDFailure,
        ChangeProjectIPFailure,
        CreateTunnelFailure,
        ScanStarted,
        ScanEnded,
        BeginUpdatingThumbnails,
        EndUpdatingThumbnails,
        BeginSuspendingFinished,
        EndSuspendingFinished,
        BeginSuspendingFuture,
        EndSuspendingFuture,
        BeginSuspendingOutOfHours,
        EndSuspendingOutOfHours,
        BeginStartingClassroom,
        EndStartingClassroom
    }
}
