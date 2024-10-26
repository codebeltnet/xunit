using System;
using System.IO;
using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Assets
{
    public class WemoryStream : MemoryStream, IAsyncDisposable
    {
        protected virtual async ValueTask DisposeAsyncCore()
        {
            Dispose(false);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }
    }
}
