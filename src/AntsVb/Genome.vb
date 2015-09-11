'Simple genetic algorithm class for the ants.
Public Class Genome
    Private mAnts() As Ant
    Private mPopSize As Integer
    Private mChromoLength As Integer

    Private mTotalFitness As Single
    Private mBestFitness As Single
    Private mAvgFitness As Single
    Private mWorstFitness As Single
    Private mFittestGenome As Integer

    Private mMutationRate As Single
    Private mCrossOverRate As Single

    Private mGeneration As Integer

    'Setup the GA.  The Ant class is our actual chromosome while the weights in the ants neural network form the genes.
    Public Sub New(ByRef Ants() As Ant, ByVal ChromoLength As Integer, ByVal MutationRate As Single, ByVal CrossOverRate As Single)
        mAnts = Ants
        mPopSize = mAnts.Length - 1
        Reset()
        mGeneration = 0
        mMutationRate = MutationRate
        mCrossOverRate = CrossOverRate
        mChromoLength = ChromoLength
    End Sub

    'Simple single point cross over function.
    Public Sub CrossOVer(ByRef P1() As Single, ByRef P2() As Single, ByRef C1() As Single, ByRef C2() As Single)
        Dim idx As Integer
        If Rand.NextDouble > mCrossOverRate Then
            'If we shouldn't cross over then we'll just return the two children
            For idx = 0 To P1.Length - 1
                C1(idx) = P1(idx)
                C2(idx) = P2(idx)
            Next
            Return
        End If
        Dim XPoint As Integer = Rand.Next(0, P1.Length - 1)
        For idx = 0 To XPoint - 1
            C1(idx) = P1(idx)
            C2(idx) = P2(idx)
        Next
        For idx = XPoint To P1.Length - 1
            C1(idx) = P2(idx)
            C2(idx) = P1(idx)
        Next
    End Sub

    'Loops over each gene in the chromosome and decides whether to mutate it or not.  No max mutation rate
    'has been set for this function.
    Public Sub Mutate(ByRef C() As Single)
        Dim idx As Integer
        For idx = 0 To C.Length - 1
            If Rand.NextDouble < mMutationRate Then
                C(idx) += Rand.NextDouble
            End If
        Next
    End Sub

    'Use roulette selection to return an ant.
    Public Function RouletteSelection() As Ant
        Dim Slice As Single = Rand.NextDouble * mTotalFitness
        Dim ChosenOne As Ant
        Dim FitSoFar As Single
        Dim idx As Integer

        For idx = 0 To mAnts.Length - 1
            FitSoFar += mAnts(idx).Fitness
            If FitSoFar > Slice Then
                ChosenOne = mAnts(idx)
                Exit For
            End If
        Next
        If ChosenOne Is Nothing Then
            ChosenOne = mAnts(idx - 1)
        End If
        Return ChosenOne
    End Function

    'The main work horse of a Genetic Algorithm
    Public Sub Epoch()
        Reset()
        'Sort it for elitism and scaling functions
        mAnts.Sort(mAnts)
        CalcStats()
        'Create an array to store the new population.
        Dim NewPop()() As Single
        ReDim NewPop(mAnts.Length - 1)
        Dim idx As Integer = mAnts.Length * 0.1
        'A bit of elitism here.  We'll grab the top 10% and put it into the new population.
        GrabNBest(idx, NewPop)
        'Now go and make the rest of the population
        idx += 1
        While idx < mAnts.Length
            Dim P1 As Single() = RouletteSelection().Weights
            Dim P2 As Single() = RouletteSelection().Weights
            Dim C1 As Single()
            Dim C2 As Single()
            ReDim C1(P1.Length - 1)
            ReDim C2(P2.Length - 1)
            CrossOVer(P1, P2, C1, C2)
            Mutate(C1)
            Mutate(C2)
            NewPop(idx) = C1
            If idx + 1 < mAnts.Length Then
                NewPop(idx + 1) = C2
            End If
            idx += 2
        End While
        'Put the new weights back into the population.
        For idx = 0 To mAnts.Length - 1
            mAnts(idx).Weights = NewPop(idx)
        Next
        mGeneration += 1
    End Sub

    'Elitism function.  It grabs the first "num" entries in the ants array.  Its assumed that the
    'array has already been sorted.
    Public Sub GrabNBest(ByVal num As Integer, ByVal NewPop()() As Single)
        Dim idx As Integer
        For idx = 0 To num
            NewPop(idx) = mAnts(idx).Weights
        Next
    End Sub

    'Just some statistics on the population.
    Public Sub CalcStats()
        Dim idx As Integer
        Dim highest As Single = 0
        Dim lowest As Single = 999999

        For idx = 0 To mAnts.Length - 1
            mTotalFitness += mAnts(idx).Fitness
            If mAnts(idx).Fitness > highest Then
                highest = mAnts(idx).Fitness
                mFittestGenome = idx
            End If
            If mAnts(idx).Fitness < lowest Then
                lowest = mAnts(idx).Fitness
            End If
        Next
        mWorstFitness = lowest
        mBestFitness = highest
        mAvgFitness = mTotalFitness / mAnts.Length - 1
    End Sub

    'Reset the algorithms statistics.
    Private Sub Reset()
        mTotalFitness = 0
        mBestFitness = 0
        mAvgFitness = 0
        mWorstFitness = 99999
        mFittestGenome = 0
    End Sub

    'The following are just accessor properties to the statistics.
    Public ReadOnly Property TotalFitness() As Single
        Get
            Return mTotalFitness
        End Get
    End Property
    Public ReadOnly Property BestFitness() As Single
        Get
            Return mBestFitness
        End Get
    End Property
    Public ReadOnly Property AvgFitness() As Single
        Get
            Return mAvgFitness
        End Get
    End Property
    Public ReadOnly Property WorstFitness() As Single
        Get
            Return mWorstFitness
        End Get
    End Property
    Public ReadOnly Property Generations() As Integer
        Get
            Return mGeneration
        End Get
    End Property
End Class
