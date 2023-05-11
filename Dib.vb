Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Dib

    Private l As Integer

    Private TamFotgEstr As New Point(48, 48)
    Private Matr_rgb(,) As colores
    Private RectFin As Rectangle
    Private posx, posy As Integer
    Private Rect As Rectangle
    Private reloj As New Stopwatch
    Private rnd As New Random

    Enum colores
        ninguno = -1
        blanco = 0
        rojo = 1
        verde = 2
        azul = 3
    End Enum
    'Private fuente As Font = New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel)
    'Private fuente1 As Font = New Font("Rockwell", 14, FontStyle.Bold, GraphicsUnit.Pixel)
    'Private fuenteg As Font = New Font("Rockwell", 30, FontStyle.Bold, GraphicsUnit.Pixel)
    'Private fuenteg2 As Font = New Font("Rockwell", 26, FontStyle.Bold, GraphicsUnit.Pixel)
    'Private fuenteg3 As Font = New Font("Rockwell", 20, FontStyle.Bold, GraphicsUnit.Pixel)

    Public Enum TipColision
        Pasa
        Nopasa
    End Enum

    Public Enum Tipo
        Espacio
        Bloque
        Hueco
        HuecoG
        Jugador
        Enemigo
        Fin
        Desvio
        PuertaH
        PuertaV
        Salida
    End Enum

    Public Structure Conjunto
        Dim colision As TipColision
        Dim tip As Tipo
    End Structure

    Public Sub New()

    End Sub

    Public Sub New(ByVal Pant As PictureBox)
        PictPant = Pant

    End Sub

    Public Sub inic_matriz(ByVal dimension As Byte)   'INICIALIZAR LA MATRIZ CON "-" ESTO SE HACE CADA VEZ QUE SE QUIERA GENERAR UN RECORRIDO

        For f As Byte = 0 To dimension - 1
            For c As Byte = 0 To dimension - 1
                Matr(f, c).tip = Tipo.Espacio
                Matr(f, c).colision = TipColision.Pasa
            Next
        Next

    End Sub

    Private Sub inic_matriz_rgb(ByVal dimensiones As Integer)

        For f As Byte = 0 To dimensiones - 1
            For c As Byte = 0 To dimensiones - 1
                Matr_rgb(f, c) = colores.ninguno
            Next
        Next

    End Sub

    Public Sub Escenario(ByVal serial As System.IO.Ports.SerialPort)

        'GENERAR LOS RECORRIDOS CON MATRICES 2X2,3X3,4X4 Y 5X5

        'CREAR UNA MATRIZ PRIMERO SIN OBJETOS SIN COLISION CON UNA CON ESAS DIMENSIONES

        'CREAR UN CICLO DONDE SE VAYA REDIMENSIONANDO LA MATRIZ CADA VEZ QUE SE ENCUENTRE EL LIMITE DE 
        'GENERACION DE RECORRIDOS

        'SE TOMO LA DESICION DE ENVIAR 20 ESCENARIOS POR CADA NIVEL
        'SE GUARDARAN EN LA MEMORIA EEPROM CUMPLIENDOSE POS_MEM<256-14 (14 PARA EVITAR QUE SE DESBORDE LA MEOMORIA)
        'DESPUES SE TOMARA UN TIEMPO PARA GUARDAR EN MEMORIA SERIAL, MIENTRAS QUE SE HACE LA ESPERA
        'EN ALTO NIVEL PARA SEGUIR ENVIANDO LOS SIGUIENTES DATOS
        'TENER EN CUENTA HASTA DONDE SE VA A LEER EN LA MEMORIA EEPROM DEL MICROCONTROLADOR

        Dim cont_bytes As Integer = 0

        For dimensiones As Byte = 3 To 5

            For cont_esc As Byte = 1 To 10

                Dimx = dimensiones
                Dimy = dimensiones
                ReDim Matr(dimensiones - 1, dimensiones - 1)
                'INICIALIZAR LA MATRIZ CON "-" ESTO SE HACE CADA VEZ QUE SE QUIERA GENERAR UN RECORRIDO
                inic_matriz(dimensiones)
                Enem.Mov(dimensiones)
                Refresh()

                'SE GENERA PUNTO INICIAL Y FINAL HACIENDO USO DE LA CLASE ENEMIGO


                'List_cam_xy
                'ENVIO SERIAL
                'ENVIAR MODALIDAD DE JUEGO
                serial.Write(Chr(6))    '0x61=97d ó 0x62=98d

                retardo_envio()

                serial.Write(Chr(1))    '0x61=97d ó 0x62=98d

                retardo_envio()

                cont_bytes += 1


                retardo_envio()

                ''''''''''''''''''''''''''0x71=113d 0x72=114d 0x73=115d 0x74=116d

                ''''''''''''''''''''''''''0x70=112d
                Dim niv_ As Byte = dimensiones - 2

                Dim niba As Byte = 7



                serial.Write(Chr(7))

                retardo_envio()

                serial.Write(Chr(niv_))

                retardo_envio()


                cont_bytes += 1
                retardo_envio()

                For x_ As Integer = List_cam_xy.Count - 1 To 0 Step -1
                    'TRANSFORMAR A UN BYTE CON PARTE ALTA Y BAJA
                    'SE MULTIPLICA LA VARIABLE DEL PUNTO "Y" POR 16
                    'Y SE SUMA CON LA X

                    Dim byte_y As Byte = List_cam_xy(x_).X
                    '     byte_y = byte_y * 16
                    Dim byte_x As Byte = List_cam_xy(x_).Y

                    'SE ENVIA CHAR

                    serial.Write(Chr(byte_y))

                    retardo_envio()

                    serial.Write(Chr(byte_x))

                    retardo_envio()

                    cont_bytes += 1
                 
                Next

                List_cam_xy.Clear()

                'REVISAR SI PASO DEL LIMITE DE LA MEMORIA




                'PARA PRUEBA

            Next

        Next

        'GENERAR ESCENARIOS COLORES
        Dim fin As Boolean

