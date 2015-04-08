Class MainWindow 
    Dim yelpinfo As New Yelpbot
    Dim results As New Businesses

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        setDropDowns()
    End Sub

    Private Sub setDropDowns()
        Dim dates As New ArrayList

        For item As Integer = 0 To 6
            Dim datetoadd As Date = Now.AddDays(item)
            If datetoadd = Now Then
                dates.Add("Today")
            ElseIf datetoadd = Now.AddDays(1) Then
                dates.Add("Tomorrow")
            ElseIf datetoadd > Now.AddDays(1) Then
                dates.Add(datetoadd.DayOfWeek.ToString)
            End If
        Next
        For Each item In dates
            cboDate.Items.Add(item.ToString)
        Next
        Dim states As String() = {"AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", _
                                  "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", _
                                  "LA", "ME", "MD", "MS", "MI", "MN", "MS", "MO", _
                                  "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", _
                                  "ND", "OH", "OK", "OR", "PA", "RI", "SC", _
                                  "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", _
                                  "WI", "WY"}

        For Each item In states
            cboState.Items.Add(item)
        Next
        Dim times As Array = {"Lunch", "Dinner", "Drinks"}

    End Sub

    Private Sub setLabels()
        If results(0).name Is Nothing Then
            lblName1.Content = ""
            lblAddress1.Content = ""
            lblName2.Content = ""
            lblAddress2.Content = ""
            lblName3.Content = ""
            lblAddress3.Content = ""
            Me.Width = 266.238
        Else
            'set business one label to the business 1 name
            lblName1.Content = results(0).name.ToString
            'set the address one label to business 1 name
            lblAddress1.Content = results(0).location.ToString
            'set this variable to a new uri 
            Dim rating As New Uri(results(0).rating.ToString, UriKind.Absolute)
            'convert the uri to an image source
            Dim ratingSource As ImageSource = New BitmapImage(rating)
            'set the image to the newly defined image source
            imgRating.Source = ratingSource

            Dim picture As New Uri(results(0).bizImage.ToString, UriKind.Absolute)
            Dim imgsource As ImageSource = New BitmapImage(picture)
            imgBiz1.Source = imgsource

            lblName2.Content = results(1).name.ToString
            lblAddress2.Content = results(1).location.ToString

            Dim rating1 As New Uri(results(1).rating.ToString, UriKind.Absolute)
            Dim ratingSource1 As ImageSource = New BitmapImage(rating1)
            imgRating1.Source = ratingSource1

            Dim picture1 As New Uri(results(1).bizImage.ToString, UriKind.Absolute)
            Dim imgsource1 As ImageSource = New BitmapImage(picture1)
            imgBiz2.Source = imgsource1

            lblName3.Content = results(2).name.ToString
            lblAddress3.Content = results(2).location.ToString
            Dim rating2 As New Uri(results(2).rating.ToString, UriKind.Absolute)
            Dim ratingSource2 As ImageSource = New BitmapImage(rating2)
            imgRating2.Source = ratingSource2

            Dim picture2 As New Uri(results(2).bizImage.ToString, UriKind.Absolute)
            Dim imgsource2 As ImageSource = New BitmapImage(picture2)
            imgBiz3.Source = imgsource2
            Me.Width = 891.238
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As RoutedEventArgs) Handles btnSubmit.Click
        yelpinfo.getXML(yelpinfo.buildURL(txtCity.Text, cboState.SelectedItem.ToString))
        yelpinfo.setNodeLists()
        Dim business1 As New Business
        Dim business2 As New Business
        Dim business3 As New Business
        For number As Integer = 0 To 2
            Dim business As New Business
            business.buildBusiness(yelpinfo.names(number).InnerText, yelpinfo.locations(number).InnerText, yelpinfo.ratings(number).InnerText, yelpinfo.images(number).InnerText)
            If number = 0 Then
                business1 = business
                results.add(business1)
            ElseIf number = 1 Then
                business2 = business
                results.add(business2)
            ElseIf number = 2 Then
                business3 = business
                results.add(business3)
            End If
        Next
        setLabels()
        '    lblName1.Content = business1.name.ToString
        '    lblAddress1.Content = business1.location.ToString
        '    lblRating1.Content = business1.rating.ToString
        '    lblName2.Content = business2.name.ToString
        '    lblAddress2.Content = business2.location.ToString
        '    lblRating2.Content = business2.rating.ToString
        '    lblName3.Content = business3.name.ToString
        '    lblAddress3.Content = business3.location.ToString
        '    lblRating3.Content = business3.rating.ToString
    End Sub
End Class