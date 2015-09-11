Public Class Ant
    'The Ant class is the container for the neural network and main object in the ant farm.
    Implements IComparable 'Used for sorting the array of ants.

    Private mBrain As NeuralNetwork
    'Chromosome stuff
    Private mFitness As Single
    'Outputs from the NN
    Private mRTrack As Single
    Private mLTrack As Single

    Private mPosition As PointF
    'Inputs for the NN
    'Index of the closest food
    Private mLastFoundFood As Integer
    Private mLookAt As PointF

    Private mRotation As Single
    Private mSpeed As Single

    Private mMaxTurnRate As Single = 0.9

    'Scale to draw the Ant at.
    Public mScale As Single


    Public Sub New(ByVal nn As NeuralNetwork, ByVal x As Single, ByVal y As Single)
        mBrain = nn
        mPosition = New PointF(x, y)
        mRTrack = Rand.NextDouble
        mLTrack = Rand.NextDouble
        'Debug.WriteLine(mLTrack.ToString & " " & mRTrack.ToString)
    End Sub

    'Called for each tick in the program.  The update function gets inputs for the NN and then sets the track values according to the output.
    Public Function Update(ByRef food() As PointF)
        Dim inputs() As Single
        ReDim inputs(3)
        Dim closestfood As PointF = GetClosestObject(food)
        VectorNormalize(closestfood)
        inputs(0) = closestfood.X
        inputs(1) = closestfood.Y
        inputs(2) = mLookAt.X
        inputs(3) = mLookAt.Y

        Dim output() As Single = mBrain.Outputs(inputs)
        mLTrack = output(0)
        mRTrack = output(1)


        Dim RotForce As Single
        RotForce = mLTrack - mRTrack
        Clamp(RotForce, -1 * mMaxTurnRate, mMaxTurnRate)
        mRotation += RotForce
        mSpeed = mLTrack + mRTrack

        mLookAt.X = Math.Sin(mRotation) * -1
        mLookAt.Y = Math.Cos(mRotation)

        mPosition = AddVectors(mPosition, MultVect(mLookAt, mSpeed))
    End Function

    'Returns a vector to the closest food/object
    Public Function GetClosestObject(ByRef food() As PointF) As PointF
        Dim ClosestSoFar As Integer = 9999
        Dim ClosestObject As PointF = New PointF(0, 0)
        Dim idx As Integer
        For idx = 0 To food.Length - 1
            Dim lenToObject As Single = VectorLength(VectDistance(food(idx), mPosition))
            If lenToObject < ClosestSoFar Then
                ClosestSoFar = lenToObject
                ClosestObject = VectDistance(mPosition, food(idx))
                mLastFoundFood = idx
            End If
        Next
        Return ClosestObject
    End Function

    'Checks to see if the Ant has found food and returns the index of the food or -1 if not found.
    Public Function CheckForFood(ByRef food() As PointF, ByVal size As Single) As Integer
        Dim distance As PointF = VectDistance(mPosition, food(mLastFoundFood))
        If VectorLength(distance) < size + 5 Then
            Return mLastFoundFood
        Else
            Return -1
        End If
    End Function

    'Called after each epoch.  This just resets the fitness score and randomly places the ant on the board.
    Public Sub Reset(ByVal width As Integer, ByVal height As Integer)
        'Debug.WriteLine("Reseting ant")
        Me.Fitness = 0
        mPosition = New PointF(Rand.NextDouble * width, Rand.NextDouble * height)
        'Debug.WriteLine("Ant reseting to position " & mPosition.ToString)
        mRotation = Rand.NextDouble * Math.PI * 2
    End Sub

    'Called when the ant finds a food
    Public Sub IncFitness()
        Me.Fitness += 1
    End Sub

    'The next set of properties are simple accessors for the module leve variables of the ant.
    Public Property Position() As PointF
        Get
            Return mPosition
        End Get
        Set(ByVal Value As PointF)
            mPosition = Value
        End Set
    End Property

    Public Property Fitness() As Single
        Get
            Return mFitness
        End Get
        Set(ByVal Value As Single)
            mFitness = Value
        End Set
    End Property

    Public ReadOnly Property NumWeights() As Integer
        Get
            Return mBrain.WeightCount
        End Get
    End Property

    Public Property Weights() As Single()
        Get
            Return mBrain.Weights
        End Get
        Set(ByVal Value As Single())
            mBrain.Weights = Value
        End Set
    End Property

    Public Property Brain() As NeuralNetwork
        Get
            Return mBrain
        End Get
        Set(ByVal Value As NeuralNetwork)
            mBrain = Value
        End Set
    End Property

    Public Property LastFoundFood() As Integer
        Get
            Return mLastFoundFood
        End Get
        Set(ByVal Value As Integer)
            mLastFoundFood = Value
        End Set
    End Property

    'The next set of subs and functions are utility functions for the vectoring of the ant.

    'Uses pythagorean theory to get the length of a vector
    Private Function VectorLength(ByRef p As PointF) As Single
        Return Math.Sqrt(p.X * p.X + p.Y * p.Y)
    End Function

    'Normalizes the vector to values between -1 and 1
    Private Sub VectorNormalize(ByRef p As PointF)
        Dim length As Single = Me.VectorLength(p)
        p.X = p.X / length
        p.Y = p.Y / length
    End Sub

    'Returns the distance between two points.
    Private Function VectDistance(ByRef p As PointF, ByVal p2 As PointF) As PointF
        Dim ret As PointF
        ret = New PointF(p.X, p.Y)
        ret.X -= p2.X
        ret.Y -= p2.Y
        Return ret
    End Function

    Private Function MultVect(ByRef p As PointF, ByVal value As Single) As PointF
        Dim ret As PointF = New PointF(p.X, p.Y)
        ret.X *= value
        ret.Y *= value
        Return ret
    End Function

    Private Function AddVectors(ByRef p As PointF, ByRef p2 As PointF) As PointF
        p.X += p2.X
        p.Y += p2.Y
        Return p
    End Function

    'Just ensures that a value is between the min and max.
    Private Sub Clamp(ByRef arg As Single, ByVal min As Single, ByVal max As Single)
        If arg < min Then
            arg = min
        End If
        If arg > max Then
            arg = max
        End If
    End Sub

    'Used for sorting the array of ants.  The sort value is based on the other ants fitness compared to this one.
    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim a As Ant
        a = obj
        If a.Fitness < Me.Fitness Then Return -1
        If a.Fitness = Me.Fitness Then Return 0
        If a.Fitness > Me.Fitness Then Return 1
    End Function
End Class
