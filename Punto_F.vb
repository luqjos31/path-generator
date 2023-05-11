Public Class Punto_F
    Dim Fa, Puntx, Punty, G As Integer
    Dim RectP As Rectangle
    Dim TipMov As Enemigo.TipMov
    Dim L, Posicion As Integer
    Public Sub New(ByVal F As Integer, ByVal Px As Integer, ByVal Py As Integer, ByVal Gs As Integer, ByVal TipMove As Enemigo.TipMov, ByVal Ld As Integer)
        Fa = F
        G = Gs
        L = Ld
        TipMov = TipMove
        Puntx = Px
        Punty = Py
        RectP = New Rectangle(Puntx * TamCuad, Punty * TamCuad, TamCuad, TamCuad)
    End Sub
    Public Sub New(ByVal F As Integer, ByVal Rect As Rectangle, ByVal Gs As Integer, ByVal Ld As Integer, ByVal TipMove As Enemigo.TipMov, ByVal Px As Integer, ByVal Py As Integer, ByVal Posic As Integer)
        G = Gs
        Fa = F
        L = Ld
        TipMov = TipMove
        Puntx = Px
        Punty = Py
        RectP = Rect
        Posicion = Posic
    End Sub
    Public Sub New(ByVal Rect As Rectangle)
        RectP = Rect
    End Sub
    Public ReadOnly Property PXant() As Integer
        Get
            Return Puntx
        End Get
    End Property

    Public ReadOnly Property PYant() As Integer
        Get
            Return Punty
        End Get
    End Property
    Public ReadOnly Property Fs() As Integer
        Get
            Return Fa
        End Get
    End Property

    Public ReadOnly Property RectCerr() As Rectangle
        Get
            Return RectP
        End Get
    End Property

    Public ReadOnly Property Gcuad() As Integer
        Get
            Return G
        End Get
    End Property

    Public ReadOnly Property TipoMovi() As Enemigo.TipMov
        Get
            Return TipMov
        End Get
    End Property

    Public ReadOnly Property DistManh() As Integer
        Get
            Return L
        End Get
    End Property


End Class

