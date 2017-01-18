Public Class frmGetParcel

    ' parcel helper 
    Dim _ParcelHelper As ParcelHelper = Nothing

    Public Sub SetParcelHelper(ByVal ParcelHelper As ParcelHelper)
        _ParcelHelper = ParcelHelper
    End Sub

    Private _ParcelInfo As ParcelHelper.ParcelInfo = Nothing

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            _ParcelInfo = _ParcelHelper.GetParcelInfo(txtParcelId.Text.Trim)
            If _ParcelInfo Is Nothing Then
                ErrorProvider1.SetError(txtParcelId, "Parcel could not be found")
            Else
                ErrorProvider1.SetError(txtParcelId, "")
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            ErrorProvider1.SetError(txtParcelId, "Parcel could not be found - " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property ParcelInfo As ParcelHelper.ParcelInfo
        Get
            Return _ParcelInfo
        End Get
    End Property
End Class