Public Class Neuron
    'Enumerator for the Activation Function.  Currently only Step and Sigmoid are implemented
    Public Enum ActivationFunction
        AFStep = 0
        AFSigmoid = 1
    End Enum

    'Some variables to hold our property values and such.
    Private mOutput As Single
    Private mWeights() As Single

    Private mActivation As ActivationFunction

    'Constructor for the neuron.  It will setup an array for the weights and tell us what activation function to use.
    Public Sub New(ByVal NumWeights As Integer, ByVal activation As ActivationFunction)
        ReDim mWeights(NumWeights - 1) ' -1 b/c VB uses this number as the upper bound and not the number of elements. :-|
        Dim idx As Integer
        For idx = 0 To mWeights.Length - 1
            mWeights(idx) = Rand.NextDouble()
            'Debug.Write(mWeights(idx).ToString & " ")
        Next
        'Debug.WriteLine("")

        Me.Activation = activation
    End Sub

    'Change the activation function
    Public Property Activation() As ActivationFunction
        Get
            Return mActivation
        End Get
        Set(ByVal Value As ActivationFunction)
            mActivation = Value ' Store the value in a local variable.
        End Set
    End Property

    'The input weights and threshold value
    Public Property Weights() As Single()
        Get
            Return mWeights
        End Get
        Set(ByVal Value As Single())
            mWeights = Value
        End Set
    End Property

    'Just about the most important function in a neuron.  It takes an array of singles and calculates the output.
    'Usage is neuron.Output(inputs())
    Public ReadOnly Property Output(ByVal inputs As Single()) As Single
        Get
            Dim total As Single
            Dim idx As Integer
            Dim threshold As Single

            'inputs should be 1 less than the number of weights since the last weight is the threshold for activation.
            For idx = 0 To inputs.Length - 1
                total += inputs(idx) * mWeights(idx)
            Next
            'idx += 1
            threshold = mWeights(idx)
            If mActivation = ActivationFunction.AFStep Then
                If threshold < total Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 1 / (1 + Math.Exp((-total) / threshold))
            End If
        End Get
    End Property
End Class
