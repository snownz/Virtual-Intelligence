# VI.Neural
---
## Parallel Artificial Neural Network.

Library for creating and training neural networks in a simple and fast way.

-------
## Installation

### 1. Installing Neural
 ```
  Install-Package VI.Neural
 ```
#### 2. Installing ILGPU
 ```
  Install-Package ILGPU
 ```

## News
#### *Prototype Networks*
- [Recurrent](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Network/RecurrentNeuralNetwork.cs)
	```csharp
	var rnn = new RecurrentNeuralNetwork(input_size, output_size, hidden_size, learning_rate, std);
	// Forward
	hprev[-1] = new FloatArray(hidden_size);
	for (var t = 0; t < inputs.Length; t++)
		(p[t], hprev[t], _, _ ) = rnn.FeedForward(new FloatArray(inputs[t]), hprev[t - 1]);
	// Backward
	(var loss, var dWxt, var dWtt, var dWhy, var dbh, var dby,_ ) = rnn.BPTT(inputs, target, new FloatArray(hidden_size));
	smooth_loss = rnn.SmoothLoss(loss);
	rnn.UpdateParams(dWxt, dWtt, dWhy, dbh, dby);
    ```
- [LSTM](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Network/LSTM.cs)
	```csharp
	var rnn = new LSTM(input_size, output_size, hidden_size, learning_rate, std);
	// Forward
	hprev[-1] = new FloatArray(hidden_size);
	for (var t = 0; t < inputs.Length; t++)
		(_, _, _, _, cprev[t], _, hprev[t], _,  p[t]) = rnn.FeedForward(new FloatArray(inputs[t]), hprev[t - 1], cprev[t - 1]);
	// Backward
  (var loss, var dWf, var dWi, var dWc, var dWo, var dWv, var dBf, var dBi, var dBc, var dBo, var dBv, var hs, var cs) =
				rnn.BPTT(inputs, targets, new FloatArray(hidden_size), new FloatArray(hidden_size));
	smooth_loss = rnn.SmoothLoss(loss);
	rnn.UpdateParams(dWf, dWi, dWc, dWo, dWv, dBf, dBi, dBc, dBo, dBv);
    ```
----

## Usage

### 1. Neural Level
##### 1.1. Select a Device 
--- 
```csharp
    ProcessingDevice.Device = Device.CPU;
```
##### 1.2. Creating one Layer 
--- 
```csharp
    var layer = LayerCreator(size, connections)  // layer Size and number of connections
				.WithLearningRate(.1f)           // learning rate value 
				.WithMomentum(0)                 // momentum value
				.Supervised_f()					 // setup a supervised layer
                .Hidden_f()						 // setup a hidden | output layer
                .Activation_f()					 // simgle or multiple Layer (1 - N or N - N)
                .LeakRelu_f()					 // activation function
                .AdaGrad_f()					 // optmizator	    
                .FullSynapse(std)			     // std value for weight initialize
                .Build();	
```

or

```csharp
	var layer  = BuildedModels.DenseTanh(connections, size, learningRate, std, EnumOptimizerFunction.Adagrad);
```

##### 1.3. Getting the output 
--- 
```csharp
    var result = layer.FeedForward(input);
```
##### 1.4. Training
--- 
```csharp
    var error = layer.ComputeErrorNBackWard(input, desired);
```
---

## Some Samples
[MNIST](https://github.com/snownz/Virtual-Intelligence/tree/cstruct/VI/VI.Test.MNIST)
[Recurrent Text Writer](https://github.com/snownz/Virtual-Intelligence/tree/cstruct/VI/VI.Test.LSTM.TextWriter)
[LSTM Text Writer](https://github.com/snownz/Virtual-Intelligence/tree/cstruct/VI/VI.Test.Recurrent.TextWriter)

## Current Support

Model | Support
------------ | -------------
[Artificial Neural Network Operations](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/ANNOperations) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Activation Functions](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/ActivationFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Loss Function](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/LossFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Optimizer Function](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/OptimizerFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Network](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Network) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Learning Method](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Training) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/not.png)