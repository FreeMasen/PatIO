Class MainWindow 
    Dim yelpinfo As New Yelpbot
    Dim wunder As New Wunderbot
    Dim results As New Businesses
    'this solution was found on vbforums.com as a way to create
    'a background work in a WPF program, it will be leveraged as an
    'event method.
    'http://www.vbforums.com/showthread.php?577900-Backgroundworker-in-WPF
    Private WithEvents bkgroundWorker As New ComponentModel.BackgroundWorker
    Private WithEvents bkgroundWeather As New ComponentModel.BackgroundWorker

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
            'set the rating one image to the business 1 rating
            imgRating.Source = results(0).rating
            'set the image one image to the business 1 bizImage
            imgBiz1.Source = results(0).bizImage

            'same as above for business 2
            lblName2.Content = results(1).name.ToString
            lblAddress2.Content = results(1).location.ToString
            imgRating1.Source = results(1).rating
            imgBiz2.Source = results(1).bizImage

            'and again for business 3
            lblName3.Content = results(2).name.ToString
            lblAddress3.Content = results(2).location.ToString
            imgRating2.Source = results(2).rating
            imgBiz3.Source = results(2).bizImage

            'this changes the size of the window to make the form elements
            'visible
            Me.Width = 1200

            'this takes the outside bool assignment and modifies the 
            'weather data to tell the user what the conditions look
            'like. 
            If wunder.outside = True Then
                lblWunder.Text = String.Format("The forcast is {0} and {1}, let's sit OUTSIDE!", wunder.temp, wunder.condition)
            Else
                lblWunder.Text = String.Format("the forcast is {0} and {1}, let's sit inside...", wunder.temp, wunder.condition)
            End If
            'removes the content from the warning lable
            lblWarning.Content = ""
        End If
    End Sub

    'when the user clicks submit
    Private Sub btnSubmit_Click(sender As Object, e As RoutedEventArgs) Handles btnSubmit.Click
        Me.Width = 304
        'first check if the background worker is busy
        If bkgroundWeather.IsBusy = False Then
            'if not, update the warning label to tell the user we are getting the weather
            lblWarning.Content = "Getting Weather"
            'check if the form is filled out properly
            If txtCity.Text.Length > 0 And cboDate.SelectedIndex > -1 And cboState.SelectedIndex > -1 Then
                'if it is, then build the weather url
                wunder.url(txtCity.Text, CStr(cboState.SelectedValue))
                'and store it as a string in this variable
                Dim url As String = wunder.queryurl
                'now pass that string to the weather background worker
                bkgroundWeather.RunWorkerAsync(url)
                'if the form was not filled out properly update the warning label
                'to tell the user how to fix the problem
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
        End If
    End Sub

    Private Sub bkgroundWeather_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles bkgroundWeather.DoWork
        'if the computer is connected to a network
        If My.Computer.Network.IsAvailable = True Then
            'get the weather based on the weather url that was built it the click event
            wunder.getQueryInfo()
        End If
    End Sub

    Private Sub bkgroundWeather_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles bkgroundWeather.RunWorkerCompleted
        'check if there was an error getting the weather info
        If e.Error Is Nothing Then
            'if not check again that the computer is connected to a 
            'network
            If My.Computer.Network.IsAvailable = True Then
                Try
                    'if so then set the weather data into the arraylists for 
                    'us to use
                    wunder.setWeather(wunder.getDayInfo(cboDate.SelectedIndex))
                Catch ex As Exception
                    'if the weather search failed update the warining lable
                    'to tell the user that the city is not vaild
                    lblWarning.Content = "City is invalid"
                    Return
                End Try
                'check the the yelp background worker is busy
                If bkgroundWorker.IsBusy = False Then
                    'if not then update the warning label to tell the user
                    'we are on step 2
                    lblWarning.Content = "Getting restaurants"
                    'store the yelp url in this variable using the user's imput
                    'we checked that this was completed property when we first submitted
                    Dim yelp As String = yelpinfo.buildURL(txtCity.Text, CStr(cboState.SelectedValue), wunder.outside)
                    'run the yelp background worker and pass it the yelp url
                    bkgroundWorker.RunWorkerAsync(yelp)
                End If
            Else
                lblWarning.Content = "Please connect to the internet"
            End If
        End If
    End Sub

    Private Sub bkgroundWorker_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles bkgroundWorker.DoWork
        'first clear the results to ensure that
        'no old data is stored
        results.Clear()
        'check if we are still online
        If My.Computer.Network.IsAvailable = True Then
            Try
                'capture the url from the weather workercompleted
                Dim url As String = CStr(e.Argument)
                'use this to get the xml data
                yelpinfo.getXML(url)
            Catch ex As Exception
                'if there is an issue, end the work
                Return
            End Try
        Else
            lblWarning.Content = "are you online?"
            Me.Width = 304
        End If
    End Sub

    Private Sub bkgroundWorker_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles bkgroundWorker.RunWorkerCompleted
        If e.Error Is Nothing Then
            'capture that data into the array lists
            yelpinfo.setNodeLists()
            'for 3 loops
            For counter As Integer = 0 To 2
                'create a business as a new business
                Dim business As New Business
                'use the build business method to create a business object
                'we use the counter variable to make sure we are on the same 
                'response in the xml document
                business.buildBusiness(yelpinfo.names(counter).InnerText, yelpinfo.locations(counter).InnerText, _
                                       business.convertImage(yelpinfo.ratings(counter).InnerText), _
                                       business.convertImage(yelpinfo.images(counter).InnerText))
                'add this business to an array list
                results.add(business)
            Next
            'set the labels with the now completed data
            setLabels()
        Else
            'if there was an error, update the warning label
            lblWarning.Content = e.Error.ToString
        End If
    End Sub
End Class