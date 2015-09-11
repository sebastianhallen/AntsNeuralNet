Imports System.Collections.ArrayList
Public Class NeuralNetwork
    Private mLayers As ArrayList
    Private mInputLayer As NeuronLayer
    Private mOutputLayer As NeuronLayer

    'Creates a neural network.
    'Inputs: The number of neurons in the input layer
    'Outputs: The number of neurons in the output layer
    'HiddenLayers:  The number of Hidden layers in the network.  This can be 0 or greater.
    'NeuronsPerHiddenLayer:  The number of neurons in each hidden layer.
    'InputWeightSize:  The number of weights in the input layer.  The weights size for each layer above is the number of neurons in the previous layer.
    'ActivationFunction:  The activation function to use in the neuron.
    Public Sub New(ByVal Inputs As Integer, ByVal Outputs As Integer, ByVal HiddenLayers As Integer, ByVal NeuronsPerHiddenLayer As Integer, ByVal InputWeightSize As Integer, ByVal Activation As Neuron.ActivationFunction)
        mInputLayer = New NeuronLayer(Inputs, InputWeightSize + 1, Activation)
        mLayers = New ArrayList()
        Dim idx As Integer
        Dim LastLayerSize As Integer = Inputs
        For idx = 0 To HiddenLayers - 1
            mLayers.Add(New NeuronLayer(NeuronsPerHiddenLayer, LastLayerSize + 1, Activation))
            LastLayerSize = NeuronsPerHiddenLayer
        Next
        mOutputLayer = New NeuronLayer(Outputs, LastLayerSize + 1, Activation)
    End Sub

    'Returns the total number of weights in the neural network by adding up the number that comes back from each layer.
    Public ReadOnly Property WeightCount() As Integer
        Get
            Dim cnt As Integer = mInputLayer.NumWeights
            Dim nl As NeuronLayer
            For Each nl In mLayers
                cnt += nl.NumWeights
            Next
            cnt += mOutputLayer.NumWeights
            Return cnt
        End Get
    End Property

    'Gets the output of the neural network based on the passed in inputs.
    Public ReadOnly Property Outputs(ByVal Inputs() As Single) As Single()
        Get
            Dim LastOutputs() As Single
            Dim nl As NeuronLayer
            'Get the output of the input layer and then pass that on to the next hidden layer.  The output from
            'the previous layer is then used as the input to the next layer until we reach the output layer.
            LastOutputs = mInputLayer.Outputs(Inputs)
            For Each nl In mLayers
                LastOutputs = nl.Outputs(LastOutputs)
            Next
            LastOutputs = mOutputLayer.Outputs(LastOutputs)
            Return LastOutputs
        End Get
    End Property

    'Accessor method to get and assign weights to the whole neural network.  The weights also include the threshold values
    'so that it is easier to evolve the network with a GA.
    Public Property Weights() As Single()
        Get
            Dim retWeights() As Single
            ReDim retWeights(Me.WeightCount - 1)
            Dim idx As Integer
            Dim retidx As Integer
            Dim layerWeights() As Single
            Dim nl As NeuronLayer
            'Debug.Write("Input layer weights ")
            layerWeights = mInputLayer.Weights
            'Debug.Write(vbCrLf)
            For idx = 0 To layerWeights.Length - 1
                retWeights(retidx) = layerWeights(idx)
                retidx += 1
            Next
            For Each nl In mLayers
                'Debug.Write("Inner layer weights ")
                layerWeights = nl.Weights
                'Debug.Write(vbCrLf)
                For idx = 0 To layerWeights.Length - 1
                    retWeights(retidx) = layerWeights(idx)
                    retidx += 1
                Next
            Next
            'Debug.Write("Output layer weights ")
            layerWeights = mOutputLayer.Weights
            'Debug.Write(vbCrLf)
            For idx = 0 To layerWeights.Length - 1
                retWeights(retidx) = layerWeights(idx)
                retidx += 1
            Next
            Return retWeights
        End Get
        Set(ByVal Value As Single())
            Dim vIdx As Integer
            Dim nl As NeuronLayer
            Dim layerIdx As Integer
            Dim layerWeights() As Single

            For vIdx = 0 To Value.Length - 1
                ReDim layerWeights(mInputLayer.NumWeights - 1)
                For layerIdx = 0 To layerWeights.Length - 1
                    layerWeights(layerIdx) = Value(vIdx)
                    vIdx += 1
                Next
                mInputLayer.Weights = layerWeights
                For Each nl In mLayers
                    ReDim layerWeights(nl.NumWeights - 1)
                    For layerIdx = 0 To layerWeights.Length - 1
                        layerWeights(layerIdx) = Value(vIdx)
                        vIdx += 1
                    Next
                    nl.Weights = layerWeights
                Next
                ReDim layerWeights(mOutputLayer.NumWeights - 1)
                For layerIdx = 0 To layerWeights.Length - 1
                    layerWeights(layerIdx) = Value(vIdx)
                    vIdx += 1
                Next
                mOutputLayer.Weights = layerWeights
            Next
        End Set
    End Property
End Class
