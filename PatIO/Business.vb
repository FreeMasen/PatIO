Public Class Business
    'This is the skeleton of a business object
    'it stores the information that will be passed to the window's
    'display elements

    'this stores the name as a string
    Private _name As String
    Public Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    'this stores the location as a string
    Private _location As String
    Public Property location As String
        Get
            Return _location
        End Get
        Set(value As String)
            _location = value
        End Set
    End Property
    'this stores the rating url as a string
    'I would like to change this to be an image
    'with a method that would turn the image url
    'into a picture as it currently does in MainWindow
    Private _rating As String
    Public Property rating As String
        Get
            Return _rating
        End Get
        Set(value As String)
            _rating = value
        End Set
    End Property
    'this stores the image url as a string
    'I would also like to create a method to 
    'modify the url to an actuall image
    Private _image As String
    Public Property bizImage As String
        Get
            Return _image
        End Get
        Set(value As String)
            _image = value
        End Set
    End Property
    'this is the method that defines how a business is built
    Public Sub buildBusiness(name As String, location As String, rating As String, image As String)
        Me.name = name
        Me.location = location
        Me.rating = rating
        Me.bizImage = image
    End Sub

    'Public Function convertImage(imgURL As String) As ImageSource
    '    Dim imageURL As New Uri(imgURL, UriKind.Absolute)
    '    'convert the uri to an image source
    '    Dim imgSource As ImageSource = New BitmapImage(imageURL)

    '    Return imgSource
    'End Function

End Class
