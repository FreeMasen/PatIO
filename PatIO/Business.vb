Imports System.Net
Imports System.IO
Imports System.Xml

Public Class Business
    Private _name As String
    Public Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    Private _location As String
    Public Property location As String
        Get
            Return _location
        End Get
        Set(value As String)
            _location = value
        End Set
    End Property
    Private _rating As String
    Public Property rating As String
        Get
            Return _rating
        End Get
        Set(value As String)
            _rating = value
        End Set
    End Property

    Public Sub buildBusiness(name As String, location As String, rating As String, image As String)
        Me.name = name
        Me.location = location
        Me.rating = rating
        Me.bizImage = image
    End Sub
    Private _image As String
    Public Property bizImage As String
        Get
            Return _image
        End Get
        Set(value As String)
            _image = value
        End Set
    End Property

End Class
