Feature: UserCannotBookEvent
	Check if a user cannot book event functionality works

@mytag
Scenario: UserCannotBookEvent
	Given I navigate to the application
	And  I click the Login link
	And I enter username and password
		| Username | Password |
		| barsha    | bar    |
	And I click login
	Then I should see user login to the application
	And I click the Book link
	And I enter number of tickets
		| TicketCount |
		| 2   |
	And I click on calculate button
	And I selected the payment mode from the drop down
		| PaymentMode |
		| Online_Payment  | 
	And I click on the book event button
	Then User cannot book event page will be displayed