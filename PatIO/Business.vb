Public Class Business
    'This is the skeleton of a business object
    'it stores the information that will be passed to the window's
    'display elements
    Public Property name As String

    Public Property location As String

    Public Property rating As ImageSource

    Public Property bizImage As ImageSource


    Public Sub New(name As String, location As String, rating As ImageSource, image As ImageSource)
        Me.name = name
        Me.location = location
        Me.rating = rating
        Me.bizImage = image
    End Sub

    Public Shared Function convertImage(imgURL As String) As ImageSource
        Dim imageURL As New Uri(imgURL, UriKind.Absolute)
        'convert the uri to an image source
        Dim imgSource As ImageSource = New BitmapImage(imageURL)

        Return imgSource
    End Function

End Class
