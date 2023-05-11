Public Class Camino
    Dim RectCam As Rectangle
    Dim Mov As Enemigo.TipMov
    Public Sub New(ByVal RectCamino As Rectangle, ByVal Movim As Enemigo.TipMov)
        RectCam = RectCamino
        Mov = Movim
    End Sub

    Public ReadOnly Property TipMove() As Enemigo.TipMov
        Get
            Return Mov
        End Get
    End Property

    Public ReadOnly Property Rect() As Rectangle
        Get
            Return RectCam
        End Get
    End Property
End Class
