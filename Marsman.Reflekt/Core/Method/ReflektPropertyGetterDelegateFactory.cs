using System;
using System.Reflection;

namespace Marsman.Reflekt
{
    public sealed class ReflektPropertyGetterDelegateFactory
    {
        private readonly Type targetType;
        private static MethodInfo CreateDelegateRecordDefinition = Reflekt<ReflektPropertyGetterDelegateFactory>
                                                                        .Method<DelegateRecord>()
                                                                        .AsGenericDefinition()
                                                                        .Parameters<MethodInfo, Type>(x =>
                                                                            x.CreateDelegateRecord<T1, T2>);

        internal ReflektPropertyGetterDelegateFactory(Type targetType)
        {
            this.targetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public Func<object, object> Create(MethodInfo method)
        {
            var delegateType = FactoryCaches.DelegateTypes.GetOrAdd(method, method =>
            {
                var delegateTypeArguments = new Type[2];
                delegateTypeArguments[0] = targetType;
                delegateTypeArguments[1] = method.ReturnType;
                var delegateType = FactoryCaches.GetFuncType(delegateTypeArguments);
                var factory = CreateDelegateRecordDefinition.MakeGenericMethod(delegateTypeArguments);
                return factory.Invoke(this, new object[] { method, delegateType }) as DelegateRecord;
            });

            return (Func<object, object>)delegateType.WrapperDelegate;
        }

        private DelegateRecord CreateDelegateRecord<Ttarget, Tout>(MethodInfo method, Type delegateType)
        {
            var typedDelegate = Delegate.CreateDelegate(delegateType, method) as Func<Ttarget, Tout>;
            Func<object, object> wrapperDelegate =
                (object target) =>
                    target is Ttarget t ? typedDelegate.Invoke(t) : throw new InvalidOperationException("type mismatch");
            return new DelegateRecord
            {
                DelegateType = delegateType,
                TypedDelegate = typedDelegate,
                WrapperDelegate = wrapperDelegate
            };
        }
    }
}