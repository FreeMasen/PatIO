Class MainWindow 
    Dim yelpinfo As New Yelpbot
    Dim wunder As New Wunderbot
    Dim results As New Businesses


    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'when the window opens, set the dropdowns
        setDropDowns()
    End Sub


    'since we wanted the dates to be dynamic
    'we set up a method that would be called when
    'the program is loaded 
    Private Sub setDropDowns()
        'first defindes an arraylist to store the dates
        Dim dates As New ArrayList

        'a for loop that will happen 7 times
        'using the item variable to hold what 
        'iteration we are on
        For item As Integer = 0 To 6
            'this variable will hold the date in the long format
            'it starts with now + 0 and moves though the 6 additional
            'days
            Dim datetoadd As Date = Now.AddDays(item)
            'check if it is today
            If datetoadd = Now Then
                'if it is, add today to the arraylist
                dates.Add("Today")
                'if it isn't today is is tomorrow (or today plus 1)
            ElseIf datetoadd = Now.AddDays(1) Then
                'if it is, add tomorrow to the list
                dates.Add("Tomorrow")
                'if it isn't today or tomorrow
            ElseIf datetoadd > Now.AddDays(1) Then
                'add the day of the week to the arraylist
                dates.Add(datetoadd.DayOfWeek.ToString)
            End If
        Next
        'now loop over our arraylist
        For Each item In dates
            'add each of the enteries to the combo box
            cboDate.Items.Add(item.ToString)
        Next
        'this sets the states to the two letter code for each state
        Dim states As String() = {"AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", _
                                  "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", _
                                  "LA", "ME", "MD", "MS", "MI", "MN", "MS", "MO", _
                                  "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", _
                                  "ND", "OH", "OK", "OR", "PA", "RI", "SC", _
                                  "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", _
                                  "WI", "WY"}
        'this loop addes them into the combobox
        For Each item In states
            cboState.Items.Add(item)
        Next
    End Sub

    Private Sub setLabels()
        'first it checks if the results first business' name
        'is nothing. This is designed to prevent the window from 
        'modifying when there is no business information is present
        If results(0).name Is "" Then
            lblName1.Content = ""
            lblAddress1.Content = ""
            lblName2.Content = ""
            lblAddress2.Content = ""
            lblName3.Content = ""
            lblAddress3.Content = ""
            'this is the original window size
            Me.Width = 304.238
        Else
            'set business one label to the business 1 name
            lblName1.Content = results(0).name.ToString
            'set the address one label to business 1 name
            lblAddress1.Content = results(0).location.ToString

            'I would like to encapsulate this into the business object
            '---------------------------------------------------

            'set this variable to a new uri 
            Dim rating As New Uri(results(0).rating.ToString, UriKind.Absolute)
            'convert the uri to an image source
            Dim ratingSource As ImageSource = New BitmapImage(rating)
            'set the image to the newly defined image source
            imgRating.Source = ratingSource

            'Dim picture As New Uri(results(0).bizImage.ToString, UriKind.Absolute)
            'Dim imgsource As ImageSource = New BitmapImage(picture)
            imgBiz1.Source = results(0).bizImage

            '---------------------------------------------------



            'same as above for business 2
            lblName2.Content = results(1).name.ToString
            lblAddress2.Content = results(1).location.ToString

            Dim rating1 As New Uri(results(1).rating.ToString, UriKind.Absolute)
            Dim ratingSource1 As ImageSource = New BitmapImage(rating1)
            imgRating1.Source = ratingSource1

            'Dim picture1 As New Uri(results(1).bizImage.ToString, UriKind.Absolute)
            'Dim imgsource1 As ImageSource = New BitmapImage(picture1)
            imgBiz2.Source = results(1).bizImage

            'and again for business 3
            lblName3.Content = results(2).name.ToString
            lblAddress3.Content = results(2).location.ToString
            Dim rating2 As New Uri(results(2).rating.ToString, UriKind.Absolute)
            Dim ratingSource2 As ImageSource = New BitmapImage(rating2)
            imgRating2.Source = ratingSource2

            'Dim picture2 As New Uri(results(2).bizImage.ToString, UriKind.Absolute)
            'Dim imgsource2 As ImageSource = New BitmapImage(picture2)
            imgBiz3.Source = results(2).bizImage

            'this changes the size of the window to make the form elements
            'visible
            Me.Width = 1200

            If wunder.outside = True Then
                lblWunder.Text = String.Format("The forcast is {0} and {1}, let's sit OUTSIDE!", wunder.temp, wunder.condition)
            Else
                lblWunder.Text = String.Format("the forcast is {0} and {1}, let's sit inside...", wunder.temp, wunder.condition)
            End If
            lblWarning.Content = ""
        End If
    End Sub

    'when the user clicks submit
    Private Sub btnSubmit_Click(sender As Object, e As RoutedEventArgs) Handles btnSubmit.Click
        results.Clear()
        If My.Computer.Network.IsAvailable = True Then
            If txtCity.Text.Length > 0 And cboState.SelectedIndex > -1 And cboDate.SelectedIndex > -1 Then
                Try
                    wunder.url(txtCity.Text, cboState.SelectedItem.ToString)
                    wunder.getQueryInfo()
                    wunder.setWeather(wunder.getDayInfo(cboDate.SelectedIndex))

                Catch ex As Exception

                End Try
            End If
            Try
                'uses the yelpbot object's method get xml, to generate the xml information
                'this needs a url as a variable, so we use the yelpbot objects buildurl method
                'to take the user informaiton from the city txt box and the state combobox
                'and the information from the wunderbot

                yelpinfo.getXML(yelpinfo.buildURL(txtCity.Text, cboState.SelectedItem.ToString, wunder.outside))
                'now that we have an xml document saved, we can set the yelpbot xmlnodelist variables
                yelpinfo.setNodeLists()

                If txtCity.Text.Length > 0 And cboDate.SelectedIndex > -1 And cboState.SelectedIndex > -1 Then
                    'loop 3 times, once for each busines and hold the number
                    'in the number varable
                    For number As Integer = 0 To 2
                        'set a temporary business object
                        Dim business As New Business
                        'pass into that tempory object the yelp information
                        'from the node lists, by using the number variable 0, 1 or 2
                        'this will ensure we are pulling the same business information
                        'relying on how xml is structured
                        business.buildBusiness(yelpinfo.names(number).InnerText, yelpinfo.locations(number).InnerText, business.convertImage(yelpinfo.ratings(number).InnerText), business.convertImage((yelpinfo.images(number).InnerText)))
                        results.add(business)
                    Next
                    setLabels()
                ElseIf txtCity.Text.Length = 0 Or IsNumeric(txtCity.Text) = True Then
                    lblWarning.Content = "Please enter a valid city"
                    Me.Width = 304
                ElseIf cboDate.SelectedIndex < 0 Then
                    lblWarning.Content = "Please choose a date"
                    Me.Width = 304
                ElseIf cboState.SelectedIndex < 0 Then
                    lblWarning.Content = "Please choose a state"
                    Me.Width = 304
                End If
            Catch ex As Exception
                lblWarning.Content = "Is your city/State combo valid?"
                Me.Width = 304
            End Try
        Else
            lblWarning.Content = "are you online?"
            Me.Width = 304
        End If




    End Sub
End Class