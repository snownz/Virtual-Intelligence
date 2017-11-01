using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VI.Maths.Array;

namespace VI.ParallelComputing.ANN
{
    public class CpuAnnInterface<T> : IAnnParallelInterface
    {
        private Context _context;
        private Accelerator _accelerator;
        private ParalleExecutorlInterface _interface;

        public ParalleExecutorlInterface Executor
        {
            get
            {
                return _interface;
            }
        }

        public CpuAnnInterface()
        {
            _context = new Context();
            _accelerator = new CPUAccelerator(_context);

            using (var translator = new ParallelTranslator(_accelerator))
            {
                var kernels = ComputeKernels(translator);

                var activation = translator
                    .TranslateMethod(typeof(T), "Function");

                var derivate = translator
                    .TranslateMethod(typeof(T), "Derivative");

                kernels.Add("_activation_", _accelerator.LoadAutoGroupedKernel(activation));
                kernels.Add("_derivative_", _accelerator.LoadAutoGroupedKernel(derivate));

                _interface = new ParalleExecutorlInterface(_accelerator, kernels);
            }
        }

        private Dictionary<string, Kernel> ComputeKernels(ParallelTranslator translator)
        {
            var result = new Dictionary<string, Kernel>();

            var methods = typeof(ANNParallelArrayOperations)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.Name)
                .ToList();

            var compileds = translator
                .TranslateMethod(typeof(ANNParallelArrayOperations), methods)
                .ToList();

            for (int i = 0; i < methods.Count(); i++)
            {
                var kernel = _accelerator.LoadAutoGroupedKernel(compileds[i]);
                result.Add(methods[i], kernel);
            }

            return result;
        }
    }

    public class CpuAnnInterface2<T> : IAnnParallelInterface
    {
        private Context _context;
        private Accelerator _accelerator;
        private ParalleExecutorlInterface _interface;

        public ParalleExecutorlInterface Executor
        {
            get
            {
                return _interface;
            }
        }

        public CpuAnnInterface2()
        {
            _context = new Context();
            _accelerator = new CPUAccelerator(_context);

            using (var translator = new ParallelTranslator(_accelerator))
            {
                var kernels = ComputeKernels(translator);
                _interface = new ParalleExecutorlInterface(_accelerator, kernels);
            }
        }

        private Dictionary<string, Kernel> ComputeKernels(ParallelTranslator translator)
        {
            var result = new Dictionary<string, Kernel>();

            var methods = typeof(T)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.Name)
                .ToList();

            var compileds = translator
                .TranslateMethod(typeof(T), methods)
                .ToList();

            for (int i = 0; i < methods.Count(); i++)
            {
                var kernel = _accelerator.LoadAutoGroupedKernel(compileds[i]);
                result.Add(methods[i], kernel);
            }

            return result;
        }
    }
}
