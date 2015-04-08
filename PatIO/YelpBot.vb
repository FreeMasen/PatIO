Imports System.Net
Imports System
Imports System.IO
Imports System.Text
Imports SimpleOAuth
Imports System.Xml

Public Class Yelpbot
    Private _names As XmlNodeList
    Public Property names As XmlNodeList
        Get
            Return _names
        End Get
        Set(value As XmlNodeList)
            _names = value
        End Set
    End Property

    Private _locations As XmlNodeList
    Public Property locations As XmlNodeList
        Get
            Return _locations
        End Get
        Set(value As XmlNodeList)
            _locations = value
        End Set
    End Property

    Private _ratings As XmlNodeList
    Public Property ratings As XmlNodeList
        Get
            Return _ratings
        End Get
        Set(value As XmlNodeList)
            _ratings = value
        End Set
    End Property

    Private _images As XmlNodeList
    Public Property images As XmlNodeList
        Get
            Return _images
        End Get
        Set(value As XmlNodeList)
            _images = value
        End Set
    End Property
    Dim xml As XmlDocument = New XmlDocument
    Public zip As String



    Public Sub getXML(ByVal searchURL As String)
        Dim oauth_consumer_key As String = "aq4s508pN0XAgmgvw0shFQ"
        Dim consumer_secret As String = "0iujoBA_f2bpTAZuwHobPX90TyI"
        Dim oauth_token As String = "v07rB7TFgu8iRmvIXneieNCdFwTxH-w3"
        Dim oauth_token_secret As String = "rbAlZxdsim6m90YpnvLfR6AQ6Mk"

        Dim OTokens As Tokens = New Tokens()
        OTokens.ConsumerKey = oauth_consumer_key
        OTokens.ConsumerSecret = consumer_secret
        OTokens.AccessToken = oauth_token
        OTokens.AccessTokenSecret = oauth_token_secret

        Dim request As HttpWebRequest = CType(WebRequest.Create(searchURL), HttpWebRequest)
        request.SignRequest(OTokens).WithEncryption(EncryptionMethod.HMACSHA1).InHeader()

        Dim response As HttpWebResponse = CType(request.GetResponse, HttpWebResponse)
        Dim reader As Stream = response.GetResponseStream

        xml.Load(reader)

        reader.Close()

        xml.Save("yelpexample")
    End Sub

    Public Function buildURL(city As String, state As String) As String

        Dim searchLocation As String


        If city.Contains(" ") Then
            city.Replace(" ", "+")
        End If
        searchLocation = String.Format("{0}+{1}", city, state)

        Return String.Format("http://api.yelp.com/v2/search?term=patio&location={0}&category_filter=restaurants&sort=2&radius_filter=1700&limit=5&output=xml", searchLocation)

    End Function

    Public Sub setNodeLists()

        names = xml.SelectNodes("//name")
        locations = xml.SelectNodes("//address")
        ratings = xml.SelectNodes("//rating_img_url")
        images = xml.SelectNodes("//image_url")


    End Sub

    Public Function returnZip() As String
        Dim zipcode As XmlNode = xml.SelectSingleNode("//postal_code")
        zip = zipcode.InnerText.ToString
        Return zip
    End Function

End Class

