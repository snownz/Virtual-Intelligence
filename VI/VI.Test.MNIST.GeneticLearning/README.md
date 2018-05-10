# Simple implementation of Ann Framework
## Genetic search a Best Ann Achitecture 

#### I'd like to use Artificial Neural Networks to solve MNIST Dataset

### Problem:
> I do not know the best number of hidden layers
>
> How to find out the best?

### Artificial Neural Network Framework
```csharp
    AnnModelFramework();
```
> AnnModelFramework ia ns Abstraction os methods that search the best solution for a problem. Based on Genetic algorithims and search heuristics.

### How to Use?
1. Data
    + Define an heuristic to search a minimal data points inside a dataset to Train our model
    + Define an heuristic to search a minimal data points inside a dataset to Validate our model
2. Genetic Items
    + [Define our Fitness Value from ``` IFitnessValue ``` Interface]()
        > Our Fitness Value is an computed loss
    + [Define a Chromossome that can encode and decode our model informations from ``` GeneBase ```]()
        > In this case ou chromossome just represent a number of hidden layers
        >
        > An simple string array of 8 bits can do it, like as =>  ``` string a = "11111111"; ```, in this case we have 255 hidens units
        >
        > To define the size of each layer we use a logistic regession with:
        >
        >| X | Y |
        >|---|---|
        >|784| 0 |
        >| 10|256|
        >256 because the last layer have 10 units classifiers
        >        
        > With this, we can compute values for layer1, layer2, layer3, layerN
    + [Define a Fitness Function that will compute our model loss from  ``` IFitnessFunctionGeneric<DigitImage> ``` Interface]()
        > We just train the network model and comput teh loss with CrossEntropy
    + [Define an Best Chromossome selector method from  ``` ISelectionBest<DigitImage> ``` Interface]()
        > In this step we define the method to find a best chromossome

### Steps
> After defining all of the methods, we can implement the framework.

1. [Initialize teh framework with a DataType]()
    > ``` var framework = new AnnModelFramework<DigitImage>( 100, dtSelect, dtSelect, ancestral, ff, selectionGenes, selectionBest );  ```

2. [Create dataset]()
    > ``` framework.CreateDatabase( dataset, .3f );  ```

3. [Run to **minimize** ou loss]()
    > ``` framework.RunMinimize( maximalEpoch, minimalFitness );  ```

4. [Get Best model from Framework]()
    > ``` var model = framework.GetModel() as DenseModel;  ```

5. Done!!! Now we have the best model to train and use