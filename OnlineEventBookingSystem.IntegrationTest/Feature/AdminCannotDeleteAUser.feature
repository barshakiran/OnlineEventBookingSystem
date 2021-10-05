Feature: AdminCannotDeleteAUser
	Check if a admin cannot delete a user functionality works

@mytag
Scenario: AdminCannotDeleteAUser
	Given I navigate to the application
	And  I click the Login link
	And I enter username and password
		| Username | Password |
		| Admin    | admin    |
	And I click login
	And I should see admin login to the application
	And I click User link
	And I click Delete link
	And I click OK button on the alert message box
	And I click OK button on the alert message box
	Then The user exists in the user list