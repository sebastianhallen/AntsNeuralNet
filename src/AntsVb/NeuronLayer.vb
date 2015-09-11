'Neuron Layer organizes the neurons into distinct layers.  It aggregates the weights and output properties of the neurons.
Public Class NeuronLayer
    Inherits System.Collections.CollectionBase
    'The weights per neuron is cached to make some calculations faster like the total number of weights in all the neurons of this layer.
    Private mWeightsPerNeuron As Integer

    'Constructs a layer of neurons and initializes them with the number of weights and the activation function to be uses.
    Public Sub New(ByVal Neurons As Integer, ByVal WeightsPerNeuron As Integer, ByVal ActivationFunction As Neuron.ActivationFunction)
        Dim idx As Integer
        Dim n As Neuron
        For idx = 0 To Neurons - 1
            n = New Neuron(WeightsPerNeuron, ActivationFunction)
            list.Add(n)
        Next
        mWeightsPerNeuron = WeightsPerNeuron
    End Sub

    Public ReadOnly Property NumWeights() As Integer
        Get
            Return mWeightsPerNeuron * list.Count
        End Get
    End Property

    Public ReadOnly Property WeightsPerNeuron() As Integer
        Get
            Return mWeightsPerNeuron
        End Get
    End Property

    'All the weights for this layer of neurons
    Public Property Weights() As Single()
        Get
            'We'll make an array big enough to hold all of the weights in the layer and then loop through each 
            'neurons weights copying them into our ret value.
            Dim ret() As Single
            ReDim ret((mWeightsPerNeuron * list.Count) - 1)
            Dim idx As Integer
            Dim n As Neuron
            For Each n In list
                Dim tmpw As Single()
                tmpw = n.Weights
                Dim idx2 As Integer
                'Debug.Write("neuron" & list.IndexOf(n) & ": ")
                For idx2 = 0 To tmpw.Length - 1
                    ret(idx) = tmpw(idx2)
                    'Debug.Write(tmpw(idx2).ToString & ",")
                    idx += 1
                Next
                'Debug.Write("|")
            Next
            Return ret
        End Get
        Set(ByVal Value As Single())
            'Just the opposite of the get.  This distributes an array of weights to the neurons.
            Dim idx As Integer
            Dim n As Neuron
            'Loops through each neuron
            For Each n In list
                Dim tmpw() As Single
                ReDim tmpw(mWeightsPerNeuron - 1)
                Dim idx2 As Integer
                For idx2 = 0 To tmpw.Length - 1
                    tmpw(idx2) = Value(idx)
                    idx += 1
                Next
                n.Weights = tmpw
            Next
        End Set
    End Property

    'Aggregates the outputs of each neuron into one big return array.  The inputs
    'array is sent to each neuron and the output is stored in an element of the array.
    Public ReadOnly Property Outputs(ByVal Inputs() As Single) As Single()
        Get
            Dim outidx As Integer
            Dim n As Neuron
            Dim tmpout() As Single
            ReDim tmpout(list.Count - 1)
            For Each n In list
                Dim tmpins() As Single
                tmpout(outidx) = n.Output(Inputs)
                outidx += 1
            Next
            Return tmpout
        End Get
    End Property
End Class
