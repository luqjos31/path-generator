Module Modulo

    Public Pause, Espada, LHierro, LPlata, LOro, InicE, InicJ, Atrap, Aceptado, Finalizado As Boolean
    Public RectSal As Rectangle
    Public Termino As Boolean = False
    Public Declare Function GetTickCount& Lib "kernel32" ()
    Public Nivel As Byte = 1
    Public Escen As Byte = 2
    Public InicPuntJ As Point
    Public HuecoList As New List(Of Punto_F)
    Public ActFotgEstr As New Point(0, 0)
    Public AnchoJ As Byte = 25
    Public LargJ As Byte = 32
    Public TamJug As Byte = 15
    Public TamCuad As Byte = 26
    Public PictPant As PictureBox
    Public Px, Py, Xs, Ys, Xe, Ye, PosXj, PosYj, PosXe, PosYe As Integer
    Public PuntoJ, PuntoE, PosPadre As Point
    Public ContJ As Byte = 0
    Public ContE As Byte = 0
    Public Act, ActE As String
    Public Dibj As Dib
    Public PuertAnch As Byte = 4
    Public PuertLarg As Byte = 40
    Public LLavH As Byte = 0
    Public LLavP As Byte = 0
    Public LLavD As Byte = 0
    Public NumPuerta As Byte = 0
    Public Reloj As New Stopwatch
    Public ExistEnem As Boolean = False
    Public Matr(,) As Dib.Conjunto
    Public Dimx, Dimy As Integer
    Public ListaCerrada As New List(Of Punto_F)
    Public ListaAbierta As New List(Of Punto_F)
    Public ListCamCort As New List(Of Camino)
    Public List_cam_xy As New List(Of Point)
    Public n As Integer = 1
    Public PasosX, PasosY As Integer
    Public CentroC As Integer = TamCuad / 2
    Public CentroJ As Integer = AnchoJ / 2
    Public PosiCent As Integer = CentroC - CentroJ
    Public CentroE As Integer = TamJug / 2
    Public PosiCentE As Integer = CentroC - CentroE
    Public Vidas As Integer = 3

    Public Enem As Enemigo
End Module