2:      For dimensiones As Byte = 2 To 5

            For cont_esc As Byte = 1 To 10


                If dimensiones = 5 Then
                    fin = True
                    dimensiones = 4
                End If

                Dimx = dimensiones
                Dimy = dimensiones
                ReDim Matr_rgb(dimensiones - 1, dimensiones - 1)
                'INICIALIZAR LA MATRIZ CON -1
                inic_matriz_rgb(dimensiones)
                Modo_colores_rgb(dimensiones)
                Refresh2()

                'ENVIO SERIAL
                'ENVIAR MODALIDAD DE JUEGO  SEC_CAM     MEM_COL
                serial.Write(Chr(6))    '0x61=97d ó 0x62=98d

                retardo_envio()

                serial.Write(Chr(2))    '0x61=97d ó 0x62=98d

                retardo_envio()

                cont_bytes += 1
                retardo_envio()
                ''''''''''''''''''''''''''0x71=113d 0x72=114d 0x73=115d 0x74=116d

                ''''''''''''''''''''''''''0x70=112d
                Dim niv_ As Byte = dimensiones - 1

                serial.Write(Chr(7))

                retardo_envio()

                serial.Write(Chr(niv_))

                retardo_envio()

                cont_bytes += 1

                '00=BLANCO Ó NARANJA
                '01=ROJO  1
                '10=VERDE 2 
                '11=AZUL  3

                Dim arreg(dimensiones - 1) As Byte
                Dim pos_arreg As Byte = 0

                Dim fila_col As Integer = 0


                For y_ As Byte = 0 To dimensiones - 1

                    For x_ As Byte = 0 To dimensiones - 1

                        Dim color As colores = Matr_rgb(x_, y_)
                        fila_col *= 4
                        Select Case (color)
                            Case colores.rojo  '01=ROJO  1
                                'GUARDAR ROJO  
                                'ROTAR BITS X4
                                fila_col += 1

                            Case colores.verde  '10=VERDE 2 
                                'GUARDAR VERDE 
                                'ROTAR BITS X4
                                fila_col += 2


                            Case colores.azul  '11=AZUL  3
                                'GUARDAR AZUL
                                'ROTAR BITS X4
                                fila_col += 3

                        End Select

                    Next

                    Dim fila_nib_a = (fila_col And 240) / 16

                    Dim fila_nib_b = fila_col And 15

                    serial.Write(Chr(fila_nib_a))

                    retardo_envio()

                    serial.Write(Chr(fila_nib_b))

                    retardo_envio()

                    cont_bytes += 1

                    fila_col = 0
                Next

                'ENVIAR FILAS

                'PARA PRUEBA

                'If cont_bytes >= 256 - 14 Then
                '    'SE PARA LA TRANSMISION Y SE TOMA UN RETARDO
                '    cont_bytes = 0
                '    retardo_prox_256()
                'End If

            Next
            If fin Then
                dimensiones = 5
                fin = False
            End If

        Next

        'FIN DE LA TRANSMISION MANDAR 0XFF
