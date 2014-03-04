using System.AddIn.Contract;
using System.Security.Permissions;

namespace RedGate.AppHost.Interfaces
{
    /// <remarks>
    /// To work around http://support.microsoft.com/kb/982638
    /// </remarks>
    public interface INativeHandleContractWithoutIntPtr : IContract
    {
        /// <remarks>
        /// Akin to <seealso cref="INativeHandleContract.GetHandle"/>
        /// </remarks>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        long GetHandle();
    }
}