﻿using System.Runtime.InteropServices;
using WinRT;

namespace Microsoft.UI.Composition.CompositorCommon
{
    // Partial implementation for IPartner interface, sufficient for what I need to do.
    [Guid("9CBD9312-070d-4588-9bf3-bbf528cf3e84")]
    public unsafe class IPartner : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DelegateOpenSharedVisualFromHandle(IntPtr ptrInstance, IntPtr handle, ref IntPtr ptrVisual);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DelegateCreateSharedVisual(IntPtr ptrInstance, ref IntPtr ptrVisual);

        public static Guid InterfaceId = new Guid("9CBD9312-070d-4588-9bf3-bbf528cf3e84");

        private IntPtr _vftableInternal;

        public IPartner(IntPtr ptr)
        {
            _vftableInternal = ptr;
        }

        public unsafe Visual CreateSharedVisual()
        {
            var ptrCreateSharedVisual = (*(IntPtr*)((*(IntPtr*)_vftableInternal) + (nint)5 * sizeof(void*)));
            var impCreateSharedVisual = Marshal.GetDelegateForFunctionPointer<DelegateCreateSharedVisual>(ptrCreateSharedVisual);

            IntPtr visualPtr = IntPtr.Zero;
            ExceptionHelpers.ThrowExceptionForHR(impCreateSharedVisual(_vftableInternal, ref visualPtr));
            if (visualPtr == IntPtr.Zero) throw new InvalidOperationException();
            return Visual.FromAbi(visualPtr);
        }

        public unsafe Visual OpenShardVisualFromHandle(IntPtr handle)
        {
            var ptrOpenSharedVisualFromHandle = (*(IntPtr*)((*(IntPtr*)_vftableInternal) + (nint)11 * sizeof(void*)));
            var impOpenSharedVisualFromHandle = Marshal.GetDelegateForFunctionPointer<DelegateOpenSharedVisualFromHandle>(ptrOpenSharedVisualFromHandle);

            IntPtr visualPtr = IntPtr.Zero;
            ExceptionHelpers.ThrowExceptionForHR(impOpenSharedVisualFromHandle(_vftableInternal, handle, ref visualPtr));
            if (visualPtr == IntPtr.Zero) throw new InvalidOperationException();
            return Visual.FromAbi(visualPtr);
        }

        public void Dispose()
        {
            if (_vftableInternal != IntPtr.Zero)
            {
                Marshal.Release(_vftableInternal);
                _vftableInternal = IntPtr.Zero;
            }
        }
    }
}
