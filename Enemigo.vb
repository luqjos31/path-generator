Imports System.Math
Imports System
Public Class Enemigo
    Dim ContX, ContY As Integer
    Private PasPost As Double = 2
    Private MatrCoordX, MatrCoordY As Double
    Private MenorF As Integer
    Private ProfColi As Byte = 10
    Private Direc As Camino
    Private TipoI, TipoD, TipoAr, TipoAb, TipoIzqArr, TipoIzqAbj, TipoDerArr, TipoDerAbj As Dib.Tipo
    Private GinicI, GinicD, GinicAr, GinicAb, GinicIAr, GinicIAb, GinicDAr, GinicDAb As Boolean
    Private PosListAb As Integer
    Private F_izq, F_der, F_arri, F_abj, F_izq_arri, F_izq_abj, F_der_arri, F_der_abj As Integer
    Private Getick As String
    Private NumAlt As Byte
    Private ColisionI, ColisionD, ColisionAb, ColisionAr, ColiIzqAbj, ColiIzqArr, ColiDerAbj, ColiDerArr As Dib.TipColision
    Private lista_sec_cad As New List(Of String)

    Public Enum TipMov
        HorizontPuntIzq
        HorizontPuntDer
        VerticalPuntArrb
        VerticalPuntAbj
        Ang45ºPuntAbj
        Ang45ºPuntArrb
        Ang135ºPuntAbj
        Ang135ºPuntArrb
        Nulo
    End Enum

    Private Structure Lado
        Dim LadoI As Integer
        Dim LadoD As Integer
        Dim LadoAr As Integer
        Dim LadoAb As Integer
    End Structure

    Public Sub inic_lista_sec()
        lista_sec_cad.Clear()
    End Sub

    Dim Pl As Lado

    Public Sub New(ByVal X As Byte, ByVal Y As Byte)
        Xe = X
        Ye = Y
        'Guardamos la Posicion inicial a la lista cerrada
        ListaCerrada.Add(New Punto_F(0, Xe, Ye, 0, TipMov.Nulo, 0))
        PosXe = Xe * TamCuad + PosiCent
        PosYe = Ye * TamCuad + PosiCent
        PuntoE = New Point(PosXe, PosYe)
        PosPadre = New Point(PosXe, PosYe)
    End Sub

    Public Sub New()

    End Sub


    Public Sub Fotogr(ByVal Cont As Byte)
        Cont = Cont
    End Sub

    Public Function PosAleat()
        Do
            Getick = GetTickCount&.ToString
            NumAlt = Val(Mid(Getick, Getick.Length - 1, 2))
        Loop Until NumAlt < Dimx - 1
        Return NumAlt
    End Function

    Private Function BuscSal() As Point
        Dim PuntoS As Point
        For x As Integer = 0 To Dimx - 1
            For y As Integer = 0 To Dimy - 1
                Dim Obj As Dib.Tipo = Dibj.TipObjeto(x, y)
                If Obj = Dib.Tipo.Salida Then
                    PuntoS = New Point(x, y)
                    Return PuntoS
                End If
            Next
        Next
    End Function

    Public Sub Mov(ByVal dimension As Byte) 'ByVal Orient As String)
       
        Dim CajaX, CajaY As Double
        Dim rnd As New Random
        Dim cont_fila_aleatorio As Byte = 0 '1
        Dim intent_gen As Byte = 0

        'Select Case Orient
        '    Case "SitAleat"

        '        Lug = False
        '        Do
        '            CajaX = PosAleatX()
        '            CajaY = PosAleatY()
        '            TipoOb = Dibj.TipObjeto(CajaX, CajaY)

        '            If TipoOb = Dib.Tipo.Espacio Or TipoOb = Dib.Tipo.Desvio Or TipoOb = Dib.Tipo.Fin Then
        '                Lug = True
        '            End If
        '        Loop Until Lug = True

        '    Case "Jug"
        '        Jug = New Jugador
        '        CajaX = Math.Round(Jug.CajaJug.Left / TamCuad)
        '        CajaY = Math.Round(Jug.CajaJug.Top / TamCuad)

        '    Case "Salir"
        '        PuntSal = BuscSal()
        Dibj = New Dib()

