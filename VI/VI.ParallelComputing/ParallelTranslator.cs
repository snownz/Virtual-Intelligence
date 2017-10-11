using ILGPU;
using ILGPU.Backends;
using ILGPU.Compiler;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace VI.ParallelComputing
{
    public class ParallelTranslator : IDisposable
    {
        private Backend _backend;
        private CompileUnit _compileUnit;

        public ParallelTranslator(Accelerator device)
        {
            _backend = device.CreateBackend();
            _compileUnit = device.Context.CreateCompileUnit(_backend);
        }

        public CompiledKernel TranslateMethod(Type source, string methodname)
        {
            // Info: use compiledKernel.GetBuffer() to retrieve the compiled kernel program data
            var method = source.GetMethod(methodname, BindingFlags.Static | BindingFlags.Public);
            return _backend.Compile(_compileUnit, method);
        }
        public IEnumerable<CompiledKernel> TranslateMethod(Type source, IEnumerable<string> methodsname)
        {
            foreach (var methodname in methodsname)
            {
                // Info: use compiledKernel.GetBuffer() to retrieve the compiled kernel program data
                var method = source.GetMethod(methodname, BindingFlags.Public | BindingFlags.Static);
                yield return _backend.Compile(_compileUnit, method);
            }
        }

        public void Dispose()
        {
            _compileUnit.Dispose();
            _backend.Dispose();
        }
    }
}
