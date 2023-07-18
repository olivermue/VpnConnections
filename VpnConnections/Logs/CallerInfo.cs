using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VpnConnections.Logs
{
    public static partial class Logging
    {
        private class CallerInfo
        {
            private static readonly CallerInfo Unknown = new CallerInfo();

            private readonly string? _className;
            private readonly Type _declaringType;
            private readonly bool _isUnknown;
            private readonly MethodBase _method;

            public CallerInfo(MethodBase method, string? className)
            {
                _method = Initialize(method, out _declaringType);
                _className = className;
            }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

            private CallerInfo()
            {
                _className = "{Unkown}";
                _isUnknown = true;
            }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

            public string ClassName => _className ?? _declaringType.Name;

            public string MethodName => _isUnknown ? _className! : _method.Name;

            public override string ToString()
            {
                return $"[Method]: {MethodName} [Class]: {ClassName}";
            }

            internal static CallerInfo From(StackFrame? frame, string? className)
            {
                return frame == null
                    ? Unknown
                    : new CallerInfo(frame.GetMethod()!, className);
            }

            private static MethodBase Initialize(MethodBase value, out Type declaringType)
            {
                declaringType = value.DeclaringType!;

                if (declaringType.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false)
                    && (declaringType.IsAssignableTo(typeof(IAsyncStateMachine))
                        || declaringType.IsAssignableTo(typeof(IEnumerator))))
                {
                    TryResolveStateMachineMethod(ref value, out declaringType);
                }

                return value;
            }

            private static bool TryResolveStateMachineMethod(ref MethodBase method, out Type declaringType)
            {
                declaringType = method.DeclaringType!;
                var parentType = declaringType?.DeclaringType;

                if (parentType == null)
                    return false;

                var methods = parentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                if (methods == null)
                    return false;

                foreach (MethodInfo candidateMethod in methods)
                {
                    var attributes = (StateMachineAttribute[])Attribute.GetCustomAttributes(candidateMethod, typeof(StateMachineAttribute), inherit: false);
                    if (attributes == null)
                        continue;

                    var foundAttribute = false;
                    var foundIteratorAttribute = false;

                    foreach (StateMachineAttribute asma in attributes)
                    {
                        if (asma.StateMachineType == declaringType)
                        {
                            foundAttribute = true;
                            foundIteratorAttribute |= asma is IteratorStateMachineAttribute
                                                   || asma is AsyncIteratorStateMachineAttribute;
                        }
                    }

                    if (foundAttribute)
                    {
                        // If this is an iterator (sync or async), mark the iterator as changed, so it gets the + annotation
                        // of the original method. Non-iterator async state machines resolve directly to their builder methods
                        // so aren't marked as changed.
                        method = candidateMethod;
                        declaringType = candidateMethod.DeclaringType!;
                        return foundIteratorAttribute;
                    }
                }

                return false;
            }
        }
    }
}