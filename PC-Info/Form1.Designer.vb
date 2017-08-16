<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCinfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.txtHostnameOrIP = New System.Windows.Forms.TextBox()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(258, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(75, 23)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = "&Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'txtHostnameOrIP
        '
        Me.txtHostnameOrIP.Location = New System.Drawing.Point(100, 12)
        Me.txtHostnameOrIP.Name = "txtHostnameOrIP"
        Me.txtHostnameOrIP.Size = New System.Drawing.Size(152, 20)
        Me.txtHostnameOrIP.TabIndex = 0
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(11, 15)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(83, 13)
        Me.lblHost.TabIndex = 2
        Me.lblHost.Text = "Hostname or IP:"
        '
        'rtbLog
        '
        Me.rtbLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbLog.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbLog.Location = New System.Drawing.Point(11, 58)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New System.Drawing.Size(809, 323)
        Me.rtbLog.TabIndex = 2
        Me.rtbLog.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Output window:"
        '
        'frmPCinfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 393)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rtbLog)
        Me.Controls.Add(Me.lblHost)
        Me.Controls.Add(Me.txtHostnameOrIP)
        Me.Controls.Add(Me.btnGo)
        Me.Name = "frmPCinfo"
        Me.Text = "PC Info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGo As Button
    Friend WithEvents txtHostnameOrIP As TextBox
    Friend WithEvents lblHost As Label
    Friend WithEvents rtbLog As RichTextBox
    Friend WithEvents Label1 As Label
End Class
