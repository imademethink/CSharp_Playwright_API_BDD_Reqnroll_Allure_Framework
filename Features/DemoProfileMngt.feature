Feature: User profile management

  @case1
  Scenario: Registration and Login validation
    Given Complete formal registration
    When Process profile login
    Then Validate response for login

  @case2
  Scenario: Registration Login Logout validation
    Given Complete formal registration
    When Complete formal login
    When Process profile logout
    Then Validate response for logout

  @case3
  Scenario: Registration Login Get-Profile Logout validation
    Given Complete formal registration
    When Complete formal login
    When Process get profile
    Then Validate response for get profile
    Then Complete formal logout

  @case4
  Scenario: Registration Login Forget-Password Logout validation
    Given Complete formal registration
    When Complete formal login
    When Process forget password
    Then Validate response for forget password
    Then Complete formal logout

  @case5
  Scenario: Registration Login Change-Password Logout validation
    Given Complete formal registration
    When Complete formal login
    When Process change password
    Then Validate response for change password
    Then Complete formal logout

  @case6
  Scenario: Registration Login Delete-Account validation
    Given Complete formal registration
    When Complete formal login
    When Process delete account
    Then Validate response for delete account
    Then Validate response for invalid login

  @case7
  Scenario: Login without registration
    When Complete formal login