3:      serial.Write(15)

        retardo_envio()

        serial.Write(15)

        retardo_envio()


    End Sub

    Private Sub retardo_envio()

        reloj.Start()
        Do
        Loop Until reloj.ElapsedMilliseconds > 200
        reloj.Reset()

    End Sub

    Private Sub retardo_prox_256() 'EN REALIDAD PARA LA TRANSMISION DE 256 BYTES DURA 5.825 ms
        reloj.Start()
        Do
        Loop Until reloj.ElapsedMilliseconds > 200
        reloj.Reset()
    End Sub


    Private Sub Modo_colores_rgb(ByVal dimensiones As Byte)

        'MATRIZ CONFORMADAD POR LA COMBINACION DE DOS BITS PARA REPRESENTAR LOS COLORES
        'CASOS
        '00=BLANCO Ó NARANJA
        '01=ROJO  1
        '10=VERDE 2 
        '11=AZUL  3

        Select Case dimensiones
            Case 2
                '2 COLORES: AZUL ROJO, 2 CUADROS CADA COLOR

                'NUM ALEATORIO EN TODA LA MATRIZ PARA UBICAR UN COLOR, AL ASIGNAR EL COLOR
                'EN LAS CELDAS, SE ASIGNA EL OTRO COLOR A LAS OTRAS CELDAS

                asig_colores(2, 2, dimensiones, colores.rojo, colores.azul, colores.ninguno, colores.ninguno)


            Case 3
                '2 COLORES: VERDE ROJO, 4 CUADROS CADA COLOR

                asig_colores(4, 2, dimensiones, colores.rojo, colores.verde, colores.ninguno, colores.ninguno)

            Case 4
                '3 COLORES: ROJO VERDE AZUL, 4 CUADROS CADA COLOR
                asig_colores(4, 3, dimensiones, colores.rojo, colores.verde, colores.azul, colores.ninguno)
                '  asig_colores(4, dimensiones, colores.rojo, colores.verde)

            Case 5
                '3 COLORES: ROJO VERDE AZUL, 4 CUADROS CADA COLOR
                asig_colores(4, 3, dimensiones-1, colores.rojo, colores.verde, colores.azul, colores.ninguno)
                '  asig_colores(4, dimensiones, colores.rojo, colores.verde)

        End Select

    End Sub

    Private Sub asig_colores(ByVal cant_cuadros As Byte, ByVal cant_col As Byte, ByVal dimensiones As Byte, ByVal prim_col As colores, _
                             ByVal seg_col As colores, ByVal terc_col As colores, ByVal cuart_col As colores)
        Dim pto_x, pto_y As Byte
        Dim color_ As colores = prim_col

        For x_col As Byte = 1 To cant_col
            For x_ As Byte = 1 To cant_cuadros
