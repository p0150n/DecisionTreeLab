using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DecisionTreeLab
{
    public class ExpressionExecutor : IExpressionExecutor
    {
        private readonly ConcurrentDictionary<ScriptCacheKey, Script> scriptsChache;
        private readonly ScriptOptions scriptOptions;
        private readonly List<Assembly> references;

        public ExpressionExecutor(
            IEnumerable<Assembly> references,
            IEnumerable<string> imports)
        {
            this.references = references.ToList();
            this.references.Add(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderInternalCompilerException).Assembly);
            this.scriptOptions = ScriptOptions
                 .Default
                 .WithReferences(this.references)
                 .WithImports(imports);

            this.scriptsChache = new ConcurrentDictionary<ScriptCacheKey, Script>();
        }

        public Task Run<TContextType>(
            string scriptText,
            TContextType context,
            params object[] variables)
            => this.Run<TContextType, object>(scriptText, context, variables);

        public async Task<TReturnType> Run<TContextType, TReturnType>(
            string scriptText,
            TContextType context,
            params object[] variables)
        {
            Script<TReturnType> script = this.CompileScript<TReturnType, Globals<TContextType>>(scriptText);
            Globals<TContextType> globals = new Globals<TContextType>(context, variables);
            ScriptState<TReturnType> scriptRunResult =
                await script.RunAsync(globals)
                .ConfigureAwait(false);

            return scriptRunResult.ReturnValue;
        }

        private Script<ReturnType> CompileScript<ReturnType, TGlobalsType>(string scriptText)
        {
            ScriptCacheKey scriptCacheKey = new ScriptCacheKey(scriptText, typeof(TGlobalsType), typeof(ReturnType));
            if (this.scriptsChache.TryGetValue(scriptCacheKey, out Script script))
            {
                return (Script<ReturnType>)script;
            }

            script = this.CompileScriptCore<ReturnType, TGlobalsType>(scriptText);
            script = this.scriptsChache.GetOrAdd(scriptCacheKey, script);

            return (Script<ReturnType>)script;
        }

        private Script<ReturnType> CompileScriptCore<ReturnType, TGlobalsType>(string scriptText)
        {
            Script<ReturnType> script = CSharpScript.Create<ReturnType>(scriptText, this.scriptOptions, typeof(TGlobalsType));
            ImmutableArray<Diagnostic> compilationResult = script.Compile();

            return script;
        }
    }
}