2:      Dim punto_inic As Byte = rnd.Next(dimension)
        Dim punto_final As Byte = rnd.Next(dimension)

        Dibj.inic_matriz(dimension)
        ''COORDENADAS DEL OBJETIVO
        CajaX = punto_final
        CajaY = dimension - 1

        'End Select
1:      ListaAbierta.Clear()
        ListaCerrada.Clear()

        PosPadre = New Point(punto_inic * TamCuad, 0)
        ListaCerrada.Add(New Punto_F(0, punto_inic, 0, 0, TipMov.Nulo, 0))

        Dim AuxRect As Rectangle
        Dim AuxPx, AuxPy, L, AuxG, TotalGPadr, AuxL, GPadre, AuxP As Integer
        Dim Busc, BuscCerrad, Lug, otro_camino As Boolean
        Dim AuxHijo As TipMov
        Dim n As Byte = 10

        Dim Px, Py As Integer

        Do
            'POSPADRE ES LA POSICION DEL ENEMIGO
            'BUSCAR POSICION ALEATORIA 

            MatrCoordX = Math.Round(PosPadre.X / TamCuad)
            MatrCoordY = Math.Round(PosPadre.Y / TamCuad)

            Pl.LadoI = MatrCoordX - 1
            Pl.LadoD = MatrCoordX + 1
            Pl.LadoAr = MatrCoordY - 1
            Pl.LadoAb = MatrCoordY + 1

            'Verficar Colision
            ColisionI = Dibj.RevColision(Pl.LadoI, MatrCoordY)
            ColisionD = Dibj.RevColision(Pl.LadoD, MatrCoordY)
            ColisionAr = Dibj.RevColision(MatrCoordX, Pl.LadoAr)
            ColisionAb = Dibj.RevColision(MatrCoordX, Pl.LadoAb)
            ColiIzqArr = Dibj.RevColision(Pl.LadoI, Pl.LadoAr)
            ColiIzqAbj = Dibj.RevColision(Pl.LadoI, Pl.LadoAb)
            ColiDerArr = Dibj.RevColision(Pl.LadoD, Pl.LadoAr)
            ColiDerAbj = Dibj.RevColision(Pl.LadoD, Pl.LadoAb)

            'Verificar Tipo de Objeto Enemigo
            TipoI = Dibj.TipObjeto(Pl.LadoI, MatrCoordY)
            TipoD = Dibj.TipObjeto(Pl.LadoD, MatrCoordY)
            TipoAr = Dibj.TipObjeto(MatrCoordX, Pl.LadoAr)
            TipoAb = Dibj.TipObjeto(MatrCoordX, Pl.LadoAb)
            TipoIzqArr = Dibj.TipObjeto(Pl.LadoI, Pl.LadoAr)
            TipoIzqAbj = Dibj.TipObjeto(Pl.LadoI, Pl.LadoAb)
            TipoDerArr = Dibj.TipObjeto(Pl.LadoD, Pl.LadoAr)
            TipoDerAbj = Dibj.TipObjeto(Pl.LadoD, Pl.LadoAb)
            Dim TipoSit As New List(Of Dib.Tipo)
            TipoSit.Add(TipoI)
            TipoSit.Add(TipoD)
            TipoSit.Add(TipoAr)
            TipoSit.Add(TipoAb)
            TipoSit.Add(TipoIzqArr)
            TipoSit.Add(TipoIzqAbj)
            TipoSit.Add(TipoDerArr)
            TipoSit.Add(TipoDerAbj)

            'Algoritmo A* para el camino más óptimo!
            'F = G + H
            'G = Valor desde la posicion del Padre hasta la posicion adyacente
            'H = Distancia de ese cuadro en evaluacion hasta el obejtivo , sin recorridos diagonales 
            'Valores de G=10 para movimientos horizontales y verticales, y G=14 para diagonales

            'Se tiene la Posicion inicial Guardada en la lista cerrada
            'Se analiza los cuadros adyacentes


            'Evitar que encuentre Jugador con puerta cerrada

            If CajaX <> Pl.LadoI Or CajaY <> Pl.LadoAr Then
                'Lado Izquierdo
                BuscCerrad = Busqueda(Pl.LadoI, MatrCoordY, ListaCerrada)
                If ColisionI <> Dib.TipColision.Nopasa And TipoI <> Dib.Tipo.Hueco And TipoI <> Dib.Tipo.HuecoG And BuscCerrad = False Then

                    L = (Math.Abs(Pl.LadoI - CajaX) + Math.Abs(MatrCoordY - CajaY)) * n

                    'Revisa si este cuadro se habia analizado y agregado en la lista abierta, si es asi se verifica si el valor de G es menor 
                    Busc = Busqueda(Pl.LadoI, MatrCoordY, ListaAbierta)
                    If Busc Then
                        TotalGPadr = 10 + AuxG
                        If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                            F_izq = ListaAbierta.Item(PosListAb).Gcuad + L
                        Else
                            'Recalculamos G,F de este cuadro
                            F_izq = GPadre + 10 + L
                            Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                            Px = ClsPunt.PXant
                            Py = ClsPunt.PYant
                            ListaAbierta.RemoveAt(PosListAb)
                            ListaAbierta.Insert(PosListAb, New Punto_F(F_izq, Px, Py, GPadre + 10, TipMov.HorizontPuntDer, L))
                        End If
                    Else
                        If GinicI = False Then
                            F_izq = 10 + L
                            ListaAbierta.Add(New Punto_F(F_izq, Pl.LadoI, MatrCoordY, 10, TipMov.HorizontPuntDer, L))
                            GinicI = True
                        Else
                            F_izq = GPadre + 10 + L
                            ListaAbierta.Add(New Punto_F(F_izq, Pl.LadoI, MatrCoordY, GPadre + 10, TipMov.HorizontPuntDer, L))
                        End If

                    End If
                End If


                'Lado Derecho
                BuscCerrad = Busqueda(Pl.LadoD, MatrCoordY, ListaCerrada)
                If ColisionD <> Dib.TipColision.Nopasa And TipoD <> Dib.Tipo.Hueco And TipoD <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    L = (Math.Abs(Pl.LadoD - CajaX) + Math.Abs(MatrCoordY - CajaY)) * n
                    Busc = Busqueda(Pl.LadoD, MatrCoordY, ListaAbierta)
                    If Busc Then
                        TotalGPadr = 10 + AuxG
                        If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                            F_der = ListaAbierta.Item(PosListAb).Gcuad + L
                        Else
                            F_der = GPadre + 10 + L
                            Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                            Px = ClsPunt.PXant
                            Py = ClsPunt.PYant
                            ListaAbierta.RemoveAt(PosListAb)
                            ListaAbierta.Insert(PosListAb, New Punto_F(F_der, Px, Py, GPadre + 10, TipMov.HorizontPuntIzq, L))

                        End If
                    Else
                        If GinicD = False Then
                            F_der = 10 + L
                            ListaAbierta.Add(New Punto_F(F_der, Pl.LadoD, MatrCoordY, 10, TipMov.HorizontPuntIzq, L))
                            GinicD = True
                        Else
                            F_der = GPadre + 10 + L
                            ListaAbierta.Add(New Punto_F(F_der, Pl.LadoD, MatrCoordY, GPadre + 10, TipMov.HorizontPuntIzq, L))
                        End If
                    End If
                End If



                'Lado Superior

                BuscCerrad = Busqueda(MatrCoordX, Pl.LadoAr, ListaCerrada)
                If ColisionAr <> Dib.TipColision.Nopasa And TipoAr <> Dib.Tipo.Hueco And TipoAr <> Dib.Tipo.HuecoG And BuscCerrad = False Then

                    L = (Math.Abs(Pl.LadoAr - CajaY) + Math.Abs(MatrCoordX - CajaX)) * n
                    Busc = Busqueda(MatrCoordX, Pl.LadoAr, ListaAbierta)
                    If Busc Then
                        TotalGPadr = 10 + AuxG
                        If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                            F_arri = ListaAbierta.Item(PosListAb).Gcuad + L
                        Else
                            F_arri = GPadre + 10 + L
                            Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                            Px = ClsPunt.PXant
                            Py = ClsPunt.PYant
                            ListaAbierta.RemoveAt(PosListAb)
                            ListaAbierta.Insert(PosListAb, New Punto_F(F_arri, Px, Py, GPadre + 10, TipMov.VerticalPuntAbj, L))
                        End If
                    Else
                        If GinicAr = False Then
                            F_arri = 10 + L
                            ListaAbierta.Add(New Punto_F(F_arri, MatrCoordX, Pl.LadoAr, 10, TipMov.VerticalPuntAbj, L))
                            GinicAr = True
                        Else
                            F_arri = GPadre + 10 + L
                            ListaAbierta.Add(New Punto_F(F_arri, MatrCoordX, Pl.LadoAr, GPadre + 10, TipMov.VerticalPuntAbj, L))
                        End If
                    End If
                End If

                'Lado Inferior
                BuscCerrad = Busqueda(MatrCoordX, Pl.LadoAb, ListaCerrada)
                If ColisionAb <> Dib.TipColision.Nopasa And TipoAb <> Dib.Tipo.Hueco And TipoAb <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    L = (Math.Abs(Pl.LadoAb - CajaY) + Math.Abs(MatrCoordX - CajaX)) * n
                    Busc = Busqueda(MatrCoordX, Pl.LadoAb, ListaAbierta)
                    If Busc Then
                        TotalGPadr = 10 + AuxG
                        If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                            F_abj = ListaAbierta.Item(PosListAb).Gcuad + L
                        Else
                            F_abj = GPadre + 10 + L
                            Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                            Px = ClsPunt.PXant
                            Py = ClsPunt.PYant
                            ListaAbierta.RemoveAt(PosListAb)
                            ListaAbierta.Insert(PosListAb, New Punto_F(F_abj, Px, Py, GPadre + 10, TipMov.VerticalPuntArrb, L))

                        End If
                    Else
                        If GinicAb = False Then
                            F_abj = 10 + L
                            ListaAbierta.Add(New Punto_F(F_abj, MatrCoordX, Pl.LadoAb, 10, TipMov.VerticalPuntArrb, L))
                            GinicAb = True
                        Else
                            F_abj = GPadre + 10 + L
                            ListaAbierta.Add(New Punto_F(F_abj, MatrCoordX, Pl.LadoAb, GPadre + 10, TipMov.VerticalPuntArrb, L))
                        End If
                    End If
                End If

                'Lado IzqSup
                'Para no analizar Cuadrados que son espacios y los avecina un bloque o cualquier obstaculo

                BuscCerrad = Busqueda(Pl.LadoI, Pl.LadoAr, ListaCerrada)
                If ColiIzqArr <> Dib.TipColision.Nopasa And TipoIzqArr <> Dib.Tipo.Hueco And TipoIzqArr <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    If ColisionI <> Dib.TipColision.Nopasa And ColisionAr <> Dib.TipColision.Nopasa Then
                        L = (Math.Abs(Pl.LadoI - CajaX) + Math.Abs(Pl.LadoAr - CajaY)) * n
                        Busc = Busqueda(Pl.LadoI, Pl.LadoAr, ListaAbierta)
                        If Busc Then
                            TotalGPadr = 14 + AuxG
                            If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                                F_izq_arri = ListaAbierta.Item(PosListAb).Gcuad + L
                            Else
                                F_izq_arri = GPadre + 14 + L
                                Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                                Px = ClsPunt.PXant
                                Py = ClsPunt.PYant
                                ListaAbierta.RemoveAt(PosListAb)
                                ListaAbierta.Insert(PosListAb, New Punto_F(F_izq_arri, Px, Py, GPadre + 14, TipMov.Ang135ºPuntAbj, L))
                            End If
                        Else
                            If GinicIAr = False Then
                                F_izq_arri = 14 + L
                                ListaAbierta.Add(New Punto_F(F_izq_arri, Pl.LadoI, Pl.LadoAr, 14, TipMov.Ang135ºPuntAbj, L))
                                GinicIAr = True
                            Else
                                F_izq_arri = GPadre + 14 + L
                                ListaAbierta.Add(New Punto_F(F_izq_arri, Pl.LadoI, Pl.LadoAr, GPadre + 14, TipMov.Ang135ºPuntAbj, L))
                            End If


                        End If
                    End If
                End If

                'Lado IzqInf
                BuscCerrad = Busqueda(Pl.LadoI, Pl.LadoAb, ListaCerrada)
                If ColiIzqAbj <> Dib.TipColision.Nopasa And TipoIzqAbj <> Dib.Tipo.Hueco And TipoIzqAbj <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    If ColisionI <> Dib.TipColision.Nopasa And ColisionAb <> Dib.TipColision.Nopasa Then
                        L = (Math.Abs(Pl.LadoI - CajaX) + Math.Abs(Pl.LadoAb - CajaY)) * n
                        Busc = Busqueda(Pl.LadoI, Pl.LadoAb, ListaAbierta)
                        If Busc Then
                            TotalGPadr = 14 + AuxG
                            If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                                F_izq_abj = ListaAbierta.Item(PosListAb).Gcuad + L
                            Else
                                F_izq_abj = GPadre + 14 + L
                                Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                                Px = ClsPunt.PXant
                                Py = ClsPunt.PYant
                                ListaAbierta.RemoveAt(PosListAb)
                                ListaAbierta.Insert(PosListAb, New Punto_F(F_izq_abj, Px, Py, GPadre + 14, TipMov.Ang45ºPuntArrb, L))

                            End If
                        Else
                            If GinicIAb = False Then
                                F_izq_abj = 14 + L
                                ListaAbierta.Add(New Punto_F(F_izq_abj, Pl.LadoI, Pl.LadoAb, 14, TipMov.Ang45ºPuntArrb, L))
                                GinicIAb = True
                            Else
                                F_izq_abj = GPadre + 14 + L
                                ListaAbierta.Add(New Punto_F(F_izq_abj, Pl.LadoI, Pl.LadoAb, GPadre + 14, TipMov.Ang45ºPuntArrb, L))
                            End If

                        End If
                    End If
                End If

                '  'Lado DerSup
                BuscCerrad = Busqueda(Pl.LadoD, Pl.LadoAr, ListaCerrada)
                If ColiDerArr <> Dib.TipColision.Nopasa And TipoDerArr <> Dib.Tipo.Hueco And TipoDerArr <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    If ColisionD <> Dib.TipColision.Nopasa And ColisionAr <> Dib.TipColision.Nopasa Then
                        L = (Math.Abs(Pl.LadoD - CajaX) + Math.Abs(Pl.LadoAr - CajaY)) * n
                        Busc = Busqueda(Pl.LadoD, Pl.LadoAr, ListaAbierta)
                        If Busc Then
                            TotalGPadr = 14 + AuxG
                            If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                                F_der_arri = ListaAbierta.Item(PosListAb).Gcuad + L
                            Else
                                F_der_arri = GPadre + 14 + L
                                Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                                Px = ClsPunt.PXant
                                Py = ClsPunt.PYant
                                ListaAbierta.RemoveAt(PosListAb)
                                ListaAbierta.Insert(PosListAb, New Punto_F(F_der_arri, Px, Py, GPadre + 14, TipMov.Ang45ºPuntAbj, L))

                            End If
                        Else
                            If GinicDAr = False Then
                                F_der_arri = 14 + L
                                ListaAbierta.Add(New Punto_F(F_der_arri, Pl.LadoD, Pl.LadoAr, 14, TipMov.Ang45ºPuntAbj, L))
                                GinicDAr = True
                            Else
                                F_der_arri = GPadre + 14 + L
                                ListaAbierta.Add(New Punto_F(F_der_arri, Pl.LadoD, Pl.LadoAr, GPadre + 14, TipMov.Ang45ºPuntAbj, L))
                            End If

                        End If
                    End If

                    'Lado DerInf

                End If
                BuscCerrad = Busqueda(Pl.LadoD, Pl.LadoAb, ListaCerrada)
                If ColiDerAbj <> Dib.TipColision.Nopasa And TipoDerAbj <> Dib.Tipo.Hueco And TipoDerAbj <> Dib.Tipo.HuecoG And BuscCerrad = False Then
                    If ColisionD <> Dib.TipColision.Nopasa And ColisionAb <> Dib.TipColision.Nopasa Then
                        L = (Math.Abs(Pl.LadoD - CajaX) + Math.Abs(Pl.LadoAb - CajaY)) * n
                        Busc = Busqueda(Pl.LadoD, Pl.LadoAb, ListaAbierta)
                        If Busc Then
                            TotalGPadr = 14 + AuxG
                            If TotalGPadr >= ListaAbierta.Item(PosListAb).Gcuad Then
                                F_der_abj = ListaAbierta.Item(PosListAb).Gcuad + L
                            Else
                                F_der_abj = GPadre + 14 + L
                                Dim ClsPunt As Punto_F = ListaAbierta.Item(PosListAb)
                                Px = ClsPunt.PXant
                                Py = ClsPunt.PYant
                                ListaAbierta.RemoveAt(PosListAb)
                                ListaAbierta.Insert(PosListAb, New Punto_F(F_der_abj, Px, Py, GPadre + 14, TipMov.Ang135ºPuntArrb, L))
                            End If
                        Else
                            If GinicDAb = False Then
                                F_der_abj = 14 + L
                                ListaAbierta.Add(New Punto_F(F_der_abj, Pl.LadoD, Pl.LadoAb, 14, TipMov.Ang135ºPuntArrb, L))
                                GinicDAb = True
                            Else
                                F_der_abj = GPadre + 14 + L
                                ListaAbierta.Add(New Punto_F(F_der_abj, Pl.LadoD, Pl.LadoAb, GPadre + 14, TipMov.Ang135ºPuntArrb, L))
                            End If

                        End If
                    End If
                End If

                Dim top As Integer = 5000

                'Un tope cualquiera considerable
                MenorF = top

                'Se busca la F mas baja
                For k As Integer = 0 To ListaAbierta.Count - 1

                    Dim Fresult As Punto_F = ListaAbierta.Item(k)

                    If Fresult.Fs < MenorF Then
                        MenorF = Fresult.Fs
                        AuxRect = Fresult.RectCerr
                        AuxP = k
                        AuxG = Fresult.Gcuad
                        AuxL = Fresult.DistManh
                        AuxHijo = Fresult.TipoMovi
                        AuxPx = Fresult.PXant
                        AuxPy = Fresult.PYant
                    End If

                Next

                If AuxP < ListaAbierta.Count Then
                    ListaAbierta.RemoveAt(AuxP)
                End If
                GPadre = AuxG
                PosPadre = New Point(AuxRect.Left, AuxRect.Top)
                'Agregar el tipo de movimiento!!!!
                ListaCerrada.Add(New Punto_F(MenorF, AuxRect, GPadre, AuxL, AuxHijo, AuxPx, AuxPy, AuxP))

                If ListaCerrada.Count > dimension * dimension Then
                    otro_camino = True
                    Exit Do
                End If

                '     Dibj.Refresh()

            Else
                AuxL = 0
            End If
            TipoSit.Clear()
        Loop Until AuxL = 0


        If otro_camino Then
            'MATRIZ POR DEFECTO CON VACIO EN LA CLASE DIB
            Dibj.inic_matriz(dimension)
            otro_camino = False
            GoTo 1
        End If

        'SE AUMENTA LOS INTENTOS, SI SOBREPASA EL TOPE INICIALIZAR LA MATRIZ
        intent_gen += 1
        Dim tope_gen As Byte = 100
        If intent_gen > tope_gen Then
            intent_gen = 0
            GoTo 2
        End If


        'REVISAR SI LA CANTIDAD DE CUADROS DE RECORRIDO SON LOS CORRECTOS
        Dim colum_aleat As Byte
        Select Case dimension
            Case 2 '3 CUADROS EN TOTAL
                If ListaCerrada.Count < 4 Then
                    'SE AGREGA PARED EN EL ESCENARIO ALEATORIAMENTE
                    colum_aleat = rnd.Next(dimension)
                    Dibj.situar_pared_aleat(cont_fila_aleatorio, colum_aleat)
                    cont_fila_aleatorio += 1
                    If cont_fila_aleatorio > dimension - 2 Then
                        cont_fila_aleatorio = 0 '1
                    End If
                    GoTo 1
                End If

            Case 3 '4 CUADROS EN TOTAL
                If ListaCerrada.Count < 5 Then
                    'SE AGREGA PARED EN EL ESCENARIO ALEATORIAMENTE
                    colum_aleat = rnd.Next(dimension)
                    Dibj.situar_pared_aleat(cont_fila_aleatorio, colum_aleat)
                    cont_fila_aleatorio += 1
                    If cont_fila_aleatorio > dimension - 2 Then
                        cont_fila_aleatorio = 0 '1
                    End If
                    GoTo 1
                End If

            Case 4 '6 CUADROS EN TOTAL
                If ListaCerrada.Count < 7 Then
                    'SE AGREGA PARED EN EL ESCENARIO ALEATORIAMENTE
                    colum_aleat = rnd.Next(dimension)
                    Dibj.situar_pared_aleat(cont_fila_aleatorio, colum_aleat)
                    cont_fila_aleatorio += 1
                    If cont_fila_aleatorio > dimension - 2 Then
                        cont_fila_aleatorio = 0 '1
                    End If
                    GoTo 1
                End If

            Case 5 '8 CUADROS EN TOTAL
                If ListaCerrada.Count < 9 Then '9
                    'SE AGREGA PARED EN EL ESCENARIO ALEATORIAMENTE
                    colum_aleat = rnd.Next(dimension)
                    Dibj.situar_pared_aleat(cont_fila_aleatorio, colum_aleat)
                    cont_fila_aleatorio += 1
                    If cont_fila_aleatorio > dimension - 2 Then
                        cont_fila_aleatorio = 0 '1
                    End If
                    GoTo 1

                End If

        End Select

        Camino(dimension)



        'REVISAR SI ESE CAMINO YA SE GENERÓ, ADEMAS DE GUARDAR ESO EN UNA LISTA DE MATRICES


        Termino = True

    End Sub


    Private Function rev_cam_rep() As Boolean



    End Function

    

    Public ReadOnly Property ListCamino() As Integer
        Get
            Return ListCamCort.Count
        End Get
    End Property



    Public Sub Camino(ByVal dimension As Byte)
        Dim Celda As Integer = ListaCerrada.Count - 1
        Dim Fil, Col As Integer
        Do

            If Celda > -1 Then
                Dim DirMov As Punto_F = ListaCerrada.Item(Celda)
                Dim Camino As TipMov = DirMov.TipoMovi
                ListCamCort.Add(New Camino(New Rectangle(DirMov.PXant * TamCuad, DirMov.PYant * TamCuad, TamCuad, TamCuad), Camino))
                List_cam_xy.Add(New Point(DirMov.PYant + 1, DirMov.PXant + 1))

                Select Case Camino

                    Case TipMov.HorizontPuntIzq
                        Fil = DirMov.PXant - 1
                        Col = DirMov.PYant
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.HorizontPuntDer
                        Fil = DirMov.PXant + 1
                        Col = DirMov.PYant
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.VerticalPuntArrb
                        Fil = DirMov.PXant
                        Col = DirMov.PYant - 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.VerticalPuntAbj
                        Fil = DirMov.PXant
                        Col = DirMov.PYant + 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.Ang45ºPuntArrb
                        Fil = DirMov.PXant + 1
                        Col = DirMov.PYant - 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.Ang45ºPuntAbj
                        Fil = DirMov.PXant - 1
                        Col = DirMov.PYant + 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.Ang135ºPuntArrb
                        Fil = DirMov.PXant - 1
                        Col = DirMov.PYant - 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)

                    Case TipMov.Ang135ºPuntAbj
                        Fil = DirMov.PXant + 1
                        Col = DirMov.PYant + 1
                        Celda = BusqPosicion(Fil, Col, ListaCerrada)


                End Select
            Else
                Celda = False

            End If
        Loop Until Celda = False

        ListCamCort.Add(New Camino(ListaCerrada(Celda).RectCerr, ListaCerrada(Celda).TipoMovi))
        List_cam_xy.Add(New Point((ListaCerrada(Celda).RectCerr.Y / TamCuad) + 1, (ListaCerrada(Celda).RectCerr.X / TamCuad) + 1))

        ListaAbierta.Clear()
        ListaCerrada.Clear()

    End Sub
    Private Function Busqueda(ByVal Fx As Integer, ByVal Cy As Integer, ByVal Lista As List(Of Punto_F)) As Boolean
        Dim encontrado As Boolean = False
        Dim u As Integer = 0
        If Lista.Count > 0 Then
            Do
                Dim Fencon As Punto_F = Lista.Item(u)
                If Fencon.PXant = Fx And Fencon.PYant = Cy Then
                    encontrado = True
                    PosListAb = u
                Else
                    u += 1
                End If
            Loop Until u >= Lista.Count Or encontrado = True
        End If
        If encontrado = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function BusqPosicion(ByVal f As Integer, ByVal c As Integer, ByVal Lista As List(Of Punto_F)) As Integer
        Dim encontrado As Boolean = False
        Dim u As Integer = 0
        If Lista.Count > 0 Then
            Do
                Dim Fencon As Punto_F = Lista.Item(u)
                If Fencon.PXant = f And Fencon.PYant = c Then
                    encontrado = True
                    Return u
                Else
                    u += 1
                End If
            Loop Until u >= Lista.Count Or encontrado = True
        End If
        If encontrado = False Then
            Return 0
        End If

    End Function

    Public ReadOnly Property Fmenor() As Integer
        Get
            Return MenorF
        End Get
    End Property

    Public ReadOnly Property CajaEnem() As Rectangle
        Get
            Return New Rectangle(PuntoE.X, PuntoE.Y, TamJug, TamJug)
        End Get
    End Property

    Public ReadOnly Property CajaColi() As Rectangle
        Get
            Return New Rectangle(PuntoE.X + ProfColi, PuntoE.Y + ProfColi, TamJug - ProfColi, TamJug - ProfColi)
        End Get
    End Property

  
    Public Sub MovPasos()

        Dim Cond As Integer
        If Termino Then
            Dim k As Integer = ListCamino - n
            If k > -1 Then
                Direc = ListCamCort.Item(k)
                PuntoE = New Point(Direc.Rect.Left + PosiCent + PasosX, Direc.Rect.Top + PosiCent + PasosY)
                Dim PosPost As Integer = ListCamino - n - 1
                If PosPost > -1 Then

                    Select Case Direc.TipMove

                        Case Enemigo.TipMov.HorizontPuntDer
                            ActE = "I"
                            PasosX -= PasPost
                            Cond = Direc.Rect.Left + PosiCent + PasosX
                            If Cond <= ListCamCort.Item(PosPost).Rect.Left Then
                                n += 1
                                PasosX = 0
                                PasosY = 0
                            End If

                        Case Enemigo.TipMov.HorizontPuntIzq
                            ActE = "D"
                            PasosX += PasPost
                            Cond = Direc.Rect.Left + PosiCent + PasosX
                            If Cond >= ListCamCort.Item(PosPost).Rect.Right - 30 Then
                                n += 1
                                PasosX = 0
                                PasosY = 0
                            End If

                        Case Enemigo.TipMov.VerticalPuntAbj
                            ActE = "Ar"
                            PasosY -= PasPost
                            Cond = Direc.Rect.Top + PosiCent + PasosY
                            If Cond <= ListCamCort.Item(PosPost).Rect.Top Then
                                n += 1
                                PasosX = 0
                                PasosY = 0
                            End If

                        Case Enemigo.TipMov.VerticalPuntArrb
                            ActE = "Ab"
                            PasosY += PasPost
                            Cond = Direc.Rect.Top + PosiCent + PasosY
                            If Cond >= ListCamCort.Item(PosPost).Rect.Bottom - 30 Then
                                n += 1
                                PasosX = 0
                                PasosY = 0

                            End If

                        Case Enemigo.TipMov.Ang45ºPuntArrb
                            ActE = "Ab"
                            n += 1
                            PasosX = 0
                            PasosY = 0

                        Case Enemigo.TipMov.Ang135ºPuntArrb
                            ActE = "Ab"
                            n += 1
                            PasosX = 0
                            PasosY = 0


                        Case Enemigo.TipMov.Ang45ºPuntAbj
                            ActE = "Ar"
                            n += 1
                            PasosX = 0
                            PasosY = 0


                        Case Enemigo.TipMov.Ang135ºPuntAbj
                            ActE = "Ar"
                            n += 1
                            PasosX = 0
                            PasosY = 0

                    End Select
                Else

                    Termino = False
                    ListCamCort.Clear()
                    n = 1
                End If
            Else
                Termino = False
                ListCamCort.Clear()
                n = 1
            End If
        End If


    End Sub

  
End Class

