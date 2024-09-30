using System;
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
                GC.Collect(0, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
            }

            if (unmanaged.TryGetTarget(out var ud2))
            {
                Assert.True(ud2.Disposed);
            }
        }
    }
}
