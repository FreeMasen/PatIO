Public Class Businesses
    'this class is designed to store a collection of business objects

    Inherits CollectionBase

    'this ensures that the items added are only
    'business objects
    Public Sub add(newBusiness As Business)
        Me.List.Add(newBusiness)
    End Sub

    'this defines how to remove a business object 
    'from the collection
    Public Sub remove(oldBusiness As Business)
        Me.List.Remove(oldBusiness)
    End Sub

    'this propert assignes an integer to each item
    'acting as a index
    Default Public Property item(index As Integer) As Business
        Get
            Return CType(Me.List.Item(index), Business)
        End Get
        Set(value As Business)
            Me.List.Item(index) = value
        End Set
    End Property


End Class
