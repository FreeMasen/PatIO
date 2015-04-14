Imports System.Net
Imports System
Imports System.IO
Imports System.Text
Imports SimpleOAuth
Imports System.Xml

'This object is used to pull all of the xml information from yelp
'and store it as a set of xmlnodelist objects

Public Class Yelpbot
    'this variable stores the xmldocument informaiton
    Private xml As XmlDocument = New XmlDocument

    'this variable stores xml nodes for the names of businesses
    Private _names As XmlNodeList
    Public Property names As XmlNodeList
        Get
            Return _names
        End Get
        Set(value As XmlNodeList)
            _names = value
        End Set
    End Property

    'this variable stores the xml nodes for the address of businesses
    Private _locations As XmlNodeList
    Public Property locations As XmlNodeList
        Get
            Return _locations
        End Get
        Set(value As XmlNodeList)
            _locations = value
        End Set
    End Property

    'this variable stores the rating information for the address of businesses
    Private _ratings As XmlNodeList
    Public Property ratings As XmlNodeList
        Get
            Return _ratings
        End Get
        Set(value As XmlNodeList)
            _ratings = value
        End Set
    End Property

    'this variable stores the image of businesses
    Private _images As XmlNodeList
    Public Property images As XmlNodeList
        Get
            Return _images
        End Get
        Set(value As XmlNodeList)
            _images = value
        End Set
    End Property

    'this function takes information from the user and builds a string formatted
    'properly for the api call at yelp
    Public Function buildURL(city As String, state As String, weather As Boolean) As String

        'sets a string variable
        Dim searchLocation As String

        'checks if the user entered any " " characters
        If city.Contains(" ") Then
            'if there are spaces replace them with the "+" character
            city.Replace(" ", "+")
        End If
        'format the user input with the city+state
        searchLocation = String.Format("{0}+{1}", city, state)
        If weather = True Then
            'use the search location string to add the location to the URL            
            'and return it to the user                                                ↓
            Return String.Format("http://api.yelp.com/v2/search?term=summer&location={0}&category_filter=restaurants&category=bars&limit=10&output=xml", searchLocation)
            '                                                                         ↑
        ElseIf weather = False Then '                                                 ↓
            Return String.Format("http://api.yelp.com/v2/search?term=winter&location={0}&category_filter=restaurants&category=bars&limit=10&output=xml", searchLocation)
        End If '                                                                      ↑
    End Function

    'this method utilizes the simpleOauth library to build an oauth request
    Public Sub getXML(ByVal searchURL As String)
        'this is the token information provided to me by yelp
        Dim oauth_consumer_key As String = "aq4s508pN0XAgmgvw0shFQ"
        Dim consumer_secret As String = "0iujoBA_f2bpTAZuwHobPX90TyI"
        Dim oauth_token As String = "v07rB7TFgu8iRmvIXneieNCdFwTxH-w3"
        Dim oauth_token_secret As String = "rbAlZxdsim6m90YpnvLfR6AQ6Mk"

        'this creats a token object from the simpleOauth library and sets the 
        'above token information to the variables of the token object
        Dim OTokens As Tokens = New Tokens()
        OTokens.ConsumerKey = oauth_consumer_key
        OTokens.ConsumerSecret = consumer_secret
        OTokens.AccessToken = oauth_token
        OTokens.AccessTokenSecret = oauth_token_secret

        'creates a webrequest based on the searchURL variable that is passed into this function
        'this also signs the request with the token information
        Dim request As HttpWebRequest = CType(WebRequest.Create(searchURL), HttpWebRequest)
        request.SignRequest(OTokens).WithEncryption(EncryptionMethod.HMACSHA1).InHeader()

        'capturs the response from the web request
        Dim response As HttpWebResponse = CType(request.GetResponse, HttpWebResponse)
        'reads the response as a stream
        Dim reader As Stream = response.GetResponseStream

        'captures the stream information as a xmlDocument
        xml.Load(reader)

        'closes the connection to the website
        reader.Close()

        'saves a copy of the xmlDocument for debugging
        xml.Save("yelpexample")
    End Sub

    'this pulls the information from the xml document and stores
    'the nodes that I want to use
    Public Sub setNodeLists()
        'captures the <name> nodes
        names = xml.SelectNodes("//name")
        'captures the <address> nodes
        locations = xml.SelectNodes("//address")
        'captures the <rating_img_url> nodes, this is an image
        'of stars that are either filled, half filled or empty
        ratings = xml.SelectNodes("//rating_img_url")
        'captures the <image_url> nodes
        images = xml.SelectNodes("//image_url")
    End Sub
End Class

