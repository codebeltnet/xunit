using System.IO;

namespace Codebelt.Extensions.Xunit.Assets
{
    public class ManagedDisposable : Test
    {
        public ManagedDisposable()
        {
            Stream = new MemoryStream();
        }

        public MemoryStream Stream { get; private set; }

        protected override void OnDisposeManagedResources()
        {
            try
            {
                Stream?.Dispose();
            }
            finally
            {
                Stream = null;
            }
        }
    }
}
