' Simple project used to collect info from remote computer using WMI


Public Class frmPCinfo

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        ' Run each Sub to collect info
        Win32_OperatingSystem(txtHostnameOrIP.Text)
        Win32_Processor(txtHostnameOrIP.Text)
        Win32_BIOS(txtHostnameOrIP.Text)
        Win32_ComputerSystem(txtHostnameOrIP.Text)
        Win32_LogicalDisk(txtHostnameOrIP.Text)
        Win32_NetworkAdapterConfiguration(txtHostnameOrIP.Text)

    End Sub

    Private Sub Win32_OperatingSystem(ByVal strHostname As String)

        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_OperatingSystem")
        Dim strOSname As String = Nothing

        For Each objItem In colItems
            strOSname = objItem.Name.ToString()
            If strOSname <> Nothing Then Exit For
        Next

        ' Remove the drive partition info
        If strOSname.Contains("|") Then
            Dim arrSplit() As String = Split(strOSname, "|")
            strOSname = arrSplit(0)
        End If

        Log("OS: " & strOSname)

    End Sub

    Private Sub Win32_Processor(ByVal strHostname As String)

        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_Processor")
        Dim strCPU As String = Nothing

        For Each objItem In colItems
            strCPU = objItem.Name.ToString
            If strCPU <> Nothing Then Exit For
        Next

        Log("CPU: " & strCPU)
    End Sub

    Private Sub Win32_BIOS(ByVal strHostname As String)

        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_BIOS")
        Dim strSerialNumber As String = Nothing
        Dim strBIOSversion As String = Nothing

        For Each objItem In colItems
            strSerialNumber = objItem.SerialNumber.ToString
            strBIOSversion = objItem.SMBIOSBIOSVersion.ToString
        Next

        Log("BIOS SR#: " & strSerialNumber)
        Log("BIOS Ver: " & strBIOSversion)

    End Sub

    Private Sub Win32_ComputerSystem(ByVal strHostname As String)
        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_ComputerSystem")

        Dim strModel As String = Nothing
        Dim strName As String = Nothing
        Dim strMemory As String = Nothing
        Dim strUID As String = Nothing

        For Each objItem In colItems

            strModel = objItem.Model.ToString
            strName = objItem.Name.ToString
            strMemory = objItem.TotalPhysicalMemory.ToString
            strUID = objItem.UserName.ToString

        Next

        If strMemory > Nothing Then
            Dim intMemory As Long = CLng(strMemory)
            strMemory = FormatNumber((intMemory / 1024 / 1024), 0) & "Gb"
        End If

        Log("Model: " & strModel)
        Log("Name: " & strName)
        Log("RAM: " & strMemory)
        Log("Username: " & strUID)

    End Sub

    Private Sub Win32_LogicalDisk(ByVal strHostname As String)
        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_LogicalDisk")

        For Each objItem In colItems
            Try
                Log(("Drive: " & objItem.Caption.ToString).ToString.PadRight(8) &
                 (" | Size: " & ReformatNumber(objItem.Size.ToString)).ToString.PadRight(18) &
                 (" | Free: " & ReformatNumber(objItem.FreeSpace.ToString)).ToString.PadRight(18) &
                 (" | Type: " & objItem.Description).ToString)
            Catch ex As Exception
                Log("Error in Private Sub Win32_LogicalDisk.  Error: " & ex.Message)
            End Try
        Next
    End Sub

    Private Sub Win32_NetworkAdapterConfiguration(ByVal strHostname As String)
        Dim objWMIService As Object = GetObject("winmgmts:\\" & strHostname & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * from Win32_NetworkAdapterConfiguration Where IPEnabled=TRUE")

        Dim strDHCPenabled As String
        Dim strDHCPServer As String
        Dim strIPAddress As String
        Dim strMacAddress As String

        For Each objItem In colItems
            strDHCPenabled = " | DHCP enabled: " & objItem.DHCPEnabled
            strDHCPServer = " | DHCP server: " & objItem.DHCPServer.ToString
            If objItem.DHCPServer.ToString.Trim = String.Empty Then strDHCPServer = " | DHCP server: n/a"

            If objItem.IPAddress.Length >= 1 Then
                For i = LBound(objItem.IPAddress) To UBound(objItem.IPAddress)
                    strIPAddress = "IP: " & objItem.IPAddress(i)
                    strMacAddress = " | MAC: " & objItem.MACAddress(i)
                    Log(strIPAddress.ToString.PadRight(29) &
                        strMacAddress.ToString.PadRight(19) &
                        strDHCPenabled.ToString.PadRight(22) &
                        strDHCPServer)
                Next
            End If
        Next
    End Sub

    ' Reformat WMI Date/Time to normal Date/Time (not used in this app)
    Private Function WMIDateStringToDate(ByVal dtmDate)
        Dim dtWMIDateStringToDate = ""

        If dtmDate.ToString = Nothing Then
            Return ""
        Else
            ' Replace any * with zeros
            If InStr(dtmDate, "*") > 0 Then Replace(dtmDate, "*", "0")

            Try
                If dtmDate.ToString > "" Then
                    dtWMIDateStringToDate = CDate(Mid(dtmDate, 5, 2) & "/" &
                    Mid(dtmDate, 7, 2) & "/" & Strings.Left(dtmDate, 4) _
                    & " " & Mid(dtmDate, 9, 2) & ":" & Mid(dtmDate, 11, 2) & ":" & Mid(dtmDate, 13, 2))
                End If
            Catch ex As Exception
                WMIDateStringToDate = ""
            End Try
        End If

        Return dtWMIDateStringToDate
    End Function

    ' Reformat RAM and HDD sizes to Gb/Mb
    Public Shared Function ReformatNumber(ByVal InComingNumber As String)
        Dim strNumberToReformat As String = InComingNumber
        Dim NumberToReformat As Single

        If strNumberToReformat = "" Then strNumberToReformat = "0"

        NumberToReformat = CLng(strNumberToReformat)

        If NumberToReformat < 899999 Then    ' Kb
            NumberToReformat = NumberToReformat / 1024
            NumberToReformat = (FormatNumber(NumberToReformat, 3))
            strNumberToReformat = CStr(NumberToReformat) & " Kb"
        ElseIf NumberToReformat < 899999999 Then 'Mb
            NumberToReformat = NumberToReformat / 1024000
            NumberToReformat = (FormatNumber(NumberToReformat, 2))
            strNumberToReformat = CStr(NumberToReformat) & " Mb"
        ElseIf NumberToReformat < 899999999999 Then    ' Gb
            NumberToReformat = NumberToReformat / 1024000000
            NumberToReformat = (FormatNumber(NumberToReformat, 1))
            strNumberToReformat = CStr(NumberToReformat) & " Gb"
        ElseIf NumberToReformat < 899999999999999 Then   ' Tb
            NumberToReformat = NumberToReformat / 1024000000000
            NumberToReformat = (FormatNumber(NumberToReformat, 1))
            strNumberToReformat = CStr(NumberToReformat) & " Tb"
        End If

        Return strNumberToReformat
    End Function

    ' "Log" is easier than "rtbLog.AppendText(" every time
    Private Sub Log(ByVal strMessage As String)
        rtbLog.AppendText(strMessage & vbNewLine)
    End Sub

End Class
