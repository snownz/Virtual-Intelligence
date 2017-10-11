using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VI.Maths.ANNArray;
using VI.Maths.LogisticFunctions;

namespace VI.ParallelComputing.ANN
{
    public class CudaAnnInterface<T> : IAnnParallelInterface
        where T : IActivationFunction
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

        public CudaAnnInterface()
        {
            _context = new Context();
            _accelerator = new CudaAccelerator(_context);

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

            var methods = typeof(ANNArrayOperations)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.Name)
                .ToList();

            var compileds = translator
                .TranslateMethod(typeof(ANNArrayOperations), methods)
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
