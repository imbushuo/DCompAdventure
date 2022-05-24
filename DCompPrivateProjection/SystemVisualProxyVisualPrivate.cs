using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using WinRT;

namespace Microsoft.UI.Composition.Private
{
    public class SystemVisualProxyVisualPrivate : IDisposable, ICustomQueryInterface, IWinRTObject, IDynamicInterfaceCastable, IEquatable<SystemVisualProxyVisualPrivate>
    {
        private static Guid IIDVisual = new Guid("C0EEAB6C-C897-5AC6-A1C9-63ABD5055B9B");

        internal class _ISystemVisualProxyVisualPrivateStatics : IWinRTObject, IDynamicInterfaceCastable
        {
            private IObjectReference _obj;
            private static readonly WeakLazy<_ISystemVisualProxyVisualPrivateStatics> _instance
                = new WeakLazy<_ISystemVisualProxyVisualPrivateStatics>();

            internal static ISystemVisualProxyVisualPrivateStatics Instance => (ISystemVisualProxyVisualPrivateStatics)_instance.Value;
            IObjectReference IWinRTObject.NativeObject => _obj;
            bool IWinRTObject.HasUnwrappableNativeObject => false;

            ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> IWinRTObject.QueryInterfaceCache { get; }
                = new ConcurrentDictionary<RuntimeTypeHandle, IObjectReference>();
            ConcurrentDictionary<RuntimeTypeHandle, object> IWinRTObject.AdditionalTypeData { get; }
                = new ConcurrentDictionary<RuntimeTypeHandle, object>();

            public _ISystemVisualProxyVisualPrivateStatics()
            {
                _obj = new WinRT.BaseActivationFactory("dcompi", "Microsoft.UI.Composition.Private.SystemVisualProxyVisualPrivate")
                    ._As(GuidGenerator.GetIID(typeof(ISystemVisualProxyVisualPrivateStatics).GetHelperType()));
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        private struct InterfaceTag<I>
        {
        }

        private IObjectReference _inner;
        private readonly Lazy<ISystemVisualProxyVisualPrivate> _defaultLazy;
        private readonly Dictionary<Type, object> _lazyInterfaces;
        internal static WinRT.BaseActivationFactory _factory 
            = new WinRT.BaseActivationFactory("dcompi",
                "Microsoft.UI.Composition.Private.SystemVisualProxyVisualPrivate");

        private IntPtr ThisPtr
        {
            get
            {
                if (_inner != null)
                {
                    return _inner.ThisPtr;
                }

                return ((IWinRTObject)this).NativeObject.ThisPtr;
            }
        }
        private ISystemVisualProxyVisualPrivate _default => _defaultLazy.Value;

        bool IWinRTObject.HasUnwrappableNativeObject => true;
        IObjectReference IWinRTObject.NativeObject => _inner;
        ConcurrentDictionary<RuntimeTypeHandle, IObjectReference> IWinRTObject.QueryInterfaceCache { get; }
            = new ConcurrentDictionary<RuntimeTypeHandle, IObjectReference>();
        ConcurrentDictionary<RuntimeTypeHandle, object> IWinRTObject.AdditionalTypeData { get; }
            = new ConcurrentDictionary<RuntimeTypeHandle, object>();

        public static SystemVisualProxyVisualPrivate Create(Compositor compositor)
        {
            return  _ISystemVisualProxyVisualPrivateStatics.Instance.Create(compositor);
        }

        public static I As<I>()
        {
            return _factory.AsInterface<I>();
        }
        public static SystemVisualProxyVisualPrivate FromAbi(IntPtr thisPtr)
        {
            if (thisPtr == IntPtr.Zero)
            {
                return null;
            }

            return MarshalInspectable<SystemVisualProxyVisualPrivate>.FromAbi(thisPtr);
        }
        internal SystemVisualProxyVisualPrivate(IObjectReference objRef)
        {
            _inner = objRef.As(GuidGenerator.GetIID(typeof(ISystemVisualProxyVisualPrivate).GetHelperType()));
            _defaultLazy = new Lazy<ISystemVisualProxyVisualPrivate>(() => (ISystemVisualProxyVisualPrivate)new SingleInterfaceOptimizedObject(typeof(ISystemVisualProxyVisualPrivate), _inner));
            _lazyInterfaces = new Dictionary<Type, object>
            {
                {
                    typeof(IDisposable),
                    new Lazy<IDisposable>(() =>
                        (IDisposable)new SingleInterfaceOptimizedObject(typeof(IDisposable), _inner ?? ((IWinRTObject)this).NativeObject))
                },
                {
                    typeof(ISystemVisualProxyVisualPrivateInterop),
                    new Lazy<ISystemVisualProxyVisualPrivateInterop>(() =>
                        (ISystemVisualProxyVisualPrivateInterop) new SingleInterfaceOptimizedObject(typeof(ISystemVisualProxyVisualPrivateInterop), _inner ?? ((IWinRTObject)this).NativeObject))
                }
            };
        }

        public static bool operator ==(SystemVisualProxyVisualPrivate x, SystemVisualProxyVisualPrivate y)
        {
            return (x?.ThisPtr ?? IntPtr.Zero) == (y?.ThisPtr ?? IntPtr.Zero);
        }
        public static bool operator !=(SystemVisualProxyVisualPrivate x, SystemVisualProxyVisualPrivate y)
        {
            return !(x == y);
        }

        public bool Equals(SystemVisualProxyVisualPrivate other)
        {
            return this == other;
        }
        public override bool Equals(object obj)
        {
            SystemVisualProxyVisualPrivate compositor = obj as SystemVisualProxyVisualPrivate;
            if ((object)compositor != null)
            {
                return this == compositor;
            }

            return false;
        }
        public override int GetHashCode()
        {
            return ThisPtr.GetHashCode();
        }

        private ISystemVisualProxyVisualPrivate AsInternal(InterfaceTag<ISystemVisualProxyVisualPrivate> _)
        {
            return _default;
        }
        private ISystemVisualProxyVisualPrivateInterop AsInternal(InterfaceTag<ISystemVisualProxyVisualPrivateInterop> _)
        {
            return ((Lazy<ISystemVisualProxyVisualPrivateInterop>)_lazyInterfaces[typeof(ISystemVisualProxyVisualPrivateInterop)]).Value;
        }
        private IDisposable AsInternal(InterfaceTag<IDisposable> _)
        {
            return ((Lazy<IDisposable>)_lazyInterfaces[typeof(IDisposable)]).Value;
        }

        public void Dispose()
        {
            AsInternal(default(InterfaceTag<IDisposable>)).Dispose();
        }
        private bool IsOverridableInterface(Guid iid)
        {
            return false;
        }
        CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
        {
            ppv = IntPtr.Zero;
            if (IsOverridableInterface(iid) || typeof(IInspectable).GUID == iid)
            {
                return CustomQueryInterfaceResult.NotHandled;
            }

            if (((IWinRTObject)this).NativeObject.TryAs(iid, out ppv) >= 0)
            {
                return CustomQueryInterfaceResult.Handled;
            }

            return CustomQueryInterfaceResult.NotHandled;
        }

        public Visual AsVisual()
        {
            var x = (this as ICustomQueryInterface).GetInterface(ref IIDVisual, out IntPtr _visual);
            if (x != CustomQueryInterfaceResult.Handled || _visual == IntPtr.Zero) throw new NotImplementedException();
            return Visual.FromAbi(_visual);
        }

        public IntPtr GetHandle()
        {
            return AsInternal(default(InterfaceTag<ISystemVisualProxyVisualPrivateInterop>)).GetHandle();
        }
    }
}
