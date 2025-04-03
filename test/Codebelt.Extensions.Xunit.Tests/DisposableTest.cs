using System;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Assets;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit
{
    public class DisposableTest : Test
    {
        public DisposableTest(ITestOutputHelper output) : base(output)
        {
        }

#if NET8_0_OR_GREATER
        [Fact]
        public async Task AsyncDisposable_VerifyThatAssetIsBeingDisposed()
        {
            AsyncDisposable ad = new AsyncDisposable();
            await using (ad.ConfigureAwait(false))
            {
                Assert.NotNull(ad);
                Assert.False(ad.DisposableResourceCalled);
                Assert.False(ad.AsyncDisposableResourceCalled);
            }

            Assert.NotNull(ad);
            Assert.True(ad.DisposableResourceCalled);
            Assert.True(ad.AsyncDisposableResourceCalled);
        }
#else
        [Fact]
        public async Task AsyncDisposable_VerifyThatAssetIsBeingDisposed()
        {
            AsyncDisposable ad = new AsyncDisposable();

            Assert.NotNull(ad);
            Assert.False(ad.DisposableResourceCalled);
            Assert.False(ad.AsyncDisposableResourceCalled);

            await ad.DisposeAsync().ConfigureAwait(false);

            Assert.NotNull(ad);
            Assert.True(ad.DisposableResourceCalled);
            Assert.True(ad.AsyncDisposableResourceCalled);
        }
#endif

        [Fact]
        public void ManagedDisposable_VerifyThatAssetIsBeingDisposed()
        {
            ManagedDisposable mdRef = null;
            using (var md = new ManagedDisposable())
            {
                mdRef = md;
                Assert.NotNull(md.Stream);
                Assert.Equal(0, md.Stream.Length);
                Assert.False(mdRef.Disposed);
            }
            Assert.NotNull(mdRef);
            Assert.Null(mdRef.Stream);
            Assert.True(mdRef.Disposed);
        }

        private WeakReference<UnmanagedDisposable> unmanaged = null;

        [Fact]
        public void UnmanagedDisposable_VerifyThatAssetIsBeingDisposedOnFinalize()
        {
            Action body = () =>
            {
                var o = new UnmanagedDisposable();
                Assert.NotEqual(IntPtr.Zero, o._libHandle);
                Assert.NotEqual(IntPtr.Zero, o._handle);
                unmanaged = new WeakReference<UnmanagedDisposable>(o, true);
            };

            try
            {
                body();
            }
            finally
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
                Task.Delay(500).Wait(); // Add a small delay
            }

            if (unmanaged.TryGetTarget(out var ud2))
            {
                Assert.True(ud2.Disposed);
            }
        }
    }
}
