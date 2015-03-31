Class MainWindow 
    Dim yelpinfo As New Yelpbot
    Dim listing As New Listing

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
        Dim states As String() = {"AL", "AK", "AZ", "Arkansas", "CA", "CO", "CT", "Delaware", _
                                  "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", _
                                  "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", _
                                  "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", _
                                  "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", _
                                  "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", _
                                  "Wisconsin", "Wyoming"}

        For Each item In states
            cboState.Items.Add(item)
        Next
        Dim times As Array = {"Lunch", "Dinner", "Drinks"}
        For Each item In times
            cboTime.Items.Add(item)
        Next
    End Sub


    Private Sub btnSubmit_Click(sender As Object, e As RoutedEventArgs) Handles btnSubmit.Click

        yelpinfo.buildURL(txtCity.Text, "")
        listing.lblBiz1Address.Content = yelpinfo.names(0).ToString
        listing.lblBiz1Address.Content = yelpinfo.locations(0).ToString
        listing.lblBiz1Rating.Content = yelpinfo.ratings(0).ToString
        listing.lblBiz2Name.Content = yelpinfo.names(1).ToString
        listing.lblBiz2Address.Content = yelpinfo.locations(1).ToString
        listing.lblBiz1Rating.Content = yelpinfo.ratings(1).ToString
        listing.lblBiz3Name.Content = yelpinfo.names(2).ToString
        listing.lblBiz3Address.Content = yelpinfo.locations(2).ToString
        listing.lblBiz3Rating.Content = yelpinfo.ratings(2).ToString
        listing.Show()
    End Sub
End Class
