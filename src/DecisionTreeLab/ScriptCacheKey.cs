using System;

namespace DecisionTreeLab
{
    public sealed class ScriptCacheKey : IEquatable<ScriptCacheKey>
    {
        public ScriptCacheKey(string script, Type globals, Type returnType)
        {
            this.Script = script;
            this.GlobalsType = globals;
            this.ReturnType = returnType;
        }
        public string Script { get; }

        public Type GlobalsType { get; }

        public Type ReturnType { get; }

        public override int GetHashCode()
        {
            return this.Script.GetHashCode() ^ this.GlobalsType.GetHashCode() ^ this.ReturnType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            ScriptCacheKey scriptCacheKey = obj as ScriptCacheKey;

            if (scriptCacheKey == null)
            {
                return false;
            }

            return this.Equals(scriptCacheKey);
        }

        public bool Equals(ScriptCacheKey other)
            => this.Script == other.Script &&
            this.GlobalsType == other.GlobalsType &&
            this.ReturnType == other.ReturnType;
    }
}
