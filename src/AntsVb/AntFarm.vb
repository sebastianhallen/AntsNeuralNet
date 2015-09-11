'Main storage class for the ant farm.  It runs in its own thread continually calling the update method. 
'After mMaxticks the epoch is called in the genome class to start a new generation.
Public Class AntFarm

    Private mAnts() As Ant
    Private mFood() As PointF
    Private mNumAnts As Integer
    Private mNumFood As Integer

    Private mFastRender As Boolean
    Private mNumTicks As Integer
    Private mMaxTicks As Integer = 1000
    Private mWidth As Integer
    Private mHeight As Integer

    Private rand As Random = New Random()

    Private mShouldRun As Boolean

    Private mGenome As Genome
    Private mBestAnt As Integer
    Private mBestScore As Integer

    Public Sub New(ByVal AntCnt As Integer, ByVal FoodCnt As Integer, ByVal Width As Integer, ByVal Height As Integer)
        Dim idx As Integer

        mNumAnts = AntCnt
        mNumFood = FoodCnt
        mWidth = Width
        mHeight = Height

        ReDim mAnts(AntCnt - 1)
        ReDim mFood(FoodCnt - 1)
        'Add ants to the world
        For idx = 0 To mAnts.Length - 1
            mAnts(idx) = New Ant(New NeuralNetwork(4, 2, 1, 4, 4, Neuron.ActivationFunction.AFSigmoid), rand.NextDouble * Width, rand.NextDouble * Height)
        Next

        'Add food to the world
        For idx = 0 To mFood.Length - 1
            mFood(idx) = New PointF(rand.NextDouble * Width, rand.NextDouble * Height)
        Next

        mGenome = New Genome(mAnts, mAnts(1).NumWeights, 0.01, 0.05)

    End Sub

    Public Sub Start()
        mShouldRun = True
        While (mShouldRun)
            Update()
            If mFastRender = False Then
                System.Threading.Thread.CurrentThread.Sleep(SleepInterval)
            End If
        End While
        'Debug.WriteLine("Stopped AntFarm")
    End Sub

    Public Sub Suspend()
        'Debug.WriteLine("Stopping AntFarm")
        mShouldRun = False
    End Sub

    Public Sub Update()
        Dim idx As Integer
        If mNumTicks < mMaxTicks Then
            mBestAnt = -1
            mBestScore = 0
            For idx = 0 To mAnts.Length - 1
                mAnts(idx).Update(mFood)
                Dim hit As Integer = mAnts(idx).CheckForFood(mFood, 5)
                If hit >= 0 Then
                    'Debug.WriteLine("Ant" & idx.ToString & " got food " & hit.ToString)
                    mAnts(idx).IncFitness()
                    mFood(hit) = New PointF(rand.NextDouble * mWidth, rand.NextDouble * mHeight)
                End If
                Dim p As PointF
                p = mAnts(idx).Position
                'Wrap around code to make sure the ant stays on the board.
                If p.X > mWidth Then p.X = 0
                If p.X < 0 Then p.X = mWidth
                If p.Y > mHeight Then p.Y = 1
                If p.Y < 0 Then p.Y = mHeight
                mAnts(idx).Position = p
                If mAnts(idx).Fitness > mBestScore Then
                    mBestAnt = idx
                    mBestScore = mAnts(idx).Fitness
                End If
            Next
            mNumTicks += 1
        Else 'The current generation has lived out its max life.  Its time to do an epoch and start a new generation.
            'Debug.WriteLine("Doing Epoch")
            mGenome.Epoch()
            mNumTicks = 0
            For idx = 0 To mAnts.Length - 1
                mAnts(idx).Reset(mWidth, mHeight)
            Next

        End If
    End Sub
    'The rest are simple accessor properties.
    Public ReadOnly Property Ants() As Ant()
        Get
            Return mAnts
        End Get
    End Property
    Public ReadOnly Property Food() As PointF()
        Get
            Return mFood
        End Get
    End Property
    Property FastRender() As Boolean
        Get
            Return mFastRender
        End Get
        Set(ByVal Value As Boolean)
            mFastRender = Value
        End Set
    End Property
    Public ReadOnly Property Genome() As Genome
        Get
            Return mGenome
        End Get
    End Property
    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal Value As Integer)
            mWidth = Value
        End Set
    End Property
    Public Property Height() As Integer
        Get
            Return mHeight
        End Get
        Set(ByVal Value As Integer)
            mHeight = Value
        End Set
    End Property
    Public ReadOnly Property BestAnt() As Integer
        Get
            Return mBestAnt
        End Get
    End Property
    Public ReadOnly Property BestScore() As Integer
        Get
            Return mBestScore
        End Get
    End Property
End Class
