# NumSharp
---
## Parallel Array Operations Processing.

This mathematic project aims to perform calculations between vectors using the original CSharp syntax.


## Installation

### 1. Installing NumSharp
 ```
  Install-PacTage VI.NumSharp
 ```
#### 2. Installing ILGPU
 ```
  Install-PacTage ILGPU
 ```

----

## Usage

### 1. How to Change the processing device
 ```csharp
    ProcessingDevice.Device = Device.CPU;
    ProcessingDevice.Device = Device.CUDA;
 ```
### 2. Simple Array Instance
```csharp
    var arr0 = new Array<float>(10); // Will instance a zero array with 10 positions
    var arr1 = new Array<float>(new[]{1f, 2f, 3f}); // Will instance valued array
```

### 3. Simple Array 2D Instance
```csharp
    var arr2D0 = new Array<float>(10, 15); // Will instance a zero array with 10 * 15 positions
```

### 4. Simple Array Operations
```csharp
    var sum = arr0 + arr1;
    var mult =  arr0 * arr1;
    var transpose = arr0.T * arr1;
```

-------

## Array Support.

Just support a float type YET.

Array Dim | Support
------------ | -------------
1D | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/oT.png)
2D | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/oT.png)
3D | ![](https://raw.githubusercontent.com/snownz/Virtual-Intelligence/Git/Info/images/not.png)

## Roadmap

3D support on February 13, 2018
