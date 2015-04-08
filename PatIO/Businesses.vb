Public Class Businesses
    Inherits CollectionBase

    Public Sub add(newBusiness As Business)
        Me.List.Add(newBusiness)
    End Sub

    Public Sub remove(oldBusiness As Business)
        Me.List.Remove(oldBusiness)
    End Sub

    Default Public Property item(index As Integer) As Business
        Get
            Return CType(Me.List.Item(index), Business)
        End Get
        Set(value As Business)
            Me.List.Item(index) = value
        End Set
    End Property
End Class
