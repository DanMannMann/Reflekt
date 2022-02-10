using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    internal class MethodTypesMask
    {
        public TypeMask[] Parameters { get; set; }
        public TypeMask Return { get; set; }
        public MethodInfo MethodDefinition { get; set; }
    }

    internal class TypeMask
    {
        public bool IsGeneric { get; set; }
        public int GenericParameterPosition { get; set; }

        public Type Mask<T>(Type[] typeArguments)
            => Mask(this, typeof(T), typeArguments);

        private static Type Mask(TypeMask mask, Type type, Type[] typeArguments)
        {
            if (mask.IsGeneric) return typeArguments[mask.GenericParameterPosition];
            if (mask.Parameters?.Any() == true)
            {
                if (type.GenericTypeArguments.Length != mask.Parameters.Length) throw new InvalidOperationException("param count mismatch");
                var list = new List<(Type type, TypeMask mask)>();
                for (var i = 0; i < mask.Parameters.Length; i++)
                {
                    list.Add((type.GenericTypeArguments[i], mask.Parameters[i]));
                }
                return type.GetGenericTypeDefinition().MakeGenericType(list.Select(x => Mask(x.mask, x.type, typeArguments)).ToArray());
            }
            return type;
        }

        public TypeMask[] Parameters { get; set; }
    }
    
    internal class DelegateRecord
    {
        public Type DelegateType { get; set; }
        public object WrapperDelegate { get; set; }
        public object TypedDelegate { get; set; }
    }

    public class ReflektReturnDelegateFactorySource<Ttarget, Tout>
    {
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;

        internal ReflektReturnDelegateFactorySource(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor)
        {
            _expressionVisitor = expressionVisitor;
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout> Parameterless(Expression<Func<Ttarget, Func<Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1> Parameters<T1>(Expression<Func<Ttarget, Func<T1, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2> Parameters<T1, T2>(Expression<Func<Ttarget, Func<T1, T2, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Func<T1, T2, T3, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7>(_expressionVisitor(selector).Value);
        }

        public ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7, T8> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, T8, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7, T8>(_expressionVisitor(selector).Value);
        }
    }

    public class ReflektReturnDelegate<Ttarget, Tout>
    {
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;
        private readonly Type[] types;

        internal ReflektReturnDelegate(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor, Type[] types)
        {
            if (types == null) throw new InvalidOperationException("Type arguments must be supplied via WithTypeArguments(...) to create a delegate");
            _expressionVisitor = expressionVisitor;
            this.types = types;
        }

        public Func<Ttarget, object> Parameterless(Expression<Func<Ttarget, Func<Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object> Parameters<T1>(Expression<Func<Ttarget, Func<T1, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object> Parameters<T1, T2>(Expression<Func<Ttarget, Func<T1, T2, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Func<T1, T2, T3, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object, object> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, T8, Tout>>> selector)
        {
            return new ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7, T8>(_expressionVisitor(selector).Value)
                            .CreateWithTypeArguments(types);
        }
    }

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[2];
                var factoryTypeArguments = new Type[1];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, Iout>;
            Func<Ttarget, object> wrapperDelegate =
                (Ttarget target) =>
                    typedDelegate.Invoke(target);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, Iout>;
            Func<Ttarget, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, Iout>;
            Func<Ttarget, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, Iout>;
            Func<Ttarget, object, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3, T4>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, I4, Iout>;
            Func<Ttarget, object, object, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3, T4, T5>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, I4, I5, Iout>;
            Func<Ttarget, object, object, object, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3, T4, T5, T6>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
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
                delegateTypeArguments[7] = factoryTypeArguments[6] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, I4, I5, I6, Iout>;
            Func<Ttarget, object, object, object, object, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3, T4, T5, T6, T7>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
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
                delegateTypeArguments[8] = factoryTypeArguments[7] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6, I7, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, I4, I5, I6, I7, Iout>;
            Func<Ttarget, object, object, object, object, object, object, object, object> wrapperDelegate =
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

    public sealed class ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7, T8> : ReflektReturnDelegateFactoryBase
    {
        private MethodInfo method;

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegateFactory<Ttarget, Tout, T1, T2, T3, T4, T5, T6, T7, T8>>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<Tout, T1, T2, T3, T4, T5, T6, T7, T8>);

        internal ReflektReturnDelegateFactory(MethodInfo method)
        {
            this.method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object, object> CreateWithTypeArguments(params Type[] types)
        {
            var typeMasks = GetTypeMasks(method);
            if (types.Length != typeMasks.MethodDefinition.GetGenericArguments().Length) throw new InvalidOperationException("wrong number of type arguments");
            var typedMethod = FactoryCaches.GetTypedMethod(typeMasks.MethodDefinition, types);
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(typedMethod, method =>
            {
                var delegateTypeArguments = new Type[10];
                var factoryTypeArguments = new Type[9];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Parameters[5].Mask<T6>(types);
                delegateTypeArguments[7] = factoryTypeArguments[6] = typeMasks.Parameters[6].Mask<T7>(types);
                delegateTypeArguments[8] = factoryTypeArguments[7] = typeMasks.Parameters[7].Mask<T8>(types);
                delegateTypeArguments[9] = factoryTypeArguments[8] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<I1, I2, I3, I4, I5, I6, I7, I8, Iout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, I1, I2, I3, I4, I5, I6, I7, I8, Iout>;
            Func<Ttarget, object, object, object, object, object, object, object, object, object> wrapperDelegate =
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

    public abstract class ReflektReturnDelegateFactoryBase
    {
        internal MethodTypesMask GetTypeMasks(MethodInfo method)
        {
            return FactoryCaches.ParameterTypeMasks.GetOrAdd(method.IsGenericMethodDefinition ? method : method.GetGenericMethodDefinition(), genericMethodDefinition =>
            {
                var parms = genericMethodDefinition.GetParameters();
                var parameterMasks = new TypeMask[parms.Length];
                for (var i = 0; i < parms.Length; i++)
                {
                    parameterMasks[i] = BuildTypeMask(parms[i].ParameterType);
                }
                var result = new MethodTypesMask
                {
                    Parameters = parameterMasks,
                    Return = BuildTypeMask(genericMethodDefinition.ReturnType),
                    MethodDefinition = genericMethodDefinition
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

    internal static class FactoryCaches
    {
        private static ConcurrentDictionary<int, Type> FuncTypes = new ConcurrentDictionary<int, Type>();
        private static ConcurrentDictionary<int, Type> ActionTypes = new ConcurrentDictionary<int, Type>();
        private static ConcurrentDictionary<int, MethodInfo> TypedMethods = new ConcurrentDictionary<int, MethodInfo>();
        internal static ConcurrentDictionary<MethodInfo, DelegateRecord> DelegateTypes = new ConcurrentDictionary<MethodInfo, DelegateRecord>();
        internal static ConcurrentDictionary<MethodInfo, MethodTypesMask> ParameterTypeMasks = new ConcurrentDictionary<MethodInfo, MethodTypesMask>();

        internal static MethodInfo GetTypedMethod(MethodInfo genericDefinition, params Type[] types)
        {
            var signature = new HashCode();
            signature.Add(genericDefinition.GetHashCode());
            foreach (var type in types) signature.Add(type.GetHashCode());
            return TypedMethods.GetOrAdd(signature.ToHashCode(), hash => genericDefinition.MakeGenericMethod(types));
        }

        internal static Type GetActionType(params Type[] types)
        {
            var delegateTypeSignature = new HashCode();
            for (var i = 0; i < types.Length; i++) delegateTypeSignature.Add(types[i]);
            return ActionTypes.GetOrAdd(delegateTypeSignature.ToHashCode(), x => types.Length switch
            {
                // support up to 9 args - 1 for the TTarget, up to 8 parameters
                01 => typeof(Action<>).MakeGenericType(types),
                02 => typeof(Action<,>).MakeGenericType(types),
                03 => typeof(Action<,,>).MakeGenericType(types),
                04 => typeof(Action<,,,>).MakeGenericType(types),
                05 => typeof(Action<,,,,>).MakeGenericType(types),
                06 => typeof(Action<,,,,,>).MakeGenericType(types),
                07 => typeof(Action<,,,,,,>).MakeGenericType(types),
                08 => typeof(Action<,,,,,,,>).MakeGenericType(types),
                09 => typeof(Action<,,,,,,,,>).MakeGenericType(types),
                _ => throw new InvalidOperationException("Only up to 8 method parameters are supported"),
            });
        }

        internal static Type GetFuncType(params Type[] types)
        {
            var delegateTypeSignature = new HashCode();
            for (var i = 0; i < types.Length; i++) delegateTypeSignature.Add(types[i]);
            return FuncTypes.GetOrAdd(delegateTypeSignature.ToHashCode(), x => types.Length switch
            {
                // support up to 10 args - 1 for the TTarget, up to 8 parameters and one for the return type
                01 => typeof(Func<>).MakeGenericType(types),
                02 => typeof(Func<,>).MakeGenericType(types),
                03 => typeof(Func<,,>).MakeGenericType(types),
                04 => typeof(Func<,,,>).MakeGenericType(types),
                05 => typeof(Func<,,,,>).MakeGenericType(types),
                06 => typeof(Func<,,,,,>).MakeGenericType(types),
                07 => typeof(Func<,,,,,,>).MakeGenericType(types),
                08 => typeof(Func<,,,,,,,>).MakeGenericType(types),
                09 => typeof(Func<,,,,,,,,>).MakeGenericType(types),
                10 => typeof(Func<,,,,,,,,,>).MakeGenericType(types),
                _ => throw new InvalidOperationException("Only up to 8 method parameters are supported"),
            });
        }
    }
}