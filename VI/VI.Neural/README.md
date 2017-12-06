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
    var layer = new LayerCreator(2, 2)  // layer Size and number of connections
        .WithLearningRate(.1f)          // learning rate value 
        .WithMomentum(0)                // momentum value
        .FullSynapse()                  // generate non-zero values to weight matrix
        .Supervised()                   // supervised neuron
        .DenseLayer()                   // operation to manipulate this layer
        .WithLeakRelu()                 // activation function
        .WithSgd()                      // optmizer function
        .Hidden()                       // hidden neuron
        .Build(); 
```
##### 1.3. Getting the output 
--- 
```csharp
    var result = layer.Output(input);
```
##### 1.4. Training
--- 
```csharp
    var error = ((ISupervisedLearning)layer).Learn(input, desired);
```
---

## Current Support

Model | Support
------------ | -------------
[Artificial Neural Network Operations](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/ANNOperations) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Activation Functions](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/ActivationFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Loss Function](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/LossFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Optimizer Function](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/OptimizerFunction) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/ok.png)
[Network](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Network) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/not.png)
[Learning Method](https://github.com/snownz/Virtual-Intelligence/tree/master/VI/VI.Neural/Training) | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/not.png)

## Roadmap
Model|Date
---|---
Learning Method | March, 2018
Network | on March, 2018
