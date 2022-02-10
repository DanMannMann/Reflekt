using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{

    public class ReflektVoidDelegate<Ttarget>
    {
        private class ActionRecord
        {
            public Type ActionType { get; set; }
            public object Delegate { get; set; }
        }
        private static ConcurrentDictionary<int, ActionRecord> funcTypes = new ConcurrentDictionary<int, ActionRecord>();
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;
        private readonly Type[] types;

        internal ReflektVoidDelegate(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor, Type[] types)
        {
            if (types == null) throw new InvalidOperationException("Type arguments must be supplied via WithTypeArguments(...) to create a delegate");
            _expressionVisitor = expressionVisitor;
            this.types = types;
        }

        object GetTypedDelegate(MethodInfo method)
        {
            var delegateTypes = new Type[types.Length + 1];
            var delegateTypeSignature = new HashCode();
            delegateTypeSignature.Add(delegateTypes[0] = typeof(Ttarget));
            for (var i = 1; i <= types.Length; i++) delegateTypeSignature.Add(delegateTypes[i] = types[i - 1]);

            var funcType = funcTypes.GetOrAdd(delegateTypeSignature.ToHashCode(), x =>
            {
                var type = GetActionType(delegateTypes);
                var func = Delegate.CreateDelegate(type, method);
                return new ActionRecord
                {
                    ActionType = type,
                    Delegate = func
                };
            });
            return funcType.Delegate;
        }

        static Type GetActionType(params Type[] types)
        {
            switch (types.Length)
            {
                // support up to 9 args - 1 for the TTarget and up to 8 parameters
                case 01: return typeof(Action<>).MakeGenericType(types);
                case 02: return typeof(Action<,>).MakeGenericType(types);
                case 03: return typeof(Action<,,>).MakeGenericType(types);
                case 04: return typeof(Action<,,,>).MakeGenericType(types);
                case 05: return typeof(Action<,,,,>).MakeGenericType(types);
                case 06: return typeof(Action<,,,,,>).MakeGenericType(types);
                case 07: return typeof(Action<,,,,,,>).MakeGenericType(types);
                case 08: return typeof(Action<,,,,,,,>).MakeGenericType(types);
                case 09: return typeof(Action<,,,,,,,,>).MakeGenericType(types);
            }
            throw new InvalidOperationException("Only up to 8 method parameters are supported");
        }

        public Action<Ttarget> Parameterless(Expression<Func<Ttarget, Action>> selector)
        {
            return (Action<Ttarget>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object> Parameters<T1>(Expression<Func<Ttarget, Action<T1>>> selector)
        {
            return (Action<Ttarget, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object> Parameters<T1, T2>(Expression<Func<Ttarget, Action<T1, T2>>> selector)
        {
            return (Action<Ttarget, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Action<T1, T2, T3>>> selector)
        {
            return (Action<Ttarget, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object, object> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Action<T1, T2, T3, T4>>> selector)
        {
            return (Action<Ttarget, object, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5>>> selector)
        {
            return (Action<Ttarget, object, object, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6>>> selector)
        {
            return (Action<Ttarget, object, object, object, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7>>> selector)
        {
            return (Action<Ttarget, object, object, object, object, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }

        public Action<Ttarget, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7, T8>>> selector)
        {
            return (Action<Ttarget, object, object, object, object, object, object, object, object>)GetTypedDelegate(_expressionVisitor(selector).Value);
        }
    }
}