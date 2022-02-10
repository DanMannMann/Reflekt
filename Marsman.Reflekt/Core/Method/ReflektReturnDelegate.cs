using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ReflektReturnDelegate<Ttarget, Tout>
    {
        private class DelegateRecord
        {
            public Type DelegateType { get; set; }
            public object WrapperDelegate { get; set; }
            public object TypedDelegate { get; set; }
        }

        private class MethodTypesMask
        {
            public TypeMask[] Parameters { get; set; }
            public TypeMask Return { get; set; }
        }

        private static ConcurrentDictionary<MethodInfo, DelegateRecord> delegateTypes = new ConcurrentDictionary<MethodInfo, DelegateRecord>();
        private static ConcurrentDictionary<int, Type> funcTypes = new ConcurrentDictionary<int, Type>();
        private static ConcurrentDictionary<MethodInfo, MethodTypesMask> parameterTypeMasks = new ConcurrentDictionary<MethodInfo, MethodTypesMask>();
        private readonly Func<Expression, MethodReflektor<Ttarget>> _expressionVisitor;
        private readonly Type[] types;

        internal ReflektReturnDelegate(Func<Expression, MethodReflektor<Ttarget>> expressionVisitor, Type[] types)
        {
            if (types == null) throw new InvalidOperationException("Type arguments must be supplied via WithTypeArguments(...) to create a delegate");
            _expressionVisitor = expressionVisitor;
            this.types = types;
        }

        static Type GetFuncType(params Type[] types)
        {
            var delegateTypeSignature = new HashCode();
            for (var i = 0; i < types.Length; i++) delegateTypeSignature.Add(types[i]);
            return funcTypes.GetOrAdd(delegateTypeSignature.ToHashCode(), x => types.Length switch
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

        public Func<Ttarget, object> Parameterless(Expression<Func<Ttarget, Func<Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[2];
                var factoryTypeArguments = new Type[1];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<TReturn>);
        private DelegateRecord CreateDelegateRecord<Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, Treturn>;
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

        public Func<Ttarget, object, object> Parameters<T1>(Expression<Func<Ttarget, Func<T1, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[3];
                var factoryTypeArguments = new Type[2];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition1.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition1 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, Treturn>;
            Func<Ttarget, object, object> wrapperDelegate =
                (Ttarget target, object o1) =>
                    typedDelegate.Invoke(target, (T1)o1);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object> Parameters<T1, T2>(Expression<Func<Ttarget, Func<T1, T2, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[4];
                var factoryTypeArguments = new Type[3];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition2.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition2 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, Treturn>;
            Func<Ttarget, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object> Parameters<T1, T2, T3>(Expression<Func<Ttarget, Func<T1, T2, T3, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[5];
                var factoryTypeArguments = new Type[4];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition3.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition3 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, Treturn>;
            Func<Ttarget, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object, object> Parameters<T1, T2, T3, T4>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[6];
                var factoryTypeArguments = new Type[5];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition4.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition4 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, T4, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, T4, Treturn>;
            Func<Ttarget, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3, (T4)o4);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
                var delegateTypeArguments = new Type[7];
                var factoryTypeArguments = new Type[6];
                delegateTypeArguments[0] = typeof(Ttarget);
                delegateTypeArguments[1] = factoryTypeArguments[0] = typeMasks.Parameters[0].Mask<T1>(types);
                delegateTypeArguments[2] = factoryTypeArguments[1] = typeMasks.Parameters[1].Mask<T2>(types);
                delegateTypeArguments[3] = factoryTypeArguments[2] = typeMasks.Parameters[2].Mask<T3>(types);
                delegateTypeArguments[4] = factoryTypeArguments[3] = typeMasks.Parameters[3].Mask<T4>(types);
                delegateTypeArguments[5] = factoryTypeArguments[4] = typeMasks.Parameters[4].Mask<T5>(types);
                delegateTypeArguments[6] = factoryTypeArguments[5] = typeMasks.Return.Mask<Tout>(types);
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition5.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition5 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, T4, T5, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, T4, T5, Treturn>;
            Func<Ttarget, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3, (T4)o4, (T5)o5);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
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
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition6.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition6 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, T4, T5, T6, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, T4, T5, T6, Treturn>;
            Func<Ttarget, object, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3, (T4)o4, (T5)o5, (T6)o6);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
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
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition7.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition7 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, T4, T5, T6, T7, Treturn>;
            Func<Ttarget, object, object, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6, object o7) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3, (T4)o4, (T5)o5, (T6)o6, (T7)o7);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        public Func<Ttarget, object, object, object, object, object, object, object, object, object> Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<Ttarget, Func<T1, T2, T3, T4, T5, T6, T7, T8, Tout>>> selector)
        {
            var delegateType = delegateTypes.GetOrAdd(_expressionVisitor(selector).Value, method =>
            {
                var typeMasks = GetTypeMasks(method);
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
                var delegateType = GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition8.MakeGenericMethod(factoryTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<Ttarget, object, object, object, object, object, object, object, object, object>)delegateType.WrapperDelegate;
        }

        private static MethodInfo CreateDelegateRecordDefinition8 = Reflekt<ReflektReturnDelegate<Ttarget, Tout>>
                                                                        .Method<DelegateRecord>()
                                                                        .GenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x => 
                                                                            x.CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>);
        private DelegateRecord CreateDelegateRecord<T1, T2, T3, T4, T5, T6, T7, T8, Treturn>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, T1, T2, T3, T4, T5, T6, T7, T8, Treturn>;
            Func<Ttarget, object, object, object, object, object, object, object, object, object> wrapperDelegate =
                (Ttarget target, object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8) =>
                    typedDelegate.Invoke(target, (T1)o1, (T2)o2, (T3)o3, (T4)o4, (T5)o5, (T6)o6, (T7)o7, (T8)o8);
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }

        private static MethodTypesMask GetTypeMasks(MethodInfo method)
        {
            return parameterTypeMasks.GetOrAdd(method.GetGenericMethodDefinition(), genericMethodDefinition =>
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
                    Return = BuildTypeMask(genericMethodDefinition.ReturnType)
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