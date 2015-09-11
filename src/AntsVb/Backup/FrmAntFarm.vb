'Controls the antfarm class.  Sorry but there isn't a lot of fancy graphics here.
Public Class FrmFarm
    Inherits System.Windows.Forms.Form
    Private mAntFarm As AntFarm
    Private mFarmThread As System.Threading.Thread

    Private mFoodPen As System.Drawing.Pen
    Private mFoodRec As System.Drawing.Rectangle
    Private mAntPen As System.Drawing.Pen
    Private mUberAntPen As System.Drawing.Pen
    Private mAntRec As System.Drawing.Rectangle

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents CmdStart As System.Windows.Forms.Button
    Friend WithEvents CmdReset As System.Windows.Forms.Button
    Friend WithEvents ChkLock As System.Windows.Forms.CheckBox
    Friend WithEvents ChkRender As System.Windows.Forms.CheckBox
    Friend WithEvents TxtAnts As System.Windows.Forms.TextBox
    Friend WithEvents TxtFood As System.Windows.Forms.TextBox
    Friend WithEvents LblAnts As System.Windows.Forms.Label
    Friend WithEvents LblFood As System.Windows.Forms.Label
    Friend WithEvents PnlField As System.Windows.Forms.Panel
    Friend WithEvents TimerPaint As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblAvg As System.Windows.Forms.Label
    Friend WithEvents LblTot As System.Windows.Forms.Label
    Friend WithEvents LblBest As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LblNumGen As System.Windows.Forms.Label
    Friend WithEvents PnlControl As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LblCurBestScore As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PnlControl = New System.Windows.Forms.Panel()
        Me.LblFood = New System.Windows.Forms.Label()
        Me.LblAnts = New System.Windows.Forms.Label()
        Me.TxtFood = New System.Windows.Forms.TextBox()
        Me.TxtAnts = New System.Windows.Forms.TextBox()
        Me.ChkRender = New System.Windows.Forms.CheckBox()
        Me.ChkLock = New System.Windows.Forms.CheckBox()
        Me.CmdReset = New System.Windows.Forms.Button()
        Me.CmdStart = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblBest = New System.Windows.Forms.Label()
        Me.LblTot = New System.Windows.Forms.Label()
        Me.LblAvg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PnlField = New System.Windows.Forms.Panel()
        Me.TimerPaint = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblNumGen = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblCurBestScore = New System.Windows.Forms.Label()
        Me.PnlControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlControl
        '
        Me.PnlControl.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label3, Me.LblFood, Me.LblAnts, Me.TxtFood, Me.TxtAnts, Me.ChkRender, Me.ChkLock, Me.CmdReset, Me.CmdStart, Me.Label7, Me.Label6, Me.LblBest, Me.LblTot, Me.LblAvg, Me.Label2})
        Me.PnlControl.Location = New System.Drawing.Point(346, 24)
        Me.PnlControl.Name = "PnlControl"
        Me.PnlControl.Size = New System.Drawing.Size(136, 387)
        Me.PnlControl.TabIndex = 0
        '
        'LblFood
        '
        Me.LblFood.Location = New System.Drawing.Point(21, 308)
        Me.LblFood.Name = "LblFood"
        Me.LblFood.Size = New System.Drawing.Size(32, 23)
        Me.LblFood.TabIndex = 8
        Me.LblFood.Text = "Food"
        '
        'LblAnts
        '
        Me.LblAnts.Location = New System.Drawing.Point(21, 280)
        Me.LblAnts.Name = "LblAnts"
        Me.LblAnts.Size = New System.Drawing.Size(32, 23)
        Me.LblAnts.TabIndex = 7
        Me.LblAnts.Text = "Ants"
        '
        'TxtFood
        '
        Me.TxtFood.Location = New System.Drawing.Point(64, 308)
        Me.TxtFood.Name = "TxtFood"
        Me.TxtFood.Size = New System.Drawing.Size(44, 20)
        Me.TxtFood.TabIndex = 6
        Me.TxtFood.Text = "25"
        '
        'TxtAnts
        '
        Me.TxtAnts.Location = New System.Drawing.Point(64, 280)
        Me.TxtAnts.Name = "TxtAnts"
        Me.TxtAnts.Size = New System.Drawing.Size(44, 20)
        Me.TxtAnts.TabIndex = 5
        Me.TxtAnts.Text = "20"
        '
        'ChkRender
        '
        Me.ChkRender.Checked = True
        Me.ChkRender.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkRender.Location = New System.Drawing.Point(28, 39)
        Me.ChkRender.Name = "ChkRender"
        Me.ChkRender.Size = New System.Drawing.Size(79, 24)
        Me.ChkRender.TabIndex = 4
        Me.ChkRender.Text = "Render"
        '
        'ChkLock
        '
        Me.ChkLock.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.ChkLock.Checked = True
        Me.ChkLock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkLock.Location = New System.Drawing.Point(28, 8)
        Me.ChkLock.Name = "ChkLock"
        Me.ChkLock.Size = New System.Drawing.Size(79, 24)
        Me.ChkLock.TabIndex = 3
        Me.ChkLock.Text = "Lock"
        '
        'CmdReset
        '
        Me.CmdReset.Location = New System.Drawing.Point(21, 336)
        Me.CmdReset.Name = "CmdReset"
        Me.CmdReset.Size = New System.Drawing.Size(88, 28)
        Me.CmdReset.TabIndex = 1
        Me.CmdReset.Text = "Reset"
        '
        'CmdStart
        '
        Me.CmdStart.Location = New System.Drawing.Point(23, 71)
        Me.CmdStart.Name = "CmdStart"
        Me.CmdStart.Size = New System.Drawing.Size(88, 28)
        Me.CmdStart.TabIndex = 0
        Me.CmdStart.Text = "&Start"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(17, 159)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 16)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Avg Fitness"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(17, 183)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Best Fitness"
        '
        'LblBest
        '
        Me.LblBest.Location = New System.Drawing.Point(92, 183)
        Me.LblBest.Name = "LblBest"
        Me.LblBest.Size = New System.Drawing.Size(36, 16)
        Me.LblBest.TabIndex = 14
        '
        'LblTot
        '
        Me.LblTot.Location = New System.Drawing.Point(92, 135)
        Me.LblTot.Name = "LblTot"
        Me.LblTot.Size = New System.Drawing.Size(36, 16)
        Me.LblTot.TabIndex = 13
        '
        'LblAvg
        '
        Me.LblAvg.Location = New System.Drawing.Point(92, 159)
        Me.LblAvg.Name = "LblAvg"
        Me.LblAvg.Size = New System.Drawing.Size(36, 16)
        Me.LblAvg.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(17, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Total Fitness"
        '
        'PnlField
        '
        Me.PnlField.BackColor = System.Drawing.Color.Black
        Me.PnlField.Location = New System.Drawing.Point(4, 24)
        Me.PnlField.Name = "PnlField"
        Me.PnlField.Size = New System.Drawing.Size(332, 388)
        Me.PnlField.TabIndex = 1
        '
        'TimerPaint
        '
        Me.TimerPaint.Interval = 33
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Generations:"
        '
        'LblNumGen
        '
        Me.LblNumGen.Location = New System.Drawing.Point(84, 4)
        Me.LblNumGen.Name = "LblNumGen"
        Me.LblNumGen.Size = New System.Drawing.Size(36, 16)
        Me.LblNumGen.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Last Generation Stats"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(132, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Best Score:"
        '
        'LblCurBestScore
        '
        Me.LblCurBestScore.Location = New System.Drawing.Point(200, 4)
        Me.LblCurBestScore.Name = "LblCurBestScore"
        Me.LblCurBestScore.Size = New System.Drawing.Size(44, 16)
        Me.LblCurBestScore.TabIndex = 12
        '
        'FrmFarm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(484, 417)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.LblCurBestScore, Me.Label4, Me.LblNumGen, Me.PnlField, Me.PnlControl, Me.Label1})
        Me.Name = "FrmFarm"
        Me.Text = "Ant Farm"
        Me.PnlControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub FrmFarm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mFoodPen = New System.Drawing.Pen(Color.Yellow)
        mFoodRec = New System.Drawing.Rectangle(1, 1, 5, 5)
        mAntPen = New System.Drawing.Pen(Color.Red)
        mUberAntPen = New System.Drawing.Pen(Color.Azure)
        mAntRec = New System.Drawing.Rectangle(1, 1, 5, 5)
        TimerPaint.Interval = SleepInterval
    End Sub

    Private Sub PnlField_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PnlField.Paint
        Dim idx As Integer
        If ChkRender.Checked = False Then Return
        If mAntFarm Is Nothing Then Return 'don't want to draw nothing.
        Dim g As System.Drawing.Graphics = PnlField.CreateGraphics()
        Dim p As PointF
        For idx = 0 To mAntFarm.Ants.Length - 1
            p = mAntFarm.Ants(idx).Position
            mAntRec.X = p.X
            mAntRec.Y = p.Y
            If mAntFarm.Ants(idx).Fitness = mAntFarm.BestScore And mAntFarm.BestScore <> 0 Then
                g.DrawEllipse(mUberAntPen, mAntRec)
            Else
                g.DrawEllipse(mAntPen, mAntRec)
            End If

        Next
        For idx = 0 To mAntFarm.Food.Length - 1

            p = mAntFarm.Food(idx)
            mFoodRec.X = p.X
            mFoodRec.Y = p.Y
            g.DrawRectangle(mFoodPen, mFoodRec)
        Next
        g.Dispose()
    End Sub

    Private Sub CmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdStart.Click
        StartRun()
        CmdStart.Enabled = False
    End Sub
    Private Sub StartRun()
        Dim numAnts As Integer
        Dim numFood As Integer
        numAnts = Integer.Parse(TxtAnts.Text)
        numFood = Integer.Parse(TxtFood.Text)
        mAntFarm = New AntFarm(numAnts, numFood, PnlField.Width, PnlField.Height)
        Dim x() As Single = mAntFarm.Ants(1).Brain.Weights
        Dim y() As Single = mAntFarm.Ants(2).Brain.Weights

        If mFarmThread Is Nothing = False Then
            mFarmThread.Abort()
            mFarmThread = Nothing
        End If
        mFarmThread = New System.Threading.Thread(AddressOf mAntFarm.Start)
        mFarmThread.Start()
        TimerPaint.Enabled = True
    End Sub
    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        'Debug.WriteLine("closed")
        If mAntFarm Is Nothing = False Then
            mAntFarm.Suspend()
        End If
    End Sub

    Private Sub TimerPaint_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerPaint.Tick
        PnlField.Invalidate()
        LblNumGen.Text = mAntFarm.Genome.Generations.ToString
        LblTot.Text = mAntFarm.Genome.TotalFitness.ToString
        LblAvg.Text = mAntFarm.Genome.AvgFitness.ToString
        LblBest.Text = mAntFarm.Genome.BestFitness.ToString
        LblCurBestScore.Text = mAntFarm.BestScore.ToString
    End Sub

    Private Sub ChkLock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLock.CheckedChanged
        If mAntFarm Is Nothing = False Then
            mAntFarm.FastRender = Not ChkLock.Checked
            If mAntFarm.FastRender Then
                TimerPaint.Interval = 1
            Else
                TimerPaint.Interval = 33
            End If
        End If
    End Sub

    Private Sub CmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdReset.Click
        StartRun()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        PnlControl.Left = Me.Width - PnlControl.Width - 10
        PnlField.Width = Me.Width - 10 - PnlControl.Width - 20
        PnlField.Height = Me.Height - PnlField.Top - 30
        If mAntFarm Is Nothing Then Exit Sub
        mAntFarm.Width = PnlField.Width
        mAntFarm.Height = PnlField.Height
    End Sub
End Class