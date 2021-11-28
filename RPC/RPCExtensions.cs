using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Razorwing.RPC
{
    public static class RPCExtensions
    {
        public static void RPC(this object target, Action method, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, new object[] {});


        public static void RPC<U>(this object target, Func<U> method, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, new object[] { });

        public static void RPC<T0>(this object target, Action<T0> method, T0 arg0, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0);

        public static void RPC<T0, U>(this object target, Func<T0, U> method, T0 arg0, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0);

        public static void RPC<T0, T1>(this object target, Action<T0, T1> method, T0 arg0, T1 arg1, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1);

        public static void RPC<T0, T1, U>(this object target, Func<T0, T1, U> method, T0 arg0, T1 arg1, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1);

        public static void RPC<T0, T1, T2>(this object target, Action<T0, T1, T2> method, T0 arg0, T1 arg1, T2 arg2, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2);

        public static void RPC<T0, T1, T2, U>(this object target, Func<T0, T1, T2, U> method, T0 arg0, T1 arg1, T2 arg2, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2);

        public static void RPC<T0, T1, T2, T3>(this object target, Action<T0, T1, T2, T3> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3);

        public static void RPC<T0, T1, T2, T3, U>(this object target, Func<T0, T1, T2, T3, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3);

        public static void RPC<T0, T1, T2, T3, T4>(this object target, Action<T0, T1, T2, T3, T4> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4);

        public static void RPC<T0, T1, T2, T3, T4, U>(this object target, Func<T0, T1, T2, T3, T4, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4);

        public static void RPC<T0, T1, T2, T3, T4, T5>(this object target, Action<T0, T1, T2, T3, T4, T5> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5);

        public static void RPC<T0, T1, T2, T3, T4, T5, U>(this object target, Func<T0, T1, T2, T3, T4, T5, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6>(this object target, Action<T0, T1, T2, T3, T4, T5, T6> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this object target, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        public static void RPC<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, U>(this object target, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, U> method, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, ExecutingSide side = ExecutingSide.Both)
            => RPCManager.ICall(target, method.Method, side, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
    }
}