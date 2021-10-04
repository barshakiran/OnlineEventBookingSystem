﻿Feature: Book
	Check if book event functionality works

@mytag
Scenario: BookEvent
	Given I navigate to the application
	And  I click the Login link
	And I enter username and password
		| Username | Password |
		| xyz    | xyz    |
	And I click login
	Then I should see user login to the application
	And I click the Book link
	And I enter number of tickets
		| TicketCount |
		| 4    |
	And I click on calculate button
	And I selected the payment mode from the drop down
		| PaymentMode |
		| Credit_Card  | 
	And I click on the book event button
	Then Booked event confirmation page will be displayed