3:              pto_x = rnd.Next(dimensiones)
                pto_y = rnd.Next(dimensiones)
                If Matr_rgb(pto_x, pto_y) = colores.ninguno Then
                    'SI NO SE HA ASIGNADO COLOR 
                    Matr_rgb(pto_x, pto_y) = color_
                Else
                    GoTo 3
                End If
            Next
            Select Case x_col
                Case 1
                    color_ = seg_col
                Case 2
                    color_ = terc_col
            End Select

        Next
    End Sub

    Private Sub GrafEsc(ByVal g As Graphics, ByVal Mat(,) As Conjunto)

        g.DrawRectangle(Pens.White, New Rectangle(0, 0, Dimx * TamCuad, Dimy * TamCuad))
        For y As Byte = 0 To Dimy - 1
            For x As Byte = 0 To Dimx - 1
                posx = x * TamCuad
                posy = y * TamCuad
                Rect = New Rectangle(posx, posy, TamCuad, TamCuad)
                If Mat(x, y).tip = Tipo.Bloque And Mat(x, y).colision = TipColision.Nopasa Then
                    g.DrawRectangle(Pens.Silver, Rect)
                    Dim ColorG As Color = Color.FromArgb(98, 98, 98)
                    Dim Brocha As New SolidBrush(ColorG)
                    g.FillRectangle(Brocha, Rect)
                    'g.DrawImage(My.Resources.Recursos.Bloque, New Rectangle(Rect.Left, Rect.Top, TamCuad, TamCuad))
                End If
            Next
        Next
    End Sub


    Public Function RevColision(ByVal px As Integer, ByVal py As Integer) As TipColision
        If px < 0 Or px >= Dimx Then
            Return TipColision.Nopasa
        End If
        If py < 0 Or py >= Dimy Then
            Return TipColision.Nopasa
        End If
        Return Matr(px, py).colision

    End Function


    Public Sub situar_pared_aleat(ByVal fila_esc As Byte, ByVal colum_esc As Byte)

        Matr(colum_esc, fila_esc).tip = Tipo.Bloque
        Matr(colum_esc, fila_esc).colision = TipColision.Nopasa

    End Sub

    Public Function TipObjeto(ByVal px As Integer, ByVal py As Integer) As Tipo
        If px < 0 Or px >= Dimx Then
            Return Tipo.Bloque
        End If
        If py < 0 Or py >= Dimy Then
            Return Tipo.Bloque
        End If
        Return Matr(px, py).tip

    End Function

    Public Function CajaoB(ByVal Px As Integer, ByVal Py As Integer, ByVal Tipo As Tipo) As Rectangle
        Dim PosicX As Integer = Px * TamCuad
        Dim PosicY As Integer = Py * TamCuad

        If Tipo = Tipo.Bloque Then
            Return New Rectangle(PosicX, PosicY, TamCuad, TamCuad)
        ElseIf Tipo = Tipo.PuertaH Then
            Return New Rectangle(PosicX, PosicY, PuertLarg + 6, PuertAnch + 6)
        ElseIf Tipo = Tipo.PuertaV Then
            Return New Rectangle(PosicX, PosicY, PuertAnch, PuertLarg)
        End If


    End Function

    Private Function cant_filas_matr()
        Dim longitud_mat As Byte = Matr_rgb.Length
        Dim encon As Boolean
        Dim mult As Byte
        For mult = 2 To 4
            If mult * mult = longitud_mat Then
                encon = True
                Exit For
            End If
        Next
        Return mult
    End Function

    Public Sub Refresh2()

        Dim Bit As Bitmap = New Bitmap(PictPant.Width, PictPant.Height)
        Dim g As Graphics = Graphics.FromImage(Bit)
        '    PictPant.BackColor = Col

        Dim cant_filas As Byte = cant_filas_matr()
        Dim color_ As colores

        g.DrawRectangle(Pens.White, New Rectangle(0, 0, cant_filas * TamCuad, cant_filas * TamCuad))

        For x__ As Byte = 0 To cant_filas - 1
            For y__ As Byte = 0 To cant_filas - 1
                color_ = Matr_rgb(x__, y__)

                Select Case color_
                    Case colores.rojo
                        g.FillRectangle(Brushes.Red, New Rectangle(x__ * TamCuad, y__ * TamCuad, TamCuad, TamCuad))
                    Case colores.verde
                        g.FillRectangle(Brushes.Green, New Rectangle(x__ * TamCuad, y__ * TamCuad, TamCuad, TamCuad))
                    Case colores.azul
                        g.FillRectangle(Brushes.Blue, New Rectangle(x__ * TamCuad, y__ * TamCuad, TamCuad, TamCuad))


                End Select

            Next
        Next


        PictPant.Image = Bit
        PictPant.Refresh()

    End Sub


    Public Sub Refresh()

        Dim Bit As Bitmap = New Bitmap(PictPant.Width, PictPant.Height)
        Dim g As Graphics = Graphics.FromImage(Bit)
        '    PictPant.BackColor = Col

        GrafEsc(g, Matr)

        If Termino Then
            ' Camino(Marcado)
            For q As Integer = 0 To ListCamCort.Count - 1
                g.FillRectangle(Brushes.DarkRed, ListCamCort.Item(q).Rect)
                '   retardo()
            Next
        End If

        PictPant.Image = Bit
        PictPant.Refresh()



        ListCamCort.Clear()
    End Sub

  

End Class

