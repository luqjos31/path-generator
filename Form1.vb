Imports System.IO
Public Class Form1
    Dim WithEvents serial As New System.IO.Ports.SerialPort
    Dim cad As String
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        serial.PortName = "COM3"
        serial.BaudRate = 9600
        serial.DataBits = 8
        serial.Parity = IO.Ports.Parity.None
        serial.StopBits = IO.Ports.StopBits.One
        serial.ReadBufferSize = 8096
        serial.ReceivedBytesThreshold = 50
        'serial.Open()

        serial.Encoding = System.Text.Encoding.GetEncoding(1252)


        Dibj = New Dib(Pant)
        Enem = New Enemigo()
    End Sub

    Private Sub Rx(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles serial.DataReceived

        Dim dato = serial.ReadExisting

        Dim cant_ = dato.Length

        'OBTENER LOS NUMEROS EN DECIMAL
        dato = Trim(dato)
    End Sub


    'Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
    '    Dibj.Refresh()
    'End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Enem.inic_lista_sec()
        Dibj.Escenario(serial)
        MsgBox("SE HA COMPLETADO EL ENVIO DE DATOS")

    End Sub
End Class
