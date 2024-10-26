using System;
using System.IO;
using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Assets
{
    public class AsyncDisposable : Test
    {
        IDisposable _disposableResource = new MemoryStream();
#if NET8_0_OR_GREATER
        IAsyncDisposable _asyncDisposableResource = new MemoryStream();
#else
        IAsyncDisposable _asyncDisposableResource = new WemoryStream();
#endif

        protected override void OnDisposeManagedResources()
        {
            _disposableResource?.Dispose();
            _disposableResource = null;
            DisposableResourceCalled = true;
        }

        public bool DisposableResourceCalled { get; private set; }

        protected override async ValueTask OnDisposeManagedResourcesAsync()
        {
            if (_asyncDisposableResource is not null)
            {
                await _asyncDisposableResource.DisposeAsync().ConfigureAwait(false);
                AsyncDisposableResourceCalled = true;
            }
            _asyncDisposableResource = null;
            OnDisposeManagedResources();
        }

        public bool AsyncDisposableResourceCalled { get; private set; }
    }
}
