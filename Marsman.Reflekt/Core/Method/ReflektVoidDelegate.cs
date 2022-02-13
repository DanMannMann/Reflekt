using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{


    public class ReflektVoidDelegateFactorySource<Ttarget>
    {
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;

        internal ReflektVoidDelegateFactorySource(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor)
        {
            _expressionVisitor = expressionVisitor;
        }

        public ReflektVoidDelegateFactory<Ttarget> Parameterless(Expression<Func<Ttarget, Action>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1> Parameters<T1>(Expression<Func<Ttarget, Action<T1>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2> Parameters<T1, T2>(Expression<Func<Ttarget, Action<T1, T2>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Action<T1, T2, T3>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Action<T1, T2, T3, T4>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7>(_expressionVisitor(selector).Value);
        }

        public ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7, T8>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8>(_expressionVisitor(selector).Value);
        }
    }

    public class ReflektVoidDelegate<Ttarget>
    {
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;
        private readonly Type[] types;

        internal ReflektVoidDelegate(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor, Type[] types)
        {
            if (types == null) throw new InvalidOperationException("Type arguments must be supplied via WithTypeArguments(...) to create a delegate");
            _expressionVisitor = expressionVisitor;
            this.types = types;
        }

        public Action<Ttarget> Parameterless(Expression<Func<Ttarget, Action>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object> Parameters<T1>(Expression<Func<Ttarget, Action<T1>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object> Parameters<T1, T2>(Expression<Func<Ttarget, Action<T1, T2>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Action<T1, T2, T3>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object, object> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Action<T1, T2, T3, T4>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Action<Ttarget, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Action<T1, T2, T3, T4, T5, T6, T7, T8>>> selector)
        {
            return new ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[1];
                delegateTypeArguments[0] = typeof(Ttarget);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments); 

                // special case for no params - no late binding needed!
                var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget>;
                return new DelegateRecord
                {
                    DelegateType = delegateType,
                    TypedDelegate = typedDelegate,
                    WrapperDelegate = typedDelegate
                };
            });

            return (Action<Ttarget>)delegateType.WrapperDelegate;
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[2];
                var factoryTypeArguments = new Type[1];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1>;
            Action<Ttarget, object> wrapperDelegate =
                (Ttarget target, object o1) =>
                    typedDelegate.Invoke(target, (I1)o1);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[3];
                var factoryTypeArguments = new Type[2];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2>;
            Action<Ttarget, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[4];
                var factoryTypeArguments = new Type[3];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3>;
            Action<Ttarget, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[5];
                var factoryTypeArguments = new Type[4];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3, I4>;
            Action<Ttarget, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3, (I4)o4);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[6];
                var factoryTypeArguments = new Type[5];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3, I4, I5>;
            Action<Ttarget, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3, (I4)o4, (I5)o5);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[7];
                var factoryTypeArguments = new Type[6];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Parameters[5].Mask<T6>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3, I4, I5, I6>;
            Action<Ttarget, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3, (I4)o4, (I5)o5, (I6)o6);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[8];
                var factoryTypeArguments = new Type[7];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Parameters[5].Mask<T6>(types);
                delegateTypeArguments[7] = factoryTypeArguments[6] = typeMasks.Parameters[6].Mask<T7>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6, I7>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3, I4, I5, I6, I7>;
            Action<Ttarget, object, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6, object o7) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3, (I4)o4, (I5)o5, (I6)o6, (I7)o7);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8> : ReflektVoidDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektVoidDelegateFactory<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7, T8>);

        internal ReflektVoidDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Action<Ttarget, object, object, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = method.IsGenericMethod ? GetTypeMasks(method) : null;
            if (types.Length != (typeMasks?.MethodDefinition.GetGenericArguments().Length ?? 0)) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[9];
                var factoryTypeArguments = new Type[8];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Parameters[5].Mask<T6>(types);
                delegateTypeArguments[7] = factoryTypeArguments[6] = typeMasks.Parameters[6].Mask<T7>(types);
                delegateTypeArguments[8] = factoryTypeArguments[7] = typeMasks.Parameters[7].Mask<T8>(types);
                var delegateType = FactoryCaches.GetActionType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Action<Ttarget, object, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6, I7, I8>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Action<Ttarget, I1, I2, I3, I4, I5, I6, I7, I8>;
            Action<Ttarget, object, object, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8) =>
                    typedDelegate.Invoke(target, (I1)o1, (I2)o2, (I3)o3, (I4)o4, (I5)o5, (I6)o6, (I7)o7, (I8)o8);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public abstract class ReflektVoidDelegateFactoryBase
    {
        internal MethodTypesMask GetTypeMasks(MethodInfo method)
        {
            method = !method.IsGenericMethod || method.IsGenericMethodDefinition
                        ? method
                        : method.GetGenericMethodDefinition();
            return FactoryCaches.ParameterTypeMasks.GetOrAdd(method, m =>
            {
                var parms = m.GetParameters();
                var parameterMasks = new TypeMask[parms.Length];
                for (var i = 0; i < parms.Length; i++)
                {
                    parameterMasks[i] = BuildTypeMask(parms[i].ParameterType);
                }
                var result = new MethodTypesMask
                {
                    Parameters = parameterMasks,
                    Return = BuildTypeMask(m.ReturnType),
                    MethodDefinition = m
                };
                return result;
            });
        }

        private static TypeMask BuildTypeMask(Type parm)
        {
            if (parm.IsGenericMethodParameter)
            {
                return new TypeMask { IsGeneric = true, GenericParameterPosition = parm.GenericParameterPosition };
            }
            else
            {
                var result = new TypeMask { IsGeneric = false, GenericParameterPosition = -1 };
                if (parm.IsGenericType)
                {
                    result.Parameters = parm.GenericTypeArguments.Select(x => BuildTypeMask(x)).ToArray();
                }
                return result;
            }
        }
    }
